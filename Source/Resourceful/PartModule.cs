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

namespace KSP_Recall

{
	public class Resourceful : PartModule
	{
		[KSPField(isPersistant = false, guiActiveEditor = true, guiName = "KSP-Recall::Resourceful")]//Scale
		[UI_Toggle(disabledText = "Disabled", enabledText = "Enabled", scene = UI_Scene.Editor)]
		public bool active = false;

		#region KSP Events

		public override void OnAwake()
		{
			Log.dbg("OnAwake {0}", this.name);
		}

		public override void OnStart(StartState state)
		{
			Log.dbg("OnStart {0} {1}", this.name, this.active);
		}

		public override void OnLoad(ConfigNode node)
		{
			Log.dbg("OnLoad {0} {1}", this.name, null != node);
		}

		#endregion


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
