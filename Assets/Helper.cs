//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace AssemblyCSharp
{
	public static class Helper
	{
		public static T? GetValueOrNull<T>(string valueAsString)
			where T : struct 
		{
			if (string.IsNullOrEmpty(valueAsString))
			return null;
			return (T) Convert.ChangeType(valueAsString, typeof(T));
		}

		public static string ToMySQLDateTimeFormat(DateTime date)
		{
			string formattedDate = date.Year + "-";
			formattedDate += date.Month.ToString().Length > 1 ? (date.Month + "-") : ("0" + date.Month + "-");
			formattedDate += date.Day.ToString().Length > 1 ? (date.Day.ToString()) : ("0" + date.Day);
			return formattedDate;
		}

		public static string ReplaceQueryVoidWithNulls(string expression)
		{
			expression = Regex.Replace (expression, @"(\()\s*(,\s*)", @"(null,");
			expression = Regex.Replace (expression, @"\s*(,\s*)(\))", @",null)");
			expression = Regex.Replace (expression, @"(,\s*)(,\s*)", @",null,");
			return expression;
		}

	}
}

