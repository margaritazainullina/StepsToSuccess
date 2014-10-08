using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class Human_resourcesDAO {
	
	//returns list with all characters from db
	public static List<Human_resources> GetHuman_resources (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `human_resources`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Human_resources> human_resources = new List<Human_resources>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			int hours = Convert.ToInt32(data["hours"]);
			Int64 role_id = Convert.ToInt64(data["role_id"]);
			Int64 project_id = Convert.ToInt64(data["project_id"]);

			Human_resources hr = new Human_resources(id, hours, role_id, project_id);
			Debug.Log("Get HR id="+id);
			human_resources.Add(hr);
		}
		_connection.Close();
		return human_resources;
	}

	public static void InsertHuman_resources (MySqlConnection _connection, List<Human_resources> human_resources){		
		foreach (Human_resources hr in human_resources) {
			_connection.Open ();
			string Query = "INSERT INTO `human_resources` values(" + hr.Id + "," + hr.Hours + "," +
				hr.Project_id + "," + hr.Role_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert HR id="+hr.Id);
			_connection.Close();
		}
	}
	public static void UpdateHuman_resources (MySqlConnection _connection, List<Human_resources> human_resources){		
		foreach (Human_resources hr in human_resources) {
			_connection.Open ();
			string Query = "UPDATE `human_resources` SET hour=" + hr.Hours + ", project_id=" + hr.Project_id + 
				", role_id=" + hr.Role_id  + " where id=" + hr.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update HR id="+hr.Id);
			_connection.Close();
		}
	}
	
	public static void DeleteCharacters (MySqlConnection _connection, List<Human_resources> human_resources){		
		foreach (Human_resources hr in human_resources) {
			_connection.Open ();
			string Query = "DELETE FROM `human_resources` WHERE id="+hr.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete HR id="+hr.Id);
			_connection.Close ();
		}
	}

}