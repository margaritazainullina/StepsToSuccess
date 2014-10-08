using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class RevenueDAO {
	
	//returns list with all characters from db
	public static List<Revenue> GetRevenues (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `revenue`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Revenue> revenues = new List<Revenue>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			DateTime revenue_date = Convert.ToDateTime(data["revenue_date"]);
			decimal value = Convert.ToDecimal(data["value"]);
			Int64 id = Convert.ToInt64(data["id"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

			Revenue revenue = new Revenue(id, revenue_date, value, enterprise_id);
			Debug.Log("Get character "+id);
			revenues.Add(revenue);
		}
		_connection.Close ();
		return revenues;
	}

	public static void InsertRevenues (MySqlConnection _connection, List<Revenue> revenues){		
		foreach (Revenue revenue in revenues) {
			_connection.Open ();
			string Query = "INSERT INTO `revenue` values(" + revenue.Id + "," + revenue.Revenue_date + "," + 
				revenue.Value + "," + revenue.Enterprise_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert revenue " + revenue.Id);
			_connection.Close ();
		}
	}
	public static void UpdateRevenues (MySqlConnection _connection, List<Revenue> revenues){		
		foreach (Revenue revenue in revenues) {
			_connection.Open ();
			string Query = "UPDATE `revenue` SET revenue_date=" + revenue.Revenue_date + ", value=" + revenue.Value + 
				", enterprise_id=" + revenue.Enterprise_id  + " where id=" + revenue.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update revenue " + revenue.Id);
			_connection.Close ();
		}
	}
	
	public static void DeleteRevenues (MySqlConnection _connection, List<Revenue> revenues){		
		foreach (Revenue revenue in revenues) {
			_connection.Open ();
			string Query = "DELETE FROM `revenue` WHERE id="+revenue.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete revenue " + revenue.Id);
			_connection.Close ();
		}
	}

}