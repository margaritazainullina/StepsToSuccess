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
			DateTime planned_begin_date = Convert.ToDateTime(data["planned_begin_date"]);
			DateTime planned_end_date = Convert.ToDateTime(data["planned_end_date"]);
			DateTime real_begin_date = Convert.ToDateTime(data["real_begin_date"]);
			DateTime real_end_date = Convert.ToDateTime(data["real_end_date"]);
			int state = Convert.ToInt32(data["status"]);
			decimal stated_budget = Convert.ToDecimal(data["stated_budget"]);
			decimal real_budget = Convert.ToDecimal(data["real_budget"]);
			
			Project project = new Project(id, planned_begin_date, planned_end_date, real_begin_date, real_end_date, 
			                              state, stated_budget, real_budget, _connection);
			Debug.Log("Get character "+ id);
			projects.Add(project);
		}
		_connection.Close ();
		return projects;
	}
	
	public static void InsertProjects (MySqlConnection _connection, List<Project> projects){		
		foreach (Project project in projects) {
			_connection.Open ();
			string Query = "INSERT INTO `project` values(" + project.Id + ",'" + project.Planned_begin_date + "','" + 
				project.Planned_end_date + "','" + project.Real_begin_date + "','" + project.Real_end_date + "'," + 
					project.State + "," + project.Stated_budget + "," + project.Real_budget + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert project " + project.Id);
			_connection.Close ();
		}
	}
	
	public static void UpdateProjects (MySqlConnection _connection, List<Project> projects){		
		foreach (Project project in projects) {
			_connection.Open ();
			string Query = "UPDATE `project` SET planned_begin_date='" + project.Planned_begin_date + "', planned_begin_date='" + project.Planned_end_date + 
				"', real_begin_date='" + project.Real_begin_date + "', real_end_date='" + project.Real_end_date + 
					"', state=" + project.State + ", stated_budget=" + project.Stated_budget + ", real_budget=" + project.Real_budget + " where id=" + project.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
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
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete project " + project.Id);
			_connection.Close ();
		}
	}
	
}