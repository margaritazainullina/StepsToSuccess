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
	public class Enterprise_equipment
	{
		public DateTime Purchase_date { get; set; }
		public int? Quantity { get; set; }
		public int? Lease_term { get; set; }
		public bool? IsRunning { get; set; }

		public Int64 Enterprise_id { get; set; }
		public Int64 Equipment_id { get; set; }

		public Enterprise Enterprise { get; set; }
		public Equipment Equipment { get; set; }
		
		public Enterprise_equipment (DateTime purchase_date, int? quantity, int? lease_term, 
		                             bool? isRunning, Int64 enterprise_id, Int64 equipment_id)
		{
			Purchase_date = purchase_date;
			Quantity = quantity;
			Lease_term = lease_term;
			IsRunning = isRunning;
			Enterprise_id = enterprise_id;
			Equipment_id = equipment_id;
		}
	}
}

