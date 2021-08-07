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
using UnityEngine;

namespace KSP_Recall { namespace StayingTogether 
{
	public class LetsStayTogether : PartModule
	{
		#region KSP UI

		[KSPField(isPersistant = true, guiActive = false, guiActiveEditor = true, guiName = "KSP-Recall::LetsStayTogether")]
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#endregion

		Vector3? attPos;
		Quaternion? attRotation;

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

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			base.OnLoad(node);
			this.attPos = null;
			this.attRotation = null;
		}

		#endregion

		#region Unity Life Cycle

		private int delayTicks = 0;
		private void FixedUpdate()
		{
			if (!this.active) return;
			Log.dbg("FixedUpdate {0}", this.PartInstanceId);

			switch(HighLogic.LoadedScene)
			{
				case GameScenes.FLIGHT:
					this.SynchronousRestore();
					break;
				default:
					break;
			}

			this.enabled = --this.delayTicks > 0;
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
		}

		#endregion

		#region Part Events Handlers

		internal void SynchronousUpdate()
		{
			if (!this.active) return; // Just in case someone call it directly

			{
				Vector3 v = this.part.transform.position;
				this.attPos = new Vector3(v.x, v.y, v.z);
			}
			{
				Quaternion q = this.part.transform.rotation;
				this.attRotation = new Quaternion(q.x, q.y, q.z, q.w);
			}
		}

		internal void AsynchronousRestore()
		{
			this.delayTicks = 2;
			this.enabled = this.active;
		}

		internal void SynchronousRestore()
		{
			if (!this.active ) return; // Just in case someone call it directly
			if (null == this.attPos || null == this.attRotation) return;

			Log.dbg("Restore {0}'s attPos from {1}:{2} to the restore point {3}:{3}", this.PartInstanceId, this.part.transform.position, this.part.transform.rotation, this.attPos, this.attRotation);
			this.part.transform.SetPositionAndRotation((Vector3)this.attPos, (Quaternion)this.attRotation);

			//if (null != this.attPos && !this.attPos.Equals(this.part.transform.position))
			//{ 
			//	Vector3 position = this.part.transform.position;
			//	position.x = (float)(this.attPos?.x);
			//	position.y = (float)(this.attPos?.y);
			//	position.z = (float)(this.attPos?.z);
			//}

			//if (null != this.attRotation && !this.attRotation.Equals(this.part.transform.rotation))
			//{
			//	Quaternion rotation = this.part.transform.rotation;
			//	Log.dbg("Restore {0}'s attRotation from {1} to the restore point {2}", PartInstanceId, rotation, this.attRotation);
			//	rotation.x = (float)(this.attRotation?.x);
			//	rotation.y = (float)(this.attRotation?.y);
			//	rotation.z = (float)(this.attRotation?.z);
			//	rotation.w = (float)(this.attRotation?.w);
			//}
		}

		#endregion

		private string PartInstanceId => string.Format("{0}-{1}:{2:X}", this.VesselName, this.part.name, this.part.GetInstanceID());
		private string VesselName => null == this.part.vessel ? "<NO VESSEL>" : this.part.vessel.vesselName ;

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<LetsStayTogether>("KSP-Recall", "LetsStayTogether");
	}
} }
