using System;
using Newtonsoft.Json.Linq;
using static System.Int32;

namespace MetroUtility
{
	public static class ConversionUtility
	{
		public static bool? ToBool(string value)
		{
			return value == "true";
		}

		public static int? ParseInt(string value)
		{
			return Parse(value);
		}

		public static double? ParseDouble(string value)
		{
			return double.Parse(value);
		}

		public static TOne? GetValueOrNull<TOne, TTwo>(TTwo token, Func<string, TOne?> parser)
			where TOne : struct
		{
			if (token == null)
				return null;

			return parser(token.ToString());
		}

		public static string GetValueOrNull(JToken token)
		{
			return token?.ToString();
		}
	}
}
