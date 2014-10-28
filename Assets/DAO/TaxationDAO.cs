using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class TaxationDAO {
	
	//returns list with all characters from db
	public static List<Taxation> GetTaxations (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `taxation`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Taxation> taxations = new List<Taxation>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			int taxation_group = Convert.ToInt32(data["taxation_group"]);
			decimal max_revenue = Convert.ToDecimal(data["max_revenue"]);
			int max_employee = Convert.ToInt32(data["max_employee"]);

			double VAT = Convert.ToDouble(data["VAT"]);
			double income_duty = Convert.ToDouble(data["income_duty"]);

			Taxation taxation = new Taxation(id, taxation_group, max_revenue, max_employee, 
			                                 VAT, income_duty);
			Debug.Log("Get taxation " + id);
			taxations.Add(taxation);
		}
		_connection.Close ();
		return taxations;
	}
	
	public static void InsertTaxations (MySqlConnection _connection, List<Taxation> taxations){		
		foreach (Taxation taxation in taxations) {
			_connection.Open ();
			string Query = "INSERT INTO `taxation` values(" + taxation.Id + "," + taxation.Taxation_group + "," + 
				taxation.Max_revenue + "," + taxation.Max_employee + "," + taxation.VAT + "," + taxation.Income_duty + ");";
			
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert role " + taxation.Id);
			_connection.Close ();
		}
	}
	public static void UpdateTaxations (MySqlConnection _connection, List<Taxation> taxations){		
		foreach (Taxation taxation in taxations) {
			_connection.Open ();
			string Query = "UPDATE `taxation` SET taxation_group=" + taxation.Taxation_group + ", max_revenue=" + taxation.Max_revenue + 
				", max_employee=" + taxation.Max_employee + ", VAT=" + taxation.VAT + ", income_duty=" + taxation.Income_duty + " where id=" + taxation.Id + ";";
			
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			

			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update role " + taxation.Id);
			_connection.Close ();
		}
	}
	
	public static void DeleteTaxations (MySqlConnection _connection, List<Taxation> taxations){		
		foreach (Taxation taxation in taxations) {
			_connection.Open ();
			string Query = "DELETE FROM `taxation` WHERE id="+taxation.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete role " + taxation.Id);
			_connection.Close ();
		}
	}
}