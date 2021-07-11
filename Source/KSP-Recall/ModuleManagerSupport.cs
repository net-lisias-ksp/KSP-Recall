/*
	This file is part of KSP-Recall
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

namespace KSP_Recall
{
	public static class ModuleManagerSupport
	{
		public static IEnumerable<string> ModuleManagerAddToModList()
		{
			List<string> tags = new List<string>();

			if (checkForResourceful())	tags.Add("KSPRECALL-RESOURCEFUL");
			if (checkForDriftless())	tags.Add("KSPRECALL-DRIFTLESS");
			if (checkForAttached())		tags.Add("KSPRECALL-ATTACHED");
			if (checkForChillingOut())	tags.Add("KSPRECALL-CHILLINGOUT");
			if (checkForRefunding())	tags.Add("KSPRECALL-REFUNDING");

			return tags.ToArray();
		}

		private static bool checkForResourceful()
		{
			return (1 == KSPe.Util.KSP.Version.Current.MAJOR && 9 == KSPe.Util.KSP.Version.Current.MINOR);
		}

		private static bool checkForDriftless()
		{
			return (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,8,0))
				&&
				(KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.FindByVersion(1,11,0))
			;
		}

		private static bool checkForAttached()
		{
			//return (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,9,0));

			// Gave up on tackling this down on Recall. I didn't managed to zero in the exact point in which
			// the attach nodes are being reverted to prefab on the KSP 1.11.x VAB/SPH, so I decided to
			// brute force my way (again) on TweakScale - a less than ideal solution, as I was hoping to
			// implement something that could be resusable by third-parties too. :(
			// The not so bad news is that the change doesn't breaks the add'ons on previous KSP versions, so...
			return false; 
		}

		private static bool checkForChillingOut()
		{
			return (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,11,0));
		}

		private static bool checkForRefunding()
		{
			return KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,11,0); 
		}
	}
}
