/*
	This file is part of Resourceful, a component of KSP-Recall
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
using System.Collections.Generic;
using UnityEngine;

namespace KSP_Recall { namespace Refunds 

{
	[KSPAddon(KSPAddon.Startup.FlightAndKSC, false)]
	public class EditorHelper : MonoBehaviour
	{
		private readonly bool IsOnKSP11 = 1 == KSPe.Util.KSP.Version.Current.MAJOR && 11 == KSPe.Util.KSP.Version.Current.MINOR;

		#region Unity Life Cycle

		private void Awake()
		{
			Log.dbg("Awake {0}", this.name);
			if (this.IsOnKSP11) GameEvents.OnVesselRecoveryRequested.Add(OnVesselRecoveryRequested);
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}", this.name);
			if (this.IsOnKSP11) GameEvents.OnVesselRecoveryRequested.Remove(OnVesselRecoveryRequested);
		}

		#endregion

		#region Events Handlers

		private void OnVesselRecoveryRequested(Vessel data)
		{
			Log.dbg("OnVesselRecoveryRequested {0}", data.name);
			foreach (Part p in data.Parts) if (p.Modules.Contains<Refunding>())
				p.Modules.GetModule<Refunding>().SynchronousFullUpdate();
				
		}

		#endregion
		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<EditorHelper>("KSP-Recall", "Refunding-FlightHelper");
	}
} }