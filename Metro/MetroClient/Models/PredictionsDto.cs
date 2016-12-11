using System.Collections.Generic;

namespace MetroClient.Models
{
	public sealed class PredictionsDto
	{
		public IEnumerable<PredictionDto> Predictions { get; set; }
	}
}
