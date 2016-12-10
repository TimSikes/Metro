using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MetroClient
{
	public abstract class WebServiceClient
	{
		protected WebServiceClient(Uri baseUri)
		{
			m_baseUri = baseUri;
		}

		protected HttpWebResponse GetResponse(string path)
		{
			HttpWebRequest webRequest = WebRequest.Create(m_baseUri + path) as HttpWebRequest;

			return webRequest.GetResponse() as HttpWebResponse;
		}

		protected JObject GetJson(string path)
		{
			string responseFromServer;
			HttpWebRequest webRequest = WebRequest.Create(m_baseUri + path) as HttpWebRequest;

			using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
			{
				Stream responseStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseStream);
				responseFromServer = reader.ReadLine();
			}

			JObject route = JObject.Parse(responseFromServer);
			return route;
		}

		protected JObject GetJson(HttpWebResponse response)
		{
			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);
			string responseFromServer = reader.ReadLine();

			return JObject.Parse(responseFromServer);
		}

		private readonly Uri m_baseUri;
	}
}
