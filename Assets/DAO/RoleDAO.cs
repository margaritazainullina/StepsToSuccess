using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class RoleDAO {
	
	//returns list with all characters from db
	public static List<Role> GetRoles (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `role`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Role> roles = new List<Role>();

		List<Employee> employees = EmployeeDAO.GetEmployees (_connection);
		List<Human_resources> human_resources = Human_resourcesDAO.GetHuman_resources (_connection);

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal min_salary = Convert.ToDecimal(data["min_salary"]);
			decimal max_salary = Convert.ToDecimal(data["max_salary"]);

			Role role = new Role(id, title, min_salary, max_salary);
			Debug.Log("Get role " + title);
			roles.Add(role);
		}
		_connection.Close ();
		return roles;
	}
	
	public static void InsertRoles (MySqlConnection _connection, List<Role> roles){		
		foreach (Role role in roles) {
			_connection.Open ();
			string Query = "INSERT INTO `role` values(" + role.Id + ",'" + role.Title + "'," + 
				role.Min_salary + "," + role.Max_salary + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert role " + role.Title);
			_connection.Close ();
		}
	}
	public static void UpdateRoles (MySqlConnection _connection, List<Role> roles){		
		foreach (Role role in roles) {
			_connection.Open ();
			string Query = "UPDATE `role` SET title='" + role.Title + "', min_salary=" + role.Min_salary + 
				", max_salary=" + role.Max_salary + " where id=" + role.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Update role " + role.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteRoles (MySqlConnection _connection, List<Role> roles){		
		foreach (Role role in roles) {
			_connection.Open ();
			string Query = "DELETE FROM `role` WHERE id="+role.Id+ ";";



			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete role " + role.Title);
			_connection.Close ();
		}
	}
}