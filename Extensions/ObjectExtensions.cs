using System.Linq;

namespace Zooper.CSharp.Core.Extensions
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Indicates if any property is null
		/// </summary>
		/// <param name="myObject"></param>
		/// <returns></returns>
		public static bool IsAnyPropertyNull(this object myObject)
		{
			return (from propertyInfo in myObject.GetType().GetProperties()
				select propertyInfo.GetValue(myObject)).Any(s => s == null);
		}
	}
}
