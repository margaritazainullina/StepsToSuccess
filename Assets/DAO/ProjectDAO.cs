using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class ProjectDAO {
	
	//returns list with all characters from db
	public static List<Project> GetProjects (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `project`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Project> projects = new List<Project>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			string title = Convert.ToString(data["title"]);
			DateTime planned_begin_date = Convert.ToDateTime(data["planned_begin_date"]);
			DateTime planned_end_date = Convert.ToDateTime(data["planned_end_date"]);
			DateTime real_begin_date = Convert.ToDateTime(data["real_begin_date"]);
			DateTime real_end_date = Convert.ToDateTime(data["real_end_date"]);
			int state = Convert.ToInt32(data["state"]);
			decimal stated_budget = Convert.ToDecimal(data["stated_budget"]);
			decimal expenditures = Convert.ToDecimal(data["expenditures"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			
			Project project = new Project(id, title, planned_begin_date, planned_end_date, real_begin_date, real_end_date, 
			                              state, stated_budget, enterprise_id);
			Debug.Log("Get character "+ id);
			projects.Add(project);
		}
		_connection.Close ();
		return projects;
	}

	public static List<Project> LoadProjects (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM project WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Project> projects = new List<Project>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			String title = Convert.ToString(data["title"]);
			DateTime planned_begin_date = Convert.ToDateTime(data["planned_begin_date"]);
			DateTime planned_end_date = Convert.ToDateTime(data["planned_end_date"]);
			DateTime real_begin_date = Convert.ToDateTime(data["real_begin_date"]);
			DateTime real_end_date = Convert.ToDateTime(data["real_end_date"]);
			int state = Convert.ToInt32(data["state"]);
			decimal stated_budget = Convert.ToDecimal(data["stated_budget"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			
			Project project = new Project(id,title, planned_begin_date, planned_end_date, real_begin_date, real_end_date, 
			                              state, stated_budget, enterprise_id);
			Debug.Log("Get character "+ id);
			projects.Add(project);
		}
		_connection.Close ();
		return projects;
	}
	
	public static void InsertProjects (MySqlConnection _connection, List<Project> projects){		
		foreach (Project project in projects) {
			_connection.Open ();
			string Query = "INSERT INTO `project` values(" + project.Id + ",'" + project.Title + "','" + Helper.ToMySQLDateTimeFormat(project.Planned_begin_date) + "','" + 
				Helper.ToMySQLDateTimeFormat(project.Planned_end_date) + "','" + Helper.ToMySQLDateTimeFormat(project.Real_begin_date) + "','" + Helper.ToMySQLDateTimeFormat(project.Real_end_date) + "'," + 
					project.State + "," + project.Stated_budget + ");";
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert project " + project.Id);
			_connection.Close ();
		}
	}
	
	public static void UpdateProjects (MySqlConnection _connection, List<Project> projects){		
		foreach (Project project in projects) {
			_connection.Open ();
			string Query = "UPDATE `project` SET planned_begin_date='" + Helper.ToMySQLDateTimeFormat(project.Planned_begin_date) + "', title='" + project.Title + "', planned_begin_date='" + Helper.ToMySQLDateTimeFormat(project.Planned_end_date) + 
				"', real_begin_date='" + Helper.ToMySQLDateTimeFormat(project.Real_begin_date) + "', real_end_date='" + Helper.ToMySQLDateTimeFormat(project.Real_end_date) + 
					"', state=" + project.State + ", stated_budget=" + project.Stated_budget + " where id=" + project.Id + ";";
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update project " + project.Id);
			_connection.Close ();
		}
	}
	
	public static void DeleteCharacters (MySqlConnection _connection, List<Project> projects){		
		foreach (Project project in projects) {
			_connection.Open ();
			string Query = "DELETE FROM `project` WHERE id="+project.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete project " + project.Id);
			_connection.Close ();
		}
	}

	public static void UpdateProgress(MySqlConnection _connection, Project project, DateTime salary_payment_date) 
	{
		string Query = "SELECT Employee.id, sum(Salary_payment.hours_worked) as hours_worked, Role.title, Employee.Qualification " +
						"FROM Project, Team_member, Employee, Salary_payment, Role " + 
						"WHERE Project.Id=Team_member.Project_id AND Employee.Id=Team_member.Employee_id " +
						"AND Employee.Id=Salary_payment.Employee_id AND Employee.Role_id=Role.Id AND Project.Id=" + project.Id + " AND Salary_payment.date='" 
				+ Helper.ToMySQLDateTimeFormat (salary_payment_date) + "' group by Employee.id;";
		_connection.Open ();

		MySqlCommand command = new MySqlCommand (Query, _connection);

		double conception_hours = 0;
		double programming_hours = 0;
		double testing_hours = 0;
		double design_hours = 0;

		MySqlDataReader data = command.ExecuteReader();

		while (data.Read()) {
						int hours_worked = Convert.ToInt32 (data ["hours_worked"]);
						string title = Convert.ToString (data ["title"]);
						double qualification = Convert.ToInt32 (data ["qualification"]);
						switch (title) {
						case "Analyst":
								{
										conception_hours += (int)hours_worked * qualification;
										break;
								}
						case "Programmer":
								{
										programming_hours += (int)hours_worked * qualification;
										break;
								}
						case "Tester":
								{
										testing_hours += (int)hours_worked * qualification;
										break;
								}
						case "Designer":
								{
										design_hours += (int)hours_worked * qualification;
										break;
								}
						}
				}
		_connection.Close();

			List<Project_stage> project_stages = Project_stageDAO.GetProject_stages(_connection);
			foreach (Project_stage project_stage in project_stages) {
				if(project_stage.Project_id != project.Id)
				{
					project_stages.Remove(project_stage);
				}
			}

			project_stages[0].Conception_done = conception_hours;
			project_stages[0].Programming_done = programming_hours;
			project_stages[0].Testing_done = testing_hours;
			project_stages[0].Design_done = design_hours;

			Project_stageDAO.UpdateProject_stages(_connection, project_stages);

			Debug.Log("Get character "+ conception_hours + "!!!"+ programming_hours + "!!!"+ 
			          testing_hours + "!!!"+ design_hours + "!!!");
			//projects.Add(project);

			

			Debug.Log ("Progress updated " + project.Id);		
	}
							
	public static void UpdateEmployeesQualification (MySqlConnection connection, Project project, Product product){	

		List<Employee> employees = new List<Employee>();
		
		Dictionary<Int64,double> employeeData = new Dictionary<Int64, double>();

		try
		{
		connection.Open ();
				string Query = "SELECT Employee.id, sum(Salary_payment.hours_worked) as hours_worked, Employee.Qualification" +
						"FROM Project, Team_member, Employee, Salary_payment" +
						"WHERE Project.Id=Team_member.Project_id AND Employee.Id=Team_member.Employee_id" +
						"AND Employee.Id=Salary_payment.Employee_id AND Project.Id=" + project.Id + 
					" AND Salary_payment.`date` BETWEEN '" + project.Real_begin_date + "' AND '" + project.Real_end_date + "' group by Employee.id;";
				MySqlCommand command = new MySqlCommand (Query, connection);
				
				MySqlDataReader data = command.ExecuteReader();


			while (data.Read()) {
					Int64 id = Convert.ToInt32(data ["hours_worked"]);
					int hours_worked = Convert.ToInt32(data ["hours_worked"]);
					double qualification = Convert.ToInt32 (data ["qualification"]);
					employeeData.Add(id, qualification + (hours_worked/((project.Real_end_date - project.Real_begin_date).TotalDays))*product.Quality);
				}
				//MB create objects of employee to use update employeedao and set there qualification

		}catch(Exception ex) {
				}
		finally
		{
			connection.Close();
		}
		
		foreach (KeyValuePair<Int64, double> data in employeeData) 
		{
			employees.Add(EmployeeDAO.GetEmployeeById(connection, data.Key));
		}
	
		EmployeeDAO.UpdateEmployees(connection, employees);

	} 

}