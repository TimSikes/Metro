namespace MetroClient.Models
{
	public sealed class PredictionDto
	{
		public string BlockId { get; set; }
		public string RunId { get; set; }
		public int? RouteId { get; set; }
		public int? Seconds { get; set; }
		public int? Minutes { get; set; }
		public bool? IsDeparting { get; set; }
	}
}
