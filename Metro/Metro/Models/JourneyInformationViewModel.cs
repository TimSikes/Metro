namespace Metro.Models
{
	public sealed class JourneyInformationViewModel
	{
		public string NextBusMessage { get; set;}
		public string OtherBusTimes { get; set; }
		public WarningViewModel Message { get; set; }
		public int TravelTimeMinutes { get; set; }
		public string DepartureTitle { get; set; }
		public string ArrivalTitle { get; set; }
		public DepartureAndArrivalViewModel DepartureAndArrivalViewModel { get; set; }
		public ErrorViewModel Error { get; set; }
		public RouteViewModel RouteViewModel { get; set; }
	}
}
