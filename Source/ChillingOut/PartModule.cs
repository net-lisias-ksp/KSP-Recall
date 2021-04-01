/*
	This file is part of ChillingOut, a component of KSP-Recall
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
using UnityEngine;

namespace KSP_Recall { namespace ChillingOut
{
	public class ChillingOut : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "KSP-Recall::ChillingOut")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Flight)]
		public bool active = false;

		#endregion


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnAwake();
			this.active = Globals.Instance.ChillingOut;
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
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnSave(node);
		}

		public override void OnInitialize()
		{
			Log.dbg("OnInitialize {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnInitialize();
			this.init();
		}

		public override void OnActive()
		{
			Log.dbg("OnActive {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnActive();
			this.init();
		}

		// Needed because I had overriden OnActive.
		// See https://kerbalspaceprogram.com/api/class_part_module.html#a6f2dd76038326c527e64d2ce96bb45fe
		public override bool IsStageable()
		{
			return false;
		}

		public override void OnInactive()
		{
			Log.dbg("OnInactive {0}:{1:X}", this.name, this.part.GetInstanceID());
			base.OnInactive();
			this.deinit();
		}

		#endregion


		#region Unity Life Cycle

		private void FixedUpdate()
		{
			if (!HighLogic.LoadedSceneIsFlight) return;
			if (this.vessel.missionTime > DELTA) this.enabled = false; // We are not needed anymore

			this.part.temperature = 0;
			this.part.skinTemperature = 0;
			this.part.skinUnexposedTemperature = 0;
			this.part.skinUnexposedExternalTemp = 0;
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion


		private const float DELTA = 1.0f;	// 1 second
		private void init()
		{
		}

		private void deinit()
		{
		}

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<ChillingOut>("KSP-Recall", "ChillingOut");
	}
} }