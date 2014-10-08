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
using System.Collections.Generic;

namespace Model
{
	public class Service
	{	
		public Int64 Id { get; set; }
		public string Title { get; set; } 
		public decimal Price { get; set; } 
		public int Period { get; set; }
		public decimal Effectiveness { get; set; }

		public Int64 Asset_id { get; set; }
		public Int64 Company_id { get; set; }

		public virtual Asset Asset { get; set; }
		public virtual Company Company { get; set; }

		public Service(Int64 id, string title, decimal price, int period, 
		               decimal effectiveness, Int64 asset_id, Int64 company_id)
		{
			Id = id;
			Title = title;
			Price = price;
			Period = period;
			Asset_id = asset_id;
			Company_id = company_id;
			Effectiveness = effectiveness;
		}
	}
}