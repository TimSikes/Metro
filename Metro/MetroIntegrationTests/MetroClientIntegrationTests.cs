using System;
using System.Linq;
using MetroClient.Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MetroIntegrationTests
{
	[TestFixture]
	public class UnitTest1
	{
		
		[TestCase("704")]
		[TestCase("705")]
		[TestCase("605")]
		[TestCase("10")]
		public void GetRoutes(string routeId)
		{
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			RouteDto route = client.GetRoute(routeId);
			Assert.IsTrue(route.DisplayName != null);
		}

		[TestCase("704")]
		[TestCase("705")]
		[TestCase("10")]
		public void GetStops(string routeId)
		{
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			StopsDto stops = client.GetStops(routeId);
			Assert.IsTrue(stops.Stops.Any());
			Assert.IsTrue(stops.Stops.First().DisplayName != null);
		}

		[TestCase("704")]
		[TestCase("705")]
		[TestCase("10")]
		public void GetVehicles(string routeId)
		{
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			VehiclesDtos stops = client.GetVehicles(routeId);
			Assert.IsTrue(stops.Vehicles.Any());
		}


		private const string c_metroUri = "http://api.metro.net/agencies/lametro/";
	}
}
