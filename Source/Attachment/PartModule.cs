/*
	This file is part of Attachment, a component of KSP-Recall
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
	along with KSP-Recall. If not, see <https://www.gnu.org/licenses/>.

*/
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Recall

{
	public class Attachment: PartModule
	{

		#region KSP UI

		[KSPField(isPersistant = false, guiActiveEditor = true, guiName = "KSP-Recall::Attachment")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion


		private struct Attachment_t
		{
			[SerializeField] private AttachNode node;

			internal AttachNode ToPartAttachment()
			{
				AttachNode r = AttachNode.Clone(this.node);
				return r;
			}

			internal static Attachment_t From(AttachNode pan)
			{
				Attachment_t r = new Attachment_t
				{
					node = pan
				};
				return r;
			}
		}

		private class Attachment_List
		{
			private readonly Dictionary<int, List<Attachment_t>> map = new Dictionary<int, List<Attachment_t>>();

			public void Clear(Part part)
			{
				this.map[part.GetInstanceID()].Clear();
			}

			public void Destroy(Part part)
			{
				this.map.Remove(part.GetInstanceID());
			}

			public List<Attachment_t> List(Part part)
			{
				if (!this.map.ContainsKey(part.GetInstanceID())) this.map[part.GetInstanceID()] = new List<Attachment_t>();
				return this.map[part.GetInstanceID()];
			}

			public bool HasSomething(Part part) => this.map.ContainsKey(part.GetInstanceID());

			internal void Copy(Part from, Part to)
			{
				List<Attachment_t> l = this.List(to);
				l.Clear();
				l.AddRange(this.List(from));
			}
		}

		private static readonly Attachment_List ATTACHMENT_POOL = new Attachment_List();


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			if (ATTACHMENT_POOL.HasSomething(this.part)) this.RestoreAttachmentList();
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
			if (ATTACHMENT_POOL.HasSomething(fromModule.part))
			{
				ATTACHMENT_POOL.Copy(fromModule.part, this.part);
				this.RestoreAttachmentList();
			}
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
			ATTACHMENT_POOL.Destroy(this.part);
		}

		#endregion

		#region Part Events Handlers
		[KSPEvent(guiActive = false, active = true)]
		void OnPartAttachNodeChanged(BaseEventDetails data)
		{
			// Just to validate the package
			int instanceId = data.Get<int>("InstanceID");

			Log.dbg("OnPartAttachNodeChanged for InstanceId {0:X}", instanceId);

			this.UpdateAttachNodeList();
		}
		#endregion

		private void UpdateAttachNodeList()
		{
			ATTACHMENT_POOL.List(this.part).Clear();
			foreach (AttachNode pan in this.part.attachNodes)
				ATTACHMENT_POOL.List(this.part).Add(Attachment_t.From(pan));
			Log.dbg("Updated {0} Attach Nodes for {1}:{2:X}", ATTACHMENT_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());
		}

		private void RestoreAttachmentList()
		{
			if (!this.active)
			{
				Log.dbg("Ignoring {0} Attach Nodes for {1}:{2:X}", ATTACHMENT_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());
				return;
			}
			Log.dbg("Restoring {0} attachments for {1}:{2:X}", ATTACHMENT_POOL.List(this.part).Count, this.name, this.part.GetInstanceID());

			this.part.attachNodes.Clear();
			foreach (Attachment_t attachNode in ATTACHMENT_POOL.List(this.part))
				this.part.attachNodes.Add(attachNode.ToPartAttachment());
		}

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<Attachment>("KSP-Recall", "Attachment");
		static Attachment()
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
