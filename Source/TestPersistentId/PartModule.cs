/*
	This file is part of TestPersistentId, a component of KSP-Recall
		© 2020-2023 LisiasT : http://lisias.net <support@lisias.net>

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

namespace KSP_Recall.Test.PersistentId 
{
	public class TestPersistentId : PartModule
	{
		#region KSP UI

		#endregion


		#region KSP Life Cycle

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}:{1:X}", this.name, this.part.GetInstanceID());
			this.doLog("before OnAwake");
			base.OnAwake();
			this.doLog("after OnAwake");
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), state);
			this.doLog("before OnStart");
			base.OnStart(state);
			this.doLog("after OnStart");
		}

		public override void OnCopy(PartModule fromModule)
		{
			Log.dbg("OnCopy {0}:{1:X} from {2:X}", this.name, this.part.GetInstanceID(), fromModule.part.GetInstanceID());
			this.doLog("before OnCopy");
			base.OnCopy(fromModule);
			this.doLog("after OnCopy");
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			this.doLog("before OnLoad");
			base.OnLoad(node);
			this.doLog("after OnLoad");
		}

		public override void OnSave(ConfigNode node)
		{
			Log.dbg("OnSave {0}:{1:X} {2}", this.name, this.part.GetInstanceID(), null != node);
			this.doLog("before OnSave");
			base.OnSave(node);
			this.doLog("after OnSave");
		}

		#endregion

		#region Unity Life Cycle

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}:{1:X}", this.name, this.part.GetInstanceID());
			this.doLog("OnDestroy");
		}

		#endregion

		private void doLog(string situation)
		{
			Log.info("Part {0}:{1:X} has persistentId={2} at {3}", this.name, this.part.GetInstanceID(), this.part.persistentId, situation);
		}

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<TestPersistentId>("KSP-Recall", "Test.PersistentId", 0);
	}
}
