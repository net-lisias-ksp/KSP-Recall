/*
	This file is part of Refunding, a component of KSP-Recall
	(C) 2020-2021 Lisias T : http://lisias.net <support@lisias.net>

	KSP-Recall is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP-Recall is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP-Recall. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP-Recall. If not, see <https://www.gnu.org/licenses/>.

*/
using System.Reflection;

namespace KSP_Recall { namespace Refunds 
{
	public class Refunding : PartModule
	{
		internal const string RESOURCENAME = "RefundingForKSP111x";
		internal const string MODULECARGOPART = "ModuleCargoPart";

		#region KSP UI

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::Refunding")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = false)]
		public double OriginalCost = 0f;

		#endregion

		private Part _prefab = null;
		private Part prefab { get => _prefab ?? (_prefab = this.part.partInfo.partPrefab); set => _prefab = value; }
		internal double costFix = 0;
		private int delayTicks = 0;

		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			// Here we have a problem. Some parts have the this.part.partInfo set, others don't.
			// Don't know why, and this is worrying - TweakScale depends on this! Is this a race condition?

			// Keep this disabled until someone need the recalculated costs, to avoiding being incorrectly charged on Launch
			// (we are charged after the Craft it's initialised on launch)
			this.enabled = false;

			this.active = Globals.Instance.Refunding;
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2} {3}", this.name, this.part.GetInstanceID(), state, this.active);
			base.OnStart(state);
		}

		public override void OnCopy(PartModule fromModule)
		{
			Log.dbg("OnCopy {0}:{1:X} from {2:X}", this.name, this.part.GetInstanceID(), fromModule.part.GetInstanceID());
			base.OnCopy(fromModule);
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
			if (null == this.part.partInfo)
				this.prefab = this.part;

			// Always clean up the Resource on loading, as we need to get rid of reminiscents
			// of the previous attempts. We don't want the Refunding resource on Edit Scene anyway.
			//
			// Additionally, clean up Parts those Refunding was inactivated by the user.
			this.RemoveResourceIfAvailable();

			if (!this.active) return;

			// The cost of the craft is billed on launch, **after** loading the craft.
			//
			// So we need to postpone the Resource restoring until the last moment, or the
			// user will be overbilled.
			//
			// The OriginalCost is calcultated on Editing time, but the Resource will be updated
			// only at Flight time after launch.
 		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void FixedUpdate()
		{
			if (!this.active) return;
			if (0 != --this.delayTicks) return;
			Log.dbg("FixedUpdate {0}:{1:X}", this.name, this.part.GetInstanceID());

			switch(HighLogic.LoadedScene)
			{
				case GameScenes.FLIGHT:
					this.SynchronousFullUpdate();
					break;
				case GameScenes.EDITOR:
					this.CalculateOriginalCost();
					break;
				default:
					break;
			}
			this.enabled = false;
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion

		#region Part Events Handlers

#if false
		// This apparently is not needed, and we need to delay this thing anyway to avoiding initialising before
		// KSP charges for Launch, and it's easier to handle this without reluing on GameEvents.OnFundsChanged.
		[KSPEvent(guiActive = false, active = true)]
		void OnPartScaleChanged(BaseEventDetails data)
		{
			Log.dbg("OnPartScaleChanged");
			this.AsynchronousFullUpdate();
		}
#endif

		#endregion

		// Should be called while flight or editing, where you don't need to get the module updated on the spot.
		// This spreads the load on time, avoiding overloading the CPU on hot code.
		internal void AsynchronousUpdate(int delayTicks = 1)
		{
			this.delayTicks = delayTicks;
			this.enabled = this.active;
		}

		// Should be called before iminent situations (as flight termination) where you *NEED* the thing updated before something "terminal" happens.
		// (screw the CPU, we need the data NOW).
		internal void SynchronousFullUpdate()
		{
			if (!this.active) return; // Just in case someone call it directly

			this.Recalculate();
			this.UpdateResource();
		}

		private void Recalculate()
		{
			Log.dbg("Recalculate {0}:{1:X}", this.name, this.part.GetInstanceID());
			if (!this.active)
			{
				this.costFix = 0;
				return;
			}

			double resourceCosts = this.CalculateResourcesCost();
			double wrongCost = this.OriginalCost - resourceCosts;
			double rightCost = this.OriginalCost + this.CalculateModulesCost() - resourceCosts;
			this.costFix = -wrongCost + rightCost;

			Log.dbg("Recalculate Results originalCost: {0:0.0}; resourceCosts:{1:0.0}; wrongCost:{2:0.0}; rightCost:{3:0.0}; fix:{4:0.0} ; ", this.OriginalCost, resourceCosts, wrongCost, rightCost, this.costFix);
		}

		private void CalculateOriginalCost()
		{
			double r = this.part.partInfo.cost;
			r += this.CalculateResourcesCost();
			r += this.CalculateModulesCost();
			this.OriginalCost = r;
		}

		private double CalculateResourcesCost()
		{
			double r = 0;
			foreach (PartResource pr in this.part.Resources) if (RESOURCENAME != pr.resourceName)
			{
				double cost = (null != pr.info ? (pr.amount * pr.info.unitCost) : 0); // Why some resources have no info? o.O
				// Why this.part.vessel is NULL at this point? :/
				//Log.dbg("CalculateResourcesCost({0},{1},{2}) => {3}", this.part.vessel.vesselName, this.part.partInfo.partName, pr.resourceName, cost);
				Log.dbg("CalculateResourcesCost({0},{1}) => {2}", this.part.partInfo.name, pr.resourceName, cost);
				r += cost;
			}
			return r;
		}

		private double CalculateModulesCost()
		{
			double r = 0;
			foreach (PartModule pm in this.part.Modules) if (pm is IPartCostModifier)
			{
				float cost = ((IPartCostModifier)pm).GetModuleCost(0, ModifierStagingSituation.CURRENT);
				Log.dbg("CalculateModulesCost({0},{1}) => {2}", this.part.partInfo.name, pm.moduleName, cost);
				r += cost;
			}
			return r;
		}

		private void UpdateResource()
		{
			Log.dbg("UpdateResource {0}:{1:X}", this.part.partInfo.name, this.part.GetInstanceID());
			PartResource pr = this.part.Resources.Get(RESOURCENAME) ?? this.RestoreResource();

			Log.dbg("Before {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
			//pr.SetInfo(this.CreateCustomResourceDef(this.costFix/pr.maxAmount)); // TweakScale scales the Resource MaxAmount, so we need to divide the cost by tje current maxAmount/amount

			double scaledValue = (1/pr.info.unitCost) * this.costFix; // To compensate the 0.0001 uniCost of the PartReourceDefinition

			// One of the ugliest hacks I even did on KSP. Pretty nasty!! :P

			// This will prevent the cost of the Refunding from being subtracked from the cost of the Part, what would counter-attack
			// the fix below
			FieldInfo field = typeof(PartResource).GetField("maxAmount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, 0d);

			// This effectivelly "steals back" the Funds lost by the KSP's current stunt (using the prefab's cost on recovering costs)
			// See https://github.com/net-lisias-ksp/KSP-Recall/issues/12
			field = typeof(PartResource).GetField("amount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, scaledValue);

			Log.dbg("After {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
		}

		private void RemoveResourceIfAvailable()
		{
			Log.dbg("Removing {0} from part {1}-{2}:{3:X}", RESOURCENAME, this.part.vessel.vesselName, this.part.partName, this.part.GetInstanceID());

			PartResource pr = this.part.Resources.Get(RESOURCENAME);
			if (null != pr) this.part.Resources.Remove(pr);
		}

		// Remove the Refunding Resource from parts with PartModuleCargo that can be stackable
		internal void RemoveResourceWhenNeeded()
		{
			if (this.IsStackable())
				this.RemoveResourceIfAvailable();
		}

		private bool IsStackable()
		{
			bool r = this.part.Modules.Contains(MODULECARGOPART);

			if (r) { // Parts with resources are not stackable.
				int count = 0;
				foreach (PartResource pr in this.part.Resources) if (!RESOURCENAME.Equals(pr.resourceName))
					++count;
				r &= (0 == count);
			}

			// Yeah, I'm using reflection - so this PartModule is still useable on previous KSP versions if the user choose to fool around.
			// Hacky and messy, uh? ]:->
			if (r) { // If the part has resources, there's no point on check for the stackable
				System.Type type = System.Type.GetType(MODULECARGOPART, false, true);
				r &= (null != type);
				if (r) 
					foreach (PartModule pm in this.part.Modules) if (type.Equals(pm.GetType()))
					{
						FieldInfo field = type.GetField("stackableQuantity", BindingFlags.Instance | BindingFlags.Public);
						r &= (null != field) && ((int)field.GetValue(pm) > 0);
						break;
					}
			}

			Log.dbg("{0}-{1}:{2:X} is {3}", this.part.vessel.vesselName, this.part.partName, this.part.GetInstanceID(), r ? "stackable" : "not stackable");
			return r;
		}

#if true
		private PartResource RestoreResource()
		{
			// THIS SHOULD NOT BE CALLED when this.active is false, or it may inject a Refunding resource when none is desired.
			PartResource pr = this.part.Resources.Get(RESOURCENAME);
			if (null == pr)
			{
				PartResourceDefinition prd = PartResourceLibrary.Instance.GetDefinition(RESOURCENAME);
				this.part.Resources.Add(prd.name, 0, 1, false, false, false, false, PartResource.FlowMode.None);
			}
			if (1d == pr.maxAmount) return pr;

			FieldInfo field = typeof(PartResource).GetField("maxAmount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, 1d);
			pr.amount = 0;
			return pr;
		}
#else
		private void RestoreResource()
		{
			Log.dbg("RestoreResource {0}:{1:X}", this.name, this.part.GetInstanceID());
			PartResource pr = this.part.Resources.Get(RESOURCENAME);
			Log.dbg("Before {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
			pr.SetInfo(PartResourceLibrary.Instance.GetDefinition(RESOURCENAME));
			Log.dbg("After {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
		}

		private PartResourceDefinition CreateCustomResourceDef(double unitCost)
		{
			PartResourceDefinition prd = PartResourceLibrary.Instance.GetDefinition(RESOURCENAME);
			Log.dbg("ConfigNode Source: {0}", prd.Config.ToString());

			ConfigNode cn = new ConfigNode("RESOURCE_DEFINITION");
			cn.SetValue("unitCost", unitCost, true);
			prd.Save(cn);
			PartResourceDefinition r = new PartResourceDefinition();
			r.Load(cn);
			return r;
		}
#endif

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<Refunding>("KSP-Recall", "Refunding");
	}
} }
