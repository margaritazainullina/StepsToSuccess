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
	public class Project_stage
	{
		
		public Int64 Project_id { get; set; }

		public int? Conception_hours { get; set; }
		public int? Programming_hours { get; set; }
		public int? Testing_hours { get; set; }
		public int? Design_hours { get; set; }

		public double? Conception_done { get; set; }
		public double? Programming_done { get; set; }
		public double? Testing_done { get; set; }
		public double? Design_done { get; set; }

		public virtual Project Project { get; set; }
		
		public Project_stage(Int64 project_id, int? conception_hours, int? programming_hours, int? testing_hours, int? design_hours,
		                     double? conception_done, double? programming_done, double? testing_done, double? design_done)
		{
			Conception_hours = conception_hours;
			Programming_hours = programming_hours;
			Testing_hours = testing_hours;
			Design_hours = design_hours;
			Conception_done = conception_done;
			Programming_done = programming_done;
			Testing_done = testing_done;
			Design_done = design_done;
			Project_id = project_id;
		}
		public Project_stage(){	}

		public Project_stage(Int64 project_id, int? conception_hours, int? programming_hours, int? testing_hours, 
		                     int? design_hours)
			:this(project_id,conception_hours,programming_hours,testing_hours,design_hours,null,null,null,null)
		{

		}
	}
}