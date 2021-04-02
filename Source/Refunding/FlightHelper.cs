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
using UnityEngine;

namespace KSP_Recall { namespace Refunds 

{
	[KSPAddon(KSPAddon.Startup.FlightAndKSC, false)]
	public class FlightHelper : MonoBehaviour
	{
		#region Unity Life Cycle

		private void Awake()
		{
			Log.dbg("Awake");
			if (Globals.Instance.Refunding)
			{
				GameEvents.OnVesselRecoveryRequested.Add(OnVesselRecoveryRequested);
				GameEvents.onVesselChange.Add(OnVesselChange);
			}
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy");
			if (Globals.Instance.Refunding)
			{
				GameEvents.onVesselChange.Remove(OnVesselChange);
				GameEvents.OnVesselRecoveryRequested.Remove(OnVesselRecoveryRequested);
			}
		}

		#endregion

		#region Events Handlers

		// Called by obvious reaons. The vessel is being recovered
		private void OnVesselRecoveryRequested(Vessel vessel)
		{
			Log.dbg("OnVesselRecoveryRequested {0}", vessel.vesselName);
			this.ImmediateUpdate(vessel);
		}

		private void OnVesselChange(Vessel vessel)
		{
			Log.dbg("OnVesselChange {0}", vessel.vesselName);
			this.DelayedUpdate(vessel);
		}

		#endregion

		private void ImmediateUpdate(Vessel vessel)
		{
			foreach (Part p in vessel.Parts) if (p.Modules.Contains<Refunding>())
				p.Modules.GetModule<Refunding>().SynchronousFullUpdate();
		}

		private void AsyncUpdate(Vessel vessel)
		{
			foreach (Part p in vessel.Parts) if (p.Modules.Contains<Refunding>())
				p.Modules.GetModule<Refunding>().AsynchronousUpdate();
		}

		// The delay allows the vessel's cost to be billed before the Refund is calculated
		// Preventing overbilling on launch.
		private void DelayedUpdate(Vessel vessel)
		{
			foreach (Part p in vessel.Parts) if (p.Modules.Contains<Refunding>())
				p.Modules.GetModule<Refunding>().AsynchronousUpdate(50);
		}

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<EditorHelper>("KSP-Recall", "Refunding-FlightHelper");
	}
} }