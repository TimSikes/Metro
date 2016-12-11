using System.Web.Http;

namespace Metro.Controllers.Data
{
	public class DataController : ApiController
	{
		public DataController(MetroClient.MetroClient metroClient)
		{
			MetroClient.MetroClient m_metroClient = metroClient;
		}

		private readonly MetroClient.MetroClient m_metroClient;
	}
}
