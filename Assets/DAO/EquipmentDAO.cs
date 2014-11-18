using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class EquipmentDAO {
	
	//returns list with all characters from db
	public static List<Equipment> GetEquipment (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `equipment`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Equipment> equipment = new List<Equipment>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			double title = Convert.ToDouble(data["title"]);
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);

			Equipment equip = new Equipment(id, title, price, false);
			Debug.Log("Get piece of equipment of type =" + title);
			equipment.Add(equip);
		}
		_connection.Close();
		return equipment;
	}

	public static Equipment GetEquipmentById (MySqlConnection _connection, Int64 id)
	{		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT DISTINCT * FROM equipment WHERE id=" + id;
		MySqlDataReader data = command.ExecuteReader();

		Equipment equip = null;

		while (data.Read()){
			id = Convert.ToInt32(data["id"]);
			Int64 title = Convert.ToInt32(data["title"]);
			Int64 price = Convert.ToInt32(data["price"]);
			
			equip = new Equipment(id, title, price, false);
			Debug.Log("Get character "+id);
		}
		_connection.Close ();
		return equip;
	}


	public static void InsertEquipment (MySqlConnection _connection, List<Equipment> equipment){		
		foreach (Equipment equip in equipment) {
			_connection.Open ();
			string Query = "INSERT INTO equipment(title,price) values(" + equip.Title + "," + equip.Price + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader();
			Debug.Log("Insert piece of equipment of type = " + equip.Title);
			_connection.Close ();
		}
	}
	public static void UpdateEquipment (MySqlConnection _connection, List<Equipment> equipment){		
		foreach (Equipment equip in equipment) {
			if(equip.isNew) continue;
			_connection.Open ();
			string Query = "UPDATE `equipment` SET title=" + equip.Title + ", price=" + equip.Price + " where id=" + equip.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);

			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log("Update piece of equipment of type =" + equip.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteCharacters (MySqlConnection _connection, List<Equipment> equipment){		
		foreach (Equipment equip in equipment) {
			_connection.Open ();
			string Query = "DELETE FROM `character` WHERE id="+equip.Id+ ";";


			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log("Delete piece of equipment of type =" + equip.Title);
			_connection.Close ();
		}
	}

}