/*
	This file is part of KSP-Recall
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
using System.Collections;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace KSP_Recall
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class SanityCheck : MonoBehaviour
	{
		private static readonly int WAIT_ROUNDS = 120; // @60fps, would render 2 secs.
		
		internal static bool isConcluded = false;

		private void Start()
		{
			StartCoroutine("SanityCheckCoroutine");
		}

		private IEnumerator SanityCheckCoroutine()
		{
			SanityCheck.isConcluded = false;
			Log.info("SanityCheck: Started");

			{  // Toe Stomping Fest prevention
				for (int i = WAIT_ROUNDS; i >= 0 && null == PartLoader.LoadedPartsList; --i)
				{
					yield return null;
					if (0 == i) Log.warn("Timeout waiting for PartLoader.LoadedPartsList!!");
				}
	
				 // I Don't know if this is needed, but since I don't know that this is not needed,
				 // I choose to be safe than sorry!
				{
					int last_count = int.MinValue;
					for (int i = WAIT_ROUNDS; i >= 0; --i)
					{
						if (last_count == PartLoader.LoadedPartsList.Count) break;
						last_count = PartLoader.LoadedPartsList.Count;
						yield return null;
						if (0 == i) Log.warn("Timeout waiting for PartLoader.LoadedPartsList.Count!!");
					}
				}
			}

			int total_count = 0;
			int parts_with_attachment_count = 0;
			int parts_with_resourceful_count = 0;
			int showstoppers_count = 0;

			foreach (AvailablePart p in PartLoader.LoadedPartsList)
			{
				for (int i = WAIT_ROUNDS; i >= 0 && (null == p.partPrefab || null == p.partPrefab.Modules); --i)
				{
					yield return null;
					if (0 == i) Log.error("Timeout waiting for {0}.prefab.Modules!!", p.name);
				}
			  
				Part prefab;
				{ 
					// Historically, we had problems here.
					// However, that co-routine stunt appears to have solved it.
					// But we will keep this as a ghinea-pig in the case the problem happens again.
					int retries = WAIT_ROUNDS;
					bool containsAttachment = false;
					bool containsResourceful = false;
					Exception culprit = null;
					
					prefab = p.partPrefab; // Reaching the prefab here in the case another Mod recreates it from zero. If such hypothecical mod recreates the whole part, we're doomed no matter what.
					
					while (retries > 0)
					{ 
						bool should_yield = false;
						try 
						{
							containsAttachment = prefab.Modules.Contains("Attachment");
							containsResourceful = prefab.Modules.Contains("Resourceful");
							++total_count;
							break;  // Yeah. This while stunt was done just to be able to do this. All the rest is plain clutter! :D 
						}
						catch (Exception e)
						{
							culprit = e;
							--retries;
							should_yield = true;
						}
						if (should_yield) // This stunt is needed as we can't yield from inside a try-catch!
							yield return null;
					}

					if (0 == retries)
					{
						Log.error("Exception on {0}.prefab.Modules.Contains: {1}", p.name, culprit);
						Log.detail("{0}", prefab.Modules);
						continue;
					}

					try
					{
						if (containsAttachment && this.checkForAttachment(prefab))
						{
							Log.info("Removing Attachment support for {0} ({1}).", p.name, p.title);
							prefab.Modules.Remove(prefab.Modules["Attachment"]);
						}
						else ++parts_with_attachment_count;

						if (containsResourceful && this.checkForResourceful(prefab))
						{
							Log.info("Removing Resourceful support for {0} ({1}).", p.name, p.title);
							prefab.Modules.Remove(prefab.Modules["Resourceful"]);
						}
						else ++parts_with_resourceful_count;
					}
					catch (Exception e)
					{
						++showstoppers_count;
						Log.error("part={0} ({1}) Exception on Sanity Checks: {2}", p.name, p.title, e);
					}
					// End of hack. Ugly, uh? :P
				}
#if DEBUG
				{
					Log.dbg("Found part named {0} ; title {1}:", p.name, p.title);
					foreach (PartModule m in prefab.Modules)
						Log.dbg("\tPart {0} has module {1}", p.name, m.moduleName);
				}
#endif
			}
			Log.info("SanityCheck Concluded : {0} parts found ; {1} parts using Attachment ; {2} parts using Resourceful ; {3} show stoppers detected .", total_count, parts_with_attachment_count, parts_with_resourceful_count, showstoppers_count);
			SanityCheck.isConcluded = true;

			if (showstoppers_count > 0)
			{
				GUI.ShowStopperAlertBox.Show(showstoppers_count);
			}
		}

		private bool checkForAttachment(Part p)
		{
			Log.dbg("Checking Attachment Sanity for {0} at {1}", p.name, p.partInfo.partUrl);

			// if (0 == p.attachNodes.Count) return true; Some AddOn can add an Attach Node later...
			if (p.name.StartsWith("kerbalEVA")) return true;

			return false;
		}

		private bool checkForResourceful(Part p)
		{
			Log.dbg("Checking Resourceful Sanity for {0} at {1}", p.name, p.partInfo.partUrl);

			// if (0 == p.Resources.Count) return true; Some AddOn can add a Resource Node later...
			if (p.name.StartsWith("kerbalEVA")) return true;

			return false;
		}

	}
}
