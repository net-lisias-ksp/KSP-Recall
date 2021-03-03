/*
	This file is part of Resourceful, a component of KSP-Recall
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
using System;
using System.Linq;
using System.Reflection;

namespace KSP_Recall { namespace Refunds 
{
	public class Refunding : PartModule
	{
		internal const string RESOURCENAME = "RefundingForKSP111x";

		#region KSP UI

		[KSPField(isPersistant = false, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::Refunding")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion

		private Part _prefab = null;
		private Part prefab { get => _prefab ?? (_prefab = this.part.partInfo.partPrefab); set => _prefab = value; }
		internal double costFix = 0;

		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			// Here we have a problem. Some parts have the this.part.partInfo set, others don't.
			// Don't know why, and this is worrying - TweakScale depends on this! Is this a race condition?
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
			this.enabled = false;
 		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void Update()
		{
			switch(HighLogic.LoadedScene)
			{
				case GameScenes.FLIGHT:
					this.Recalculate();
					this.UpdateResource();
					break;
				case GameScenes.EDITOR:
					this.RestoreResource();
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

		[KSPEvent(guiActive = false, active = true)]
		void OnPartScaleChanged(BaseEventDetails data)
		{
			//int instanceId = data.Get<int>("InstanceID");
			//if (this.part.GetInstanceID() != instanceId) return;

			//Type issuer = data.Get<Type>("issuer");
			//Log.dbg("OnPartResourcesChanged for InstanceId {0:X}, issued by {1}", instanceId, issuer);
			Log.dbg("OnPartScaleChanged");
			this.enabled = true;
		}

		#endregion

		internal void Recalculate()
		{
			Log.dbg("Recalculate {0}:{1:X}", this.name, this.part.GetInstanceID());
			if (!this.part.Resources.Contains(RESOURCENAME)) return;

			double originalCost = this.CalculateOriginalCost();
			double resourceCosts = this.CalculateResourcesCost();
			double wrongCost = originalCost - resourceCosts;
			double rightCost = originalCost + this.CalculatetModulesCost() - resourceCosts;
			this.costFix = -wrongCost + rightCost;

			Log.dbg("Recalculate Results originalCost: {0:0.0}; resourceCosts:{1:0.0}; wrongCost:{2:0.0}; rightCost:{3:0.0}; fix:{4:0.0} ; ", originalCost, resourceCosts, wrongCost, rightCost, this.costFix);
		}

		private double CalculateOriginalCost()
		{
			double r = this.part.partInfo.cost;
			foreach (PartResource pr in this.prefab.Resources)
				r += (null != pr.info ? (pr.amount * pr.info.unitCost) : 0);
			return r;
		}

		private double CalculatetModulesCost()
		{
			double r = 0;
			foreach (PartModule pm in this.part.Modules) if (pm is IPartCostModifier)
				r += ((IPartCostModifier)pm).GetModuleCost(0, ModifierStagingSituation.CURRENT);
			return r;
		}

		private double CalculateResourcesCost()
		{
			double r = 0;
			foreach (PartResource pr in this.part.Resources) if (RESOURCENAME != pr.resourceName)
				r += pr.maxAmount * pr.info.unitCost;
			return r;
		}

		private void UpdateResource()
		{
			Log.dbg("UpdateResource {0}:{1:X}", this.name, this.part.GetInstanceID());
			PartResource pr = this.part.Resources.Get(RESOURCENAME);

			Log.dbg("Before {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
			//pr.SetInfo(this.CreateCustomResourceDef(this.costFix/pr.maxAmount)); // TweakScale scales the Resource MaxAmount, so we need to divide the cost by tje current maxAmount/amount

			double scaledValue = 10000 * this.costFix; // To compensate the 0.0001 uniCost of the PartReourceDefinition

			FieldInfo field = typeof(PartResource).GetField("maxAmount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, 0d);

			field = typeof(PartResource).GetField("amount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, scaledValue);

			Log.dbg("After {0} {1} {2} {3}", pr.ToString(), pr.amount, pr.maxAmount, pr.info.unitCost);
		}

#if true
		private void RestoreResource()
		{
			PartResource pr = this.part.Resources.Get(RESOURCENAME);
			if (0d == pr.maxAmount) return;

			FieldInfo field = typeof(PartResource).GetField("maxAmount", BindingFlags.Instance | BindingFlags.Public);
			field.SetValue(pr, 0d);
			pr.amount = 0;
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
