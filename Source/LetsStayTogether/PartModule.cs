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
using System.Collections.Generic;

namespace KSP_Recall { namespace StayingTogether 
{
	public class LetsStayTogether : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::LetsStayTogether")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion

		private readonly Dictionary<string,AttachNode> dict = new Dictionary<string,AttachNode>();

		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}", this.PartInstanceId);
			base.OnAwake();

			this.enabled = false;
			this.active = Globals.Instance.LetsStayTogether;
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0} {1} {2}", this.PartInstanceId, state, this.active);
			base.OnStart(state);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion

		#region Part Events Handlers

		internal void SynchronousUpdate()
		{
			if (!this.active) return; // Just in case someone call it directly

			this.dict.Clear();

			foreach (AttachNode an in this.part.attachNodes)
			{
				AttachNode ann = new AttachNode();
				ann.id = an.id;
				ann.offset = an.offset;
				ann.orientation = an.orientation;
				ann.position = an.position;
				this.dict.Add(ann.id, ann);
			}
		}

		internal void SynchronousRestore()
		{
			if (!this.active) return; // Just in case someone call it directly

			foreach (AttachNode an in this.part.attachNodes) if (this.dict.ContainsKey(an.id))
			{
				AttachNode ann = this.dict[an.id];
				an.id = ann.id;
				an.offset = ann.offset;
				an.orientation = ann.orientation;
				an.position = ann.position;
			}
		}

		#endregion

		private string PartInstanceId => string.Format("{0}-{1}:{2:X}", this.VesselName, this.part.name, this.part.GetInstanceID());
		private string VesselName => null == this.part.vessel ? "<NO VESSEL>" : this.part.vessel.vesselName ;

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<LetsStayTogether>("KSP-Recall", "LetsStayTogether");
	}
} }
