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
	public class Equipment
	{
		public Int64 Id { get; set; }
		public double Title { get; set; }
		public decimal Price { get; set; }

		public virtual ICollection<Enterprise_equipment> Enterprise_equipments {get; set;}
		public virtual ICollection<Purchase> Purchases {get; set;}

		public Equipment (Int64 id, double title, decimal price)
		{
			Id = id;
			Title = title;
			Price = price;
		}
	}
}