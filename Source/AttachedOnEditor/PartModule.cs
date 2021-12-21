/*
	This file is part of Attached, a component of KSP-Recall
		© 2020-2021 Lisias T : http://lisias.net <support@lisias.net>

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

namespace KSP_Recall { namespace AttachedOnEditor
{
	public class AttachedOnEditor : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::AttachedOnEditor")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			this.active = Globals.Instance.AttachedOnEditor;
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
			this.PreserveCurrentRadialAttachments();
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2} {3}", this.name, this.part.GetInstanceID(), state, this.active);
			if (this.active && HighLogic.LoadedSceneIsEditor) this.RestoreCurrentRadialAttachments();
			base.OnStart(state);
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion

		private UnityEngine.Vector3 originalPos;
		private void PreserveCurrentRadialAttachments()
		{
			Log.dbg("PreserveCurrentRadialAttachments from {0} to {1}", this.originalPos, this.part.partTransform.position);
			this.originalPos = this.part.partTransform.position;
		}

		private void RestoreCurrentRadialAttachments()
		{
			Log.dbg("RestoreCurrentRadialAttachments from {0} to {1}", this.part.partTransform.position, this.originalPos);
			this.part.partTransform.position = this.originalPos;
		}

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<AttachedOnEditor>("KSP-Recall", "AttachedOnEditor");
	}
} }