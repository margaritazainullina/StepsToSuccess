//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
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
	public class Salary_payment
	{
		public Int64 Id { get; set; }

		public DateTime Date { get; set; }
		public int? Hours_worked { get; set; }
		public double? Salary { get; set; }

		public Int64 Employee_id { get; set; }

		public virtual Employee Employee { get; set; }
		
		public Salary_payment(Int64 id, DateTime date, int? hours_worked, double? salary,
		                      Int64 employee_id)
		{
			Id = id;
			Date = date;
			Hours_worked = hours_worked;
			Salary = salary;
			Employee_id = employee_id;
		}
	}
}