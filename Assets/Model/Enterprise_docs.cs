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
	public class Enterprise_docs  //TROUBLE
	{
		public bool Availability { get; set; }
		public bool Is_active { get; set; }
		public DateTime Expiration_date { get; set; }

		public Int64 Document_id { get; set; }
		public Int64 Enterprise_id { get; set; }

		public Document Document { get; set; }
		public Enterprise Enterprise { get; set; }

		public Enterprise_docs (bool availability, bool is_active, DateTime expiration_date,
		                        Int64 document_id, Int64 enterprise_id)
		{
			Availability = availability;
			Is_active = is_active;
			Expiration_date = expiration_date;
			Document_id = document_id;
			Enterprise_id = enterprise_id;
		}
	}
}