using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class ServiceDAO {
	
	//returns list with all characters from db
	public static List<Service> GetServices (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `service`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Service> services = new List<Service>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);
			int period = Convert.ToInt32(data["period"]);
			int periodsPaid = Convert.ToInt32(data["periodsPaid"]);
			Int64 asset_id = Convert.ToInt64(data["asset_id"]);
			Int64 action_id = Convert.ToInt64(data["action_id"]);
			decimal effectiveness = Convert.ToDecimal(data["effectiveness"]);

			Service service = null; //new Service(id, title, price, period,
			                             // effectiveness);
			Debug.Log("Get service "+title);
			services.Add(service);
		}
		_connection.Close ();
		return services;
	}
/*
	public static Service GetServiceById (MySqlConnection _connection, Int64 id){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `service` WHERE id=" + id;
		MySqlDataReader data = command.ExecuteReader();
		
		Service service = null;
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			decimal price = Convert.ToDecimal(data["price"]);
			int period = Convert.ToInt32(data["period"]);
			Int64 company_id = Convert.ToInt64(data["company_id"]);
			Int64 action_id = Convert.ToInt64(data["action_id"]);
			decimal effectiveness = Convert.ToDecimal(data["effectiveness"]);
			
			service = null; //new Service(id, title, price, period, effectiveness);
			Debug.Log("Get service "+title);
		}
		_connection.Close ();
		return service;
	}*/

	public static void InsertServices (MySqlConnection _connection, List<Service> services){		
		foreach (Service service in services) {
			_connection.Open ();
			string Query = "INSERT INTO `service` values(" + service.Id + ",'" + service.Title + "'," + 
				service.Price + "," + service.Period+ "," + service.PeriodsPaid + "," + service.Effectiveness + "," + service.Asset_id + "," + service.Company.Id + ");";
			
			
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert service " + service.Title);
			_connection.Close ();
		}
	}
	public static void UpdateServices (MySqlConnection _connection, List<Service> services){		
		foreach (Service service in services) {
			_connection.Open ();
			string Query = "UPDATE `service` SET title='" + service.Title + "', price=" + service.Price + 
				", period=" + service.Period + ", period=" + service.PeriodsPaid +", effectiveness=" + service.Effectiveness + ", asset_id=" + 
					service.Asset_id + ", company_id=" + service.Company.Id  + " where id=" + service.Id + ";";
			
			
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update service " + service.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteServices (MySqlConnection _connection, List<Service> services){		
		foreach (Service service in services) {
			_connection.Open ();
			string Query = "DELETE FROM `character` WHERE id="+service.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete service " + service.Title);
			_connection.Close ();
		}
	}

}