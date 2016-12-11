namespace MetroClient.Models
{
	public sealed class VehicleDto
	{
		public int? Id { get; set; }
		public int? SecondsSinceReport { get; set; }
		public string RunId { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public int? Heading { get; set; }
		public int? RouteId { get; set; }
		public bool? Predictable { get; set; }
	}
}
