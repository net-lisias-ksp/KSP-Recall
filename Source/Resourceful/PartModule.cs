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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Recall

{
	public class Resourceful : PartModule
	{

		#region KSP UI

		[KSPField(isPersistant = false, guiActiveEditor = true, guiName = "KSP-Recall::Resourceful")]//Scale
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion


		[Serializable]
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

		[Serializable]
		private class Resource_List : ScriptableObject
		{
			private List<Resource_t> list = null;
			public List<Resource_t> List => this.list ?? (this.list = new List<Resource_t>());
		}

		[SerializeField]
		private Resource_List resource;


		#region KSP Events

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			if (null == this.resource) this.resource = new Resource_List();
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2} {3}", this.name, this.part.GetInstanceID(), state, this.active);
			base.OnStart(state);
			if (0 != this.resource.List.Count) this.RestoreResourceList();
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
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
			this.resource.List.Clear();
			foreach (PartResource pr in this.part.Resources)
				this.resource.List.Add(Resource_t.From(pr));
			Log.dbg("Updated {0} resources for {1}", this.resource.List.Count, this.name);
		}

		private void RestoreResourceList()
		{
			Log.dbg("Restoring {0} resources from {1}", this.resource.List.Count, this.name);

			this.part.Resources.Clear();
			foreach (Resource_t resource in this.resource.List)
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
