using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class EmployeeDAO {
	
	//returns list with all characters from db
	public static List<Employee> GetEmployees (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `employee`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Employee> employees = new List<Employee>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			int qualification =Convert.ToInt32(data["qualification"]);
			Int64 id = Convert.ToInt64(data["id"]);
			decimal salary = Convert.ToDecimal(data["salary"]);
			Int64 role_id = Convert.ToInt64(data["role_id"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

			Employee employee = new Employee(id, title, qualification, salary, role_id, enterprise_id);
			Debug.Log("Get employee "+title);
			employees.Add(employee);
		}
		_connection.Close ();
		return employees;
	}

	public static void InsertEmployees (MySqlConnection _connection, List<Employee> employees){		
		foreach (Employee employee in employees) {
			_connection.Open ();
			string Query = "INSERT INTO `employee` values(" + employee.Id + ",'" + employee.Title + 
				"'," + employee.Qualification + "," + employee.Salary + "," + employee.Role_id + 
					"," + employee.Enterprise_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert employee " + employee.Title);
			_connection.Close ();
		}
	}

	public static void UpdateEmployees (MySqlConnection _connection, List<Employee> employees){		
		foreach (Employee employee in employees) {
			_connection.Open ();
			string Query = "UPDATE `employee` SET title='" + employee.Title + "', qualification=" + 
					employee.Qualification + ", salary=" + employee.Salary + ", role_id=" + employee.Role_id +
					", enterprise_id=" + employee.Enterprise_id +" where id=" + employee.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update employee " + employee.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteEmployees (MySqlConnection _connection, List<Employee> employees){		
		foreach (Employee employee in employees) {
			_connection.Open ();
			string Query = "DELETE FROM `employee` WHERE id="+employee.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete employee " + employee.Title);
			_connection.Close ();
		}
	}

}