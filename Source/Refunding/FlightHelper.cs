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
				GameEvents.onVesselSwitching.Add(OnVesselSwitching);
				GameEvents.onVesselSwitchingToUnloaded.Add(OnVesselSwitching);
				GameEvents.onVesselGoOnRails.Add(OnVesselGoOnRails);
				GameEvents.onVesselGoOffRails.Add(OnVesselGoOffRails);
			}
		}

		private void OnDestroy()
		{
			Log.dbg("OnDestroy");
			if (Globals.Instance.Refunding)
			{
				GameEvents.onVesselGoOffRails.Add(OnVesselGoOffRails);
				GameEvents.onVesselGoOnRails.Add(OnVesselGoOnRails);
				GameEvents.onVesselSwitchingToUnloaded.Remove(OnVesselSwitching);
				GameEvents.onVesselSwitching.Remove(OnVesselSwitching);
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

		// This is called when the vessel is switched IN, including on launch
		// So this is the point where we trigger a (delayed) updated of the
		// Refunding Resource.
		private void OnVesselChange(Vessel vessel)
		{
			Log.dbg("OnVesselChange {0}", vessel.vesselName);
			this.DelayedUpdate(vessel);
		}

		// Probably not needed, here for testing purposes
		private void OnVesselSwitching(Vessel from, Vessel to)
		{
			Log.dbg("OnVesselSwitching from {0} to {1}", from.vesselName, to.vesselName);
			this.ImmediateUpdate(from);
		}

		// That's the history: I'm trying to force my hand on Parts with ModuleCargoPart that are stackable.
		// The presence of the Refunding Resource make these parts unstackable, as stackable parts must
		// be stateless (and resources add state to the part).
		//
		// So, while going on rails, we add the Refunding Resource so if the vessel is recovered while packed,
		// the costs are correctly recovered.
		private void OnVesselGoOnRails(Vessel vessel)
		{
			Log.dbg("OnVesselGo ON Rails {0}", vessel.vesselName);
			this.ImmediateUpdate(vessel);
		}

		// Continuing from the the OnRails stunt, when the vessel is unpacked we need to remove the Resource from
		// the stackable parts, so they could be stackable again.
		//
		// Since vessels are recovered while flying or while packed, I think this stunt my stick...
		private void OnVesselGoOffRails(Vessel vessel)
		{
			Log.dbg("OnVesselGo OFF Rails {0}", vessel.vesselName);
			this.RemoveResourceWhenNeeded(vessel);
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

		private void RemoveResourceWhenNeeded(Vessel vessel)
		{
			foreach (Part p in vessel.Parts) if (p.Modules.Contains<Refunding>())
				p.Modules.GetModule<Refunding>().RemoveResourceWhenNeeded();
		}

		private static KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<EditorHelper>("KSP-Recall", "Refunding-FlightHelper");
	}
} }