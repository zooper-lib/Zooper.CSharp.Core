using System;

namespace Zooper.CSharp.Core.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// <para>An Implementation of the Levenshteins distance comparison.</para>
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="source">Source.</param>
		/// <param name="target">Target.</param>
		public static int LevenshteinDistance(this string source, string target)
		{
			// degenerate cases
			if (source == target) return 0;
			if (source.Length == 0) return target.Length;
			if (target.Length == 0) return source.Length;

			// create two work vectors of integer distances
			var v0 = new int[target.Length + 1];
			var v1 = new int[target.Length + 1];

			// initialize v0 (the previous row of distances)
			// this row is A[0][i]: edit distance for an empty s
			// the distance is just the number of characters to delete from t
			for (var i = 0; i < v0.Length; i++)
				v0[i] = i;

			for (var i = 0; i < source.Length; i++)
			{
				// calculate v1 (current row distances) from the previous row v0

				// first element of v1 is A[i+1][0]
				//   edit distance is delete (i+1) chars from s to match empty t
				v1[0] = i + 1;

				// use formula to fill in the rest of the row
				for (var j = 0; j < target.Length; j++)
				{
					var cost = (source[i] == target[j]) ? 0 : 1;
					v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
				}

				// copy v1 (current row) to v0 (previous row) for next iteration
				for (var j = 0; j < v0.Length; j++)
					v0[j] = v1[j];
			}

			return v1[target.Length];
		}

		/// <summary>
		/// Calculate percentage similarity of two strings
		/// <param name="source">Source String to Compare with</param>
		/// <param name="target">Targeted String to Compare</param>
		/// <param name="ignoreCase">Indicates if the cases are ignored</param>
		/// <returns>Return Similarity between two strings from 0 to 1.0</returns>
		/// </summary>
		public static double EqualsInPercent(this string source, string target, bool ignoreCase)
		{
			if (source.Length == 0 || target.Length == 0)
			{
				return 0.0;
			}

			if (ignoreCase)
			{
				source = source.ToUpper();
				target = target.ToUpper();
			}

			if (source == target) return 1.0;

			var stepsToSame = LevenshteinDistance(source, target);
			return (1.0 - ((double)stepsToSame / Math.Max(source.Length, target.Length)));
		}

		/// <summary>
		/// Checks if the given string is identical to the target string to a minimum percentage
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="minPercent"></param>
		/// <param name="ignoreCase"></param>
		/// <returns></returns>
		public static bool IsEqualsInPercent(this string source, string target, double minPercent, bool ignoreCase)
		{
			var p = EqualsInPercent(source, target, ignoreCase);

			return p >= minPercent;
		}
	}
}