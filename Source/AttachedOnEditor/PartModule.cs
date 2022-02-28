/*
	This file is part of Attached, a component of KSP-Recall
		© 2020-2022 Lisias T : http://lisias.net <support@lisias.net>

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
using System.Collections.Generic;
using System.Diagnostics;

namespace KSP_Recall { namespace AttachedOnEditor
{
	public class AttachedOnEditor : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::AttachedOnEditor")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion


		#region PersistentData

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = false)]
		private UnityEngine.Vector3 originalPos;

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = false)]
		private UnityEngine.Quaternion originalRotation;

		[System.Obsolete("AttachedOnEditor.correctlyInitialised is deprecated and will be removed soon.")]
		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = false)]
		private bool correctlyInitialised = false;

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = false)]
		private int moduleVersion = 0;

		private List<UnityEngine.Vector3> originalAttachNodePos = new List<UnityEngine.Vector3>();

		#endregion

		private const int MODULE_VERSION = 4;
		private bool isCopy = false;

		private bool initialised => MODULE_VERSION == this.moduleVersion;

		#region KSP Life Cycle

		public override void OnAwake()
		{
			this.part.GetInstanceID();	// Force generating the InstanceId
			Log.dbg("OnAwake {0}", this.PartInstanceId);
			base.OnAwake();
			this.active = Globals.Instance.AttachedOnEditor;
			this.isCopy = false;
			this.ActivateMe();
		}

		public override void OnCopy(PartModule fromModule)
		{
			Log.dbg("OnCopy {0} from {1:X}", this.PartInstanceId, fromModule.part.GetInstanceID());
			base.OnCopy(fromModule);

			this.isCopy = true;
			this.ActivateMe();
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0} {1}", this.PartInstanceId, null != node);
			base.OnLoad(node);
			if (this.correctlyInitialised && this.moduleVersion < MODULE_VERSION)
			{	// Salvage any previsouly saved values
				Log.dbg("Older version {0} detected. Migrating to {1}", this.moduleVersion, MODULE_VERSION);
				if (this.moduleVersion < MODULE_VERSION-1) this.PreserveCurrentAttachmentNodes();
				if (this.moduleVersion < MODULE_VERSION) this.originalRotation = this.part.partTransform.rotation;
				this.moduleVersion = MODULE_VERSION;
			}

			if (this.isMerge() || this.isSubAssembly())
			{
				Log.dbg("OnLoad under merge/subassembly.");
				//return;
			}

			if (!this.initialised) this.PreserveAttachments();
			this.LoadFrom(node);
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0} {1}", this.PartInstanceId, null != node);
			base.OnSave(node);
			if (HighLogic.LoadedSceneIsEditor)
				this.PreserveAttachments();		// Updates the values, in case anyone else had changed it!
												// But only on Editor, otherwise we would screw up crafts under stress on Flight!!!
			this.SaveTo(node);
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0} {1} {2}", this.PartInstanceId, state, this.active);
			if (this.active && HighLogic.LoadedSceneIsEditor) this.RestoreAttachments();
			base.OnStart(state);
		}

		#endregion


		#region Unity Life Cycle

		private void Update()
		{
			this.enabled = false;
			if (!this.active) return;

			switch(HighLogic.LoadedScene)
			{
				case GameScenes.FLIGHT:
					this.LogCurrentAttachmentNodes();
					break;
				case GameScenes.EDITOR:
					this.LogCurrentAttachmentNodes();
					this.RestoreAttachments();
					break;
				default:
					break;
			}
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}", this.part.partName);	// Get Instance ID borks on OnDestroy...
		}

		#endregion


		private void ActivateMe()
		{
			this.active = Globals.Instance.AttachedOnEditor;
		}

		private void PreserveCurrentAttachmentNodes()
		{
			Log.dbg("PreserveCurrentAttachmentPoints for {0} hasAttachNodes? {1}", this.PartInstanceId, null != this.part.attachNodes);

			this.originalAttachNodePos.Clear();
			for (int i = 0; i < this.part.attachNodes.Count; ++i)
			{
				AttachNode an = this.part.attachNodes[i];
				this.originalAttachNodePos.Insert(i, an.position);
			}
		}

		private void PreserveCurrentAttachments()
		{
			Log.dbg("PreserveCurrentAttachments {0} from {1} to {2}", this.PartInstanceId, this.originalPos, this.part.partTransform.position);
			this.originalPos = this.part.partTransform.position;
			this.originalRotation = this.part.partTransform.rotation;
		}

		private void PreserveAttachments()
		{
			this.PreserveCurrentAttachmentNodes();
			this.PreserveCurrentAttachments();
			this.moduleVersion = MODULE_VERSION;
		}

		private void RestoreCurrentAttachmentNodes()
		{
			if (!this.initialised) return; // hack to prevent the UpgradePipeline to screw us up when loading crafts still without AttachedOnEditor
			Log.dbg("RestoreCurrentAttachmentPoints for {0}", this.PartInstanceId);
			for(int i = 0; i < this.originalAttachNodePos.Count; ++i)
				this.part.attachNodes[i].position = this.originalAttachNodePos[i];
		}

		private void RestoreCurrentAttachments()
		{
			Log.dbg("RestoreCurrentAttachments {0} from {1} to {2}", this.PartInstanceId, this.part.partTransform.position, this.originalPos);

			if (this.isCopy) return;

			this.part.partTransform.position = this.originalPos;
			this.part.partTransform.rotation = this.originalRotation;
		}

		private void RestoreAttachments()
		{
			if (!this.initialised) return; // hack to prevent the UpgradePipeline to screw us up when loading crafts still without AttachedOnEditor
			this.RestoreCurrentAttachmentNodes();
			this.RestoreCurrentAttachments();
		}

		private bool isMerge() {
			// Void KSP.UI.Screens.CraftBrowserDialog.onButtonMerge()
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
			foreach (System.Diagnostics.StackFrame frame in st.GetFrames())
			{
				string classname = frame.GetMethod().DeclaringType.Name;
				string methodname = frame.GetMethod().ToString();
				//Log.dbg("isMerge {0} {1}", classname, methodname);
				if ("CraftBrowserDialog".Equals(classname) && "Void onButtonMerge()".Equals(methodname))
					return true;
			}
			return false;
		}

		private bool isSubAssembly() {
			// Void ShipConstruction.CreateConstructFromTemplate(ShipTemplate, Callback`1[ShipConstruct])
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
			foreach (System.Diagnostics.StackFrame frame in st.GetFrames())
			{
				string classname = frame.GetMethod().DeclaringType.Name;
				string methodname = frame.GetMethod().ToString();
				//Log.dbg("isSubAssembly {0} {1}", classname, methodname);
				if ("ShipConstruction".Equals(classname) && "Void CreateConstructFromTemplate(ShipTemplate, Callback`1[ShipConstruct])".Equals(methodname))
					return true;
			}
			return false;
		}

		private void LoadFrom(ConfigNode node)
		{
			string[] list = node.GetValues("originalAttachNodePos");
			for(int i = 0; i < list.Length; ++i)
			{
				UnityEngine.Vector3 v = this.parseVector3(list[i]);
				this.originalAttachNodePos.Insert(i, v);
			}
		}

		private UnityEngine.Vector3 parseVector3(string vector3)
		{
			if ('(' != vector3[0] || ')' != vector3[vector3.Length-1]) throw new InvalidCastException(String.Format("{0} is not a UnityEngine.Vector3!", vector3));
			string v = vector3.Substring(1, vector3.Length-2);
			string[] l = v.Split(',');
			return new UnityEngine.Vector3(float.Parse(l[0]), float.Parse(l[1]), float.Parse(l[2]));
		}

		private void SaveTo(ConfigNode node)
		{
			for(int i = 0; i < this.originalAttachNodePos.Count; ++i)
				node.AddValue("originalAttachNodePos", this.originalAttachNodePos[i].ToString());
		}

		[ConditionalAttribute("DEBUG")]
		private void LogCurrentAttachmentNodes()
		{
			Log.dbg("LogCurrentAttachmentNodes for {0} hasAttachNodes? {1}", this.PartInstanceId, null != this.part.attachNodes);

			for (int i = 0; i < this.part.attachNodes.Count; ++i)
			{
				AttachNode an = this.part.attachNodes[i];
				Log.dbg("\t{0} {1} {2} nodeTransform? {3}", i, an.attachedPartId, an.position, null != an.nodeTransform);
			}
		}

		private string PartInstanceId => string.Format("{0}-{1}:{2:X}", this.VesselName, this.part.name, this.part.GetInstanceID());
		private string VesselName => null == this.part.vessel ? "<NO VESSEL>" : this.part.vessel.vesselName ;

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<AttachedOnEditor>("KSP-Recall", "AttachedOnEditor", 0);
	}
} }