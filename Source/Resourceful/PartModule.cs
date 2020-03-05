/*
	This file is part of Resourceful, a component of KSP-Recall
	(C) 2020 Lisias T : http://lisias.net <support@lisias.net>

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
	along with KSP-Recall If not, see <https://www.gnu.org/licenses/>.

*/
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Recall

{
	public class Resourceful : PartModule
	{

		#region KSP UI

		[KSPField(isPersistant = false, guiActiveEditor = true, guiName = "KSP-Recall::Resourceful")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion


		private struct Resource_t
		{
			[SerializeField] private PartResource node;

			internal PartResource ToPartResource(Part part)
			{
				PartResource r = new PartResource(part);
				r.Copy(node);
				return r;
			}

			internal static Resource_t From(PartResource pr)
			{
				Resource_t r = new Resource_t();
				r.node = pr;
				return r;
			}
		}

		private class Resource_List
		{
			private readonly Dictionary<int, List<Resource_t>> map = new Dictionary<int, List<Resource_t>>();

			public void Clear(Part part)
			{
				this.map[part.GetInstanceID()].Clear();
			}

			public void Destroy(Part part)
			{
				this.map.Remove(part.GetInstanceID());
			}

			public List<Resource_t> List(Part part)
			{
				if (!this.map.ContainsKey(part.GetInstanceID())) this.map[part.GetInstanceID()] = new List<Resource_t>();
				return this.map[part.GetInstanceID()];
			}

			public bool HasSomething(Part part) => this.map.ContainsKey(part.GetInstanceID());

			internal void Copy(Part from, Part to)
			{
				List<Resource_t> l = this.List(to);
				l.Clear();
				l.AddRange(this.List(from));
			}
		}

		private static readonly Resource_List RESOURCE_POOL = new Resource_List();


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			if (RESOURCE_POOL.HasSomething(this.part)) this.RestoreResourceList();
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
			if (RESOURCE_POOL.HasSomething(fromModule.part))
			{
				RESOURCE_POOL.Copy(fromModule.part, this.part);
				this.RestoreResourceList();
			}
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
			RESOURCE_POOL.Destroy(this.part);
		}

		#endregion

		#region Part Events Handlers
		[KSPEvent(guiActive = false, active = true)]
		void OnPartResourceChanged(BaseEventDetails data)
		{
			// Just to validate the package
			int instanceId = data.Get<int>("InstanceID");

			Log.dbg("OnPartResourceChanged for InstanceId {0:X}", instanceId);

			this.UpdateResourceList();
		}
		#endregion

		private void UpdateResourceList()
		{
			RESOURCE_POOL.List(this.part).Clear();
			foreach (PartResource pr in this.part.Resources)
				RESOURCE_POOL.List(this.part).Add(Resource_t.From(pr));
			Log.dbg("Updated {0} resources for {1}:{2:X}", RESOURCE_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());
		}

		private void RestoreResourceList()
		{
			if (!this.active)
			{
				Log.dbg("Ignoring {0} resources for {1}:{2:X}", RESOURCE_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());
				return;
			}
			Log.dbg("Restoring {0} resources for {1}:{2:X}", RESOURCE_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());

			this.part.Resources.Clear();
			foreach (Resource_t resource in RESOURCE_POOL.List(this.part))
				this.part.Resources.Add(resource.ToPartResource(this.part));
		}

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<Resourceful>("KSP-Recall", "Resourceful");
		static Resourceful()
		{
			Log.level =
#if DEBUG
				KSPe.Util.Log.Level.TRACE
#else
				KSPe.Util.Log.Level.INFO
#endif
				;
		}
	}
}
