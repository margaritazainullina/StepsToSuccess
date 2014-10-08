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

			Equipment equip = new Equipment(id, title, price);
			Debug.Log("Get piece of equipment of type =" + title);
			equipment.Add(equip);
		}
		_connection.Close();
		return equipment;
	}
	/*
	public static Equipment GetEquipmentById (MySqlConnection _connection, Int64 id, Asset asset, List<Equipment> equipment)
	{		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `purchase` WHERE id=" + id;
		MySqlDataReader data = command.ExecuteReader();
		
		Purchase purchase = null;
		
		//asset = (asset == null) ? EquipmentDAO.GetEquipmentById(_connection) : asset;
		equipment = (equipment == null) ? EquipmentDAO.GetEquipment(_connection) : equipment[id];
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int quantity = Convert.ToInt32(data["quantity"]);
			
			purchase = new Purchase(id, quantity, equipment, asset);
			Debug.Log("Get character "+id);
		}
		_connection.Close ();
		return purchase;
	}*/


	public static void InsertEquipment (MySqlConnection _connection, List<Equipment> equipment){		
		foreach (Equipment equip in equipment) {
			_connection.Open ();
			string Query = "INSERT INTO `equipment` values(" + equip.Id + "," + equip.Title + "," + equip.Price + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader();
			Debug.Log("Insert piece of equipment of type = " + equip.Title);
			_connection.Close ();
		}
	}
	public static void UpdateEquipment (MySqlConnection _connection, List<Equipment> equipment){		
		foreach (Equipment equip in equipment) {
			_connection.Open ();
			string Query = "UPDATE `equipment` SET title=" + equip.Title + ", price=" + equip.Price + " where id=" + equip.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
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
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log("Delete piece of equipment of type =" + equip.Title);
			_connection.Close ();
		}
	}

}