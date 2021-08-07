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
using System;
using KSPe;

namespace KSP_Recall
{
	public class Globals
	{
		private static Globals INSTANCE = null;

		public static Globals Instance => INSTANCE ?? (INSTANCE = new Globals());

		public readonly bool Attached;
		public readonly bool ChillingOut;
		public readonly bool Driftless;
		public readonly bool Refunding;
		public readonly bool Resourceful;
		public readonly bool LetsStayTogether;

		private Globals()
		{
			try
			{
				UrlDir.UrlConfig urlc = GameDatabase.Instance.GetConfigs("KSP-Recall")[0];
				ConfigNodeWithSteroids cn = ConfigNodeWithSteroids.from(urlc.config.GetNode("INSTALLED"));

				this.Attached = cn.GetValue<bool>("Attached");
				this.ChillingOut = cn.GetValue<bool>("ChillingOut");
				this.Driftless = cn.GetValue<bool>("Driftless");
				this.Refunding = cn.GetValue<bool>("Refunding");
				this.Resourceful = cn.GetValue<bool>("Resourceful");
				this.LetsStayTogether = cn.GetValue<bool>("LetsStayTogether");
			}
			catch (Exception)
			{
				this.Attached = false;
				this.ChillingOut = false;
				this.Driftless = false;
				this.Refunding = false;
				this.Resourceful = false;
				this.LetsStayTogether = false;
			}
		}
	}
}
