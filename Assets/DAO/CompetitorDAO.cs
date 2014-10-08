using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class CompetitorDAO {
	
	//returns list with all characters from db
	public static List<Competitor> GetCompetitors (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `competitor`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Competitor> compatitors = new List<Competitor>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			double success_rate = Convert.ToDouble(data["success_rate"]);
			Int64 id = Convert.ToInt64(data["id"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

			Competitor compatitor = new Competitor(id, title, success_rate, enterprise_id);
			Debug.Log("Get competitor "+title);
			compatitors.Add(compatitor);
		}
		_connection.Close ();
		return compatitors;
	}

	public static void InsertCompetitors (MySqlConnection _connection, List<Competitor> compatitors){		
		foreach (Competitor compatitor in compatitors) {
			_connection.Open ();
			string Query = "INSERT INTO `competitor` values(" + compatitor.Id + ",'" + compatitor.Title + "'," + 
				compatitor.Success_rate + "," + compatitor.Enterprise_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert competitor " + compatitor.Title);
			_connection.Close ();
		}
	}
	public static void UpdateCompetitors (MySqlConnection _connection, List<Competitor> compatitors){		
		foreach (Competitor compatitor in compatitors) {
			_connection.Open ();
			string Query = "UPDATE `competitor` SET title='" + compatitor.Title + "', success_rate=" + compatitor.Success_rate +
				", enterprise_id=" + compatitor.Enterprise_id  + " where id=" + compatitor.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update competitor " + compatitor.Title);
			_connection.Close ();
		}
	}

	public static void DeleteCompetitors (MySqlConnection _connection, List<Competitor> compatitors){		
		foreach (Competitor compatitor in compatitors) {
			_connection.Open ();
			string Query = "DELETE FROM `competitor` WHERE id="+compatitor.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete competitor " + compatitor.Title);
			_connection.Close ();
		}
	}
}