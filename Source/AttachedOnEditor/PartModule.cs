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

		private bool isCopy = false;

		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			this.active = Globals.Instance.AttachedOnEditor;
			this.isCopy = false;
		}

		public override void OnCopy(PartModule fromModule)
		{
			Log.dbg("OnCopy {0}:{1:X} from {2:X}", this.name, this.part.GetInstanceID(), fromModule.part.GetInstanceID());
			this.isCopy = true;
			base.OnCopy(fromModule);
			this.RememberOriginalModule(fromModule);
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
			Log.dbg("PreserveCurrentRadialAttachments {0}:{1:X} from {2} to {3}", this.name, this.part.GetInstanceID(), this.originalPos, this.part.partTransform.position);
			this.originalPos = this.part.partTransform.position;
		}

		private void RestoreCurrentRadialAttachments()
		{
			if (UnityEngine.Vector3.zero == this.originalPos) return; // hack to prevent the UpgradePipeline to screw us up when loading crafts still without AttachedOnEditor
			Log.dbg("RestoreCurrentRadialAttachments {0}:{1:X} from {2} to {3}", this.name, this.part.GetInstanceID(), this.part.partTransform.position, this.originalPos);

			// That's thing thing: Copies from Radial Symmetries are fine (believe it if you can)
			// We are having problems with Mirror Symmetry copies and with "original" parts only...
			if (this.isCopy && SymmetryMethod.Radial == EditorLogic.fetch.symmetryMethod && 0 != this.part.symmetryCounterparts.Count) return;

			this.part.partTransform.position = this.originalPos;
		}

		private void RememberOriginalModule(PartModule originalModule)
		{
			Log.dbg("RememberOriginalModule {0}:{1:X} from {2} to {3} using {4}", this.name, this.part.GetInstanceID(), this.originalPos, originalModule.part.partTransform.position, EditorLogic.fetch.symmetryMethod);
			UnityEngine.Vector3 pos = originalModule.part.partTransform.position;
			this.originalPos = pos;
		}

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<AttachedOnEditor>("KSP-Recall", "AttachedOnEditor");
	}
} }