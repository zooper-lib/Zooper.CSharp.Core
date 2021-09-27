using System.Linq;

namespace Zooper.CSharp.Core.Extensions
{
	public static class StringArrayExtensions
	{
		public static bool ContainsInPercent(this string[] source, string target, double minPercentage, bool ignoreCase)
		{
			return source.Any(val => val.IsEqualsInPercent(target, minPercentage, ignoreCase));
		}
	}
}