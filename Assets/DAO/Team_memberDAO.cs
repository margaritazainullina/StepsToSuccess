using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class Team_memberDAO {
	
	//returns list with all characters from db
	public static List<Team_member> GetAssets (MySqlConnection _connection){		
		
		List<Team_member> team_members = new List<Team_member>();	
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `team_member`;";
		MySqlDataReader data = command.ExecuteReader();

		Team_member team_member = null;

		try
		{
			//read data from dataReader and form list of Character instances
			while (data.Read()){
				
				Int64 employee_id = Convert.ToInt64(data["employee_id"]);
				Int64 project_id = Convert.ToInt64(data["project_id"]);
				
				team_member  = new Team_member(employee_id, project_id);
				
				Debug.Log("Get Team_members employee_id="+employee_id+" and project_id="+project_id);
				team_members.Add(team_member);
			}
		}catch (Exception ex) { Debug.Log("Getting data exception:" + ex.Message); }
		finally 
		{
			_connection.Close ();
		}
		_connection.Close();
		
		return team_members;
	}
	
	//What if there's no element for id
	public static void InsertTeam_members (MySqlConnection _connection, List<Team_member> team_members){		//12 ()
		foreach (Team_member team_member in team_members) {
			_connection.Open();
			
			string Query = "INSERT INTO `asset` values(" + team_member.Employee_id + "," + team_member.Project_id + ");";
			
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
			
			MySqlCommand command = new MySqlCommand (Query, _connection);
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader();
			_connection.Close ();
			Debug.Log("Inserted Team_member employee_id="+team_member.Employee_id+" and project_id="+team_member.Project_id);
		}
	}

	public static void DeleteTeam_members (MySqlConnection _connection, List<Team_member> team_members){		
		foreach (Team_member team_member in team_members) {
			_connection.Open ();
			string Query = "DELETE FROM `asset` WHERE employee_id="+team_member.Employee_id+" AND project_id=" + team_member.Project_id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
			Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log("Deleted Team_member employee_id="+team_member.Employee_id+" and project_id="+team_member.Project_id);
			_connection.Close ();
		}
	}
}