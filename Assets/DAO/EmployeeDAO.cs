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
			double qualification =Convert.ToInt32(data["qualification"]);
			Int64 id = Convert.ToInt64(data["id"]);
			decimal salary = Convert.ToDecimal(data["salary"]);
			Int64 role_id = Convert.ToInt64(data["role_id"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

			Employee employee = new Employee(id, title, qualification, salary, role_id, enterprise_id, false);
			Debug.Log("Get employee "+title);
			employees.Add(employee);
		}
		_connection.Close ();
		return employees;
	}

	public static List<Employee> LoadEmployees (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM employee WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Employee> employees = new List<Employee>();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			double qualification =Convert.ToInt32(data["qualification"]);
			Int64 id = Convert.ToInt64(data["id"]);
			decimal salary = Convert.ToDecimal(data["salary"]);
			Int64 role_id = Convert.ToInt64(data["role_id"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			
			Employee employee = new Employee(id, title, qualification, salary, role_id, enterprise_id, false);
			Debug.Log("Get employee "+title);
			employees.Add(employee);
		}
		_connection.Close ();
		return employees;
	}

	public static void InsertEmployees (MySqlConnection _connection, List<Employee> employees){		
		foreach (Employee employee in employees) {
			_connection.Open ();
			string Query = "INSERT INTO employee(title,salary,qualification,role_id,enterprise_id) values('" + employee.Title + 
				"'," + employee.Qualification + "," + employee.Salary + "," + employee.Role_id + 
					"," + employee.Enterprise_id + ");";
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert employee " + employee.Title);
			_connection.Close ();
		}
	}

	public static void UpdateEmployees (MySqlConnection _connection){		
		foreach (Employee employee in Character.Instance.Enterprise.Employees) {
			if(employee.isNew) continue;
			_connection.Open ();
			string Query = "UPDATE `employee` SET title='" + employee.Title + "', qualification=" + 
					employee.Qualification + ", salary=" + employee.Salary + ", role_id=" + employee.Role_id +
					", enterprise_id=" + employee.Enterprise_id +" where id=" + employee.Id + ";";
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

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
 
			command.ExecuteReader ();
			Debug.Log ("Delete employee " + employee.Title);
			_connection.Close ();
		}
	}

	public static Employee GetEmployeeById (MySqlConnection _connection, Int64 id){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `employee` WHERE id=" + id + ";";
		MySqlDataReader data = command.ExecuteReader();

		string title = (string)data["title"];
		int qualification =Convert.ToInt32(data["qualification"]);
		decimal salary = Convert.ToDecimal(data["salary"]);
		Int64 role_id = Convert.ToInt64(data["role_id"]);
		Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
		
		Employee employee = new Employee(id, title, qualification, salary, role_id, enterprise_id, false);
		Debug.Log("Get employee "+title); 

		_connection.Close ();
		return employee;
	}



}