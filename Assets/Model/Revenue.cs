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

namespace Model
{
	public class Revenue
	{
		public DateTime Revenue_date { get; set; } 
		public decimal Value { get; set; }

		public Int64 Enterprise_id { get; set; }

		public Revenue(DateTime revenue_date, decimal value, Int64 enterprise_id)
		{
			Revenue_date = revenue_date;
			Value = value;
			Enterprise_id = enterprise_id;
		}
	}
}