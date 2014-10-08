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
		
		List<Purchase> purchases = PurchaseDAO.GetPurchases (_connection);
		List<Service> services = ServiceDAO.GetServices (_connection);
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);

			int? conception_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["conception_hours"]));
		    int? programming_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["programming_hours"]));
		    int? testing_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["testing_hours"]));
		    int? design_hours = Helper.GetValueOrNull<int>(Convert.ToString(data["design_hours"]));

		    double? conception_done = Helper.GetValueOrNull<double>(Convert.ToString(data["conception_done"]));
		    double? programming_done = Helper.GetValueOrNull<double>(Convert.ToString(data["programming_done"]));
		    double? testing_done = Helper.GetValueOrNull<double>(Convert.ToString(data["testing_done"]));
		    double? design_done = Helper.GetValueOrNull<double>(Convert.ToString(data["design_done"]));

			Int64 project_id = Convert.ToInt64(data["project_id"]);

			Project_stage project_stage = new Project_stage(id, conception_hours, programming_hours, testing_hours, design_hours,
			                                                conception_done, programming_done, testing_done, design_done, project_id);
			Debug.Log("Get asset type="+id);
			project_stages.Add(project_stage);
		}
		_connection.Close ();
		return project_stages;
	}

	public static void InsertProject_stages (MySqlConnection _connection, List<Project_stage> project_stages, Int64 enterprise_id){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "INSERT INTO `project_stage` values(" + project_stage.Id + "," + project_stage.Conception_hours + "," + project_stage.Programming_hours+ "," + 
				project_stage.Testing_hours+ "," + project_stage.Design_hours+ "," + project_stage.Conception_done+ "," + project_stage.Programming_done+ "," 
					+ project_stage.Testing_done+ "," + project_stage.Design_done+ "," + project_stage.Project_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert project_stage type="+project_stage.Id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}

	public static void UpdateProject_stages (MySqlConnection _connection, List<Project_stage> project_stages){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "UPDATE `project_stage` SET conception_hours='" + project_stage.Conception_hours + ", programming_hours=" + project_stage.Programming_hours+ ", testing_hours=" + 
				project_stage.Testing_hours+ ", design_hours=" + project_stage.Design_hours+ ", conception_done=" + project_stage.Conception_done+ ", programming_done=" + project_stage.Programming_done+ ", testing_done=" 
					+ project_stage.Testing_done+ ", design_done=" + project_stage.Design_done+ ", project_id=" + project_stage.Project_id + " where id=" + project_stage.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update project_stage type="+project_stage.Id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}
	
	public static void DeleteProject_stages (MySqlConnection _connection, List<Project_stage> project_stages){		
		foreach (Project_stage project_stage in project_stages) {
			_connection.Open ();
			string Query = "DELETE FROM `project_stage` WHERE id="+project_stage.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete project_stage type="+project_stage.Id+" and value="+project_stage.Project_id);
			_connection.Close ();
		}
	}

}