using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class Project_stageDAO {
	
	//returns list with all characters from db
	public static List<Project_stage> GetProject_stages (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `project_stage`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Project_stage> project_stages = new List<Project_stage>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int? conception_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["conception_hours"]));
		    int? programming_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["programming_hours"]));
		    int? testing_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["testing_hours"]));
		    int? design_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["design_hours"]));

		    double? conception_done = Helper.GetValueOrNull<double>(Convert.ToString(data["conception_done"]));
		    double? programming_done = Helper.GetValueOrNull<double>(Convert.ToString(data["programming_done"]));
		    double? testing_done = Helper.GetValueOrNull<double>(Convert.ToString(data["testing_done"]));
		    double? design_done = Helper.GetValueOrNull<double>(Convert.ToString(data["design_done"]));

			Int64 project_id = Convert.ToInt64(data["project_id"]);

			Project_stage project_stage = new Project_stage(project_id, conception_hours, programming_hours, testing_hours, design_hours,
			                                                conception_done, programming_done, testing_done, design_done);
			Debug.Log("Get asset type="+project_id);
			project_stages.Add(project_stage);
		}
		_connection.Close ();
		return project_stages;
	}

	public static List<Project_stage> GetProject_stagesByProjectId (MySqlConnection _connection, Project project){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM project_stage WHERE project_id=" + project.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Project_stage> project_stages = new List<Project_stage>();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int? conception_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["conception_hours"]));
			int? programming_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["programming_hours"]));
			int? testing_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["testing_hours"]));
			int? design_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["design_hours"]));
			
			double? conception_done = Helper.GetValueOrNull<double>(Convert.ToString(data["conception_done"]));
			double? programming_done = Helper.GetValueOrNull<double>(Convert.ToString(data["programming_done"]));
			double? testing_done = Helper.GetValueOrNull<double>(Convert.ToString(data["testing_done"]));
			double? design_done = Helper.GetValueOrNull<double>(Convert.ToString(data["design_done"]));
			
			Int64 project_id = Convert.ToInt64(data["project_id"]);
			
			Project_stage project_stage = new Project_stage(project_id, conception_hours, programming_hours, testing_hours, design_hours,
			                                                conception_done, programming_done, testing_done, design_done);
			Debug.Log("Get asset type="+project_id);
			project_stages.Add(project_stage);
		}
		_connection.Close ();
		return project_stages;
	}

	public static Project_stage LoadProject_stages (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT project_stage.* FROM project_stage, project WHERE " +
			"project_stage.project_id=project.id AND project.enterprise_id ="+ enterprise.Id +";";
		MySqlDataReader data = command.ExecuteReader();
		
		Project_stage project_stage = null;
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int? conception_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["conception_hours"]));
			int? programming_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["programming_hours"]));
			int? testing_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["testing_hours"]));
			int? design_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["design_hours"]));
			
			double? conception_done = Helper.GetValueOrNull<double>(Convert.ToString(data["conception_done"]));
			double? programming_done = Helper.GetValueOrNull<double>(Convert.ToString(data["programming_done"]));
			double? testing_done = Helper.GetValueOrNull<double>(Convert.ToString(data["testing_done"]));
			double? design_done = Helper.GetValueOrNull<double>(Convert.ToString(data["design_done"]));
			
			Int64 project_id = Convert.ToInt64(data["project_id"]);
			
			project_stage = new Project_stage(project_id, conception_hours, programming_hours, testing_hours, design_hours,
			                                                conception_done, programming_done, testing_done, design_done);
			Debug.Log("Get asset type="+project_id);
		}
		_connection.Close ();
		return project_stage;
	}

	public static void InsertProject_stages (MySqlConnection _connection, List<Project_stage> project_stages, Int64 enterprise_id){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "INSERT INTO `project_stage` values(" + project_stage.Project_id + "," + project_stage.Conception_hours + "," + project_stage.Programming_hours+ "," + 
				project_stage.Testing_hours+ "," + project_stage.Design_hours+ "," + project_stage.Conception_done+ "," + project_stage.Programming_done+ "," 
					+ project_stage.Testing_done+ "," + project_stage.Design_done+ ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert project_stage type="+project_stage.Project_id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}

	public static void UpdateProject_stages (MySqlConnection _connection, List<Project_stage> project_stages){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "UPDATE `project_stage` SET conception_hours=" + project_stage.Conception_hours + ", programming_hours=" + project_stage.Programming_hours+ ", testing_hours=" + 
				project_stage.Testing_hours+ ", design_hours=" + project_stage.Design_hours+ ", conception_done=" + project_stage.Conception_done+ ", programming_done=" + project_stage.Programming_done+ ", testing_done=" 
					+ project_stage.Testing_done+ ", design_done=" + project_stage.Design_done + " where project_id=" + project_stage.Project_id + ";";
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update project_stage type="+project_stage.Project_id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}
	
	public static void DeleteProject_stages (MySqlConnection _connection, List<Project_stage> project_stages){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "DELETE FROM `project_stage` WHERE project_id="+project_stage.Project_id+ ";";

			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete project_stage type="+project_stage.Project_id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}

}