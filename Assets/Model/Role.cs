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
	public class Role
	{
		public Int64 Id { get; set; }
		public string Title { get; set; } //We must change it so it's not equal to the classname
		public decimal Min_salary { get; set; }
		public decimal Max_salary { get; set; }

		public virtual ICollection<Employee> Employees {get; set;}
		public virtual ICollection<Human_resources> Human_resources {get; set;}

		public Role(Int64 id, string title, decimal min_salary, decimal max_salary)
		{
			Id = id;
			Title = title;
			Min_salary = min_salary;
			Max_salary = max_salary;
		}
	}
}