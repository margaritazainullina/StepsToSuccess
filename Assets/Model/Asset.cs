﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//ncjbnf
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Model
{
	public class Asset 
	{	//MAKE dao CLASS STATIC //ENTERPRISE TABLE CONNECTION!
		public Int64 Id { get; set; }
		public decimal Value { get; set; }
		public DateTime Asset_date { get; set; }

		public Int64 Enterprise_id { get; set; }

		public Enterprise Enterprise { get; set; }

		//1 to 1 connection, this is the main table for:
		public virtual Purchase Purchase {get; set;}
		public virtual Service Service {get; set;} 

		public Asset (Int64 id, decimal value, DateTime asset_date, Int64 enterprise_id)
		{
			Id = id;
			Value = value;
			Asset_date = asset_date;
			Enterprise_id = enterprise_id;
		}
	}
}