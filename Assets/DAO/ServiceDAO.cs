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
			decimal effectiveness = Convert.ToDecimal(data["effectiveness"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			Int64 company_id = Convert.ToInt64(data["company_id"]);
			
			Service service = new Service(id,title,price,period,periodsPaid,effectiveness,enterprise_id,company_id, false);
			Debug.Log("Get service "+title);
			services.Add(service);
		}
		_connection.Close ();
		return services;
	}

	public static List<Service> LoadServices (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM service WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Service> services = new List<Service>();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);
			int period = Convert.ToInt32(data["period"]);
			int periodsPaid = Convert.ToInt32(data["periods_paid"]);
			decimal effectiveness = Convert.ToDecimal(data["effectiveness"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			Int64 company_id = Convert.ToInt64(data["company_id"]);
			
			Service service = new Service(id,title,price,period,periodsPaid,effectiveness,enterprise_id,company_id, false);
			// effectiveness);
			Debug.Log("Get service "+title);
			services.Add(service);
		}
		_connection.Close ();
		return services;
	}

	public static void InsertServices (MySqlConnection _connection){		
		foreach (Service service in Character.Instance.Enterprise.Services) {
			if(!service.isNew) continue;
			_connection.Open ();
			string Query = "INSERT INTO service(title,price,period,effectiveness,periods_paid,enterprise_id,company_id) values('" + service.Title + "'," + 
				service.Price + "," + service.Period+ "," + service.PeriodsPaid + "," + service.Effectiveness + "," + service.Enterprise_id + "," + service.Company_id + ");";
			
			
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert service " + service.Title);
			_connection.Close ();
		}
	}
	public static void UpdateServices (MySqlConnection _connection){		
		foreach (Service service in Character.Instance.Enterprise.Services) {
			if(service.isNew) continue;
			_connection.Open ();
			string Query = "UPDATE `service` SET title='" + service.Title + "', price=" + service.Price + 
				", period=" + service.Period + ", periods_paid=" + service.PeriodsPaid +", effectiveness=" + service.Effectiveness + ", enterprise_id=" + 
					service.Enterprise_id + ", company_id=" + service.Company_id  + " where id=" + service.Id + ";";
			
			
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