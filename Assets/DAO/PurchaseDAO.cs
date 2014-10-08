using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class PurchaseDAO {
	
	//returns list with all characters from db
	public static List<Purchase> GetPurchases (MySqlConnection _connection)
	{		
		List<Purchase> purchases = new List<Purchase>();

		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `purchase`";
		MySqlDataReader data = command.ExecuteReader();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int quantity = Convert.ToInt32(data["quantity"]);
			Int64 id = Convert.ToInt64(data["id"]);

			//Equipment equip = (equipment == null) ? EquipmentDAO.GetEquipment(_connection) : equipment[id];
			//Asset asset = (assets == null) ? AssetDAO.GetAssets(_connection) : assets[id];
			//Equipment equip = EquipmentDAO.GetEquipment(_connection);
			//Asset asset = AssetDAO.GetAssets(_connection);

			//Purchase purchase = new Purchase(id, quantity);
			Debug.Log("Get character "+id);
			//purchases.Add(purchase);
		}
		_connection.Close ();
		return purchases;
	}

	/*
	public static List<Purchase> GetRelatedPurchases (MySqlConnection _connection, Asset asset, Equipment equip)
	{		
		if(asset == null && equip == null) return null;

		List<Purchase> purchases = new List<Purchase>();

		bool connectedAlready = _connection.State == 1;

		if(!connectedAlready) _connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `purchase` WHERE ";
		command.CommandText += (asset != null) ? "asset_id = " + asset.Id : "equipment_id" + equip.Id;
		MySqlDataReader data = command.ExecuteReader();

		Purchase purchase = null;

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			int quantity = Convert.ToInt32(data["quantity"]);
			Int64 id = Convert.ToInt64(data["id"]);
			
			equip = (equip == null) ? EquipmentDAO.GetEquipmentById(_connection, data["equipment_id"]) : equip;
			asset = (asset == null) ? AssetDAO.GetAssets(_connection) : asset;

			purchase = new Purchase(id, quantity, equip, asset);
			Debug.Log("Get character "+id);
			purchases.Add(purchase);
		}

		if(!connectedAlready) _connection.Close ();

		return purchases;
	}

	public static Purchase GetPurchaseById (MySqlConnection _connection, Int64 id, Asset asset, List<Equipment> equipment)
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
	}
	*/

	public static void InsertPurchases (MySqlConnection _connection, List<Purchase> purchases){		
		foreach (Purchase purchase in purchases) {
			_connection.Open ();
			string Query = "INSERT INTO `purchase` values(" + purchase.Id + "," + purchase.Quantity + "," + 
				purchase.Equipment_id + "," + purchase.Asset_id + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert purchase " + purchase.Id);
			_connection.Close ();
		}
	}
	public static void UpdatePurchases (MySqlConnection _connection, List<Purchase> purchases){		
		foreach (Purchase purchase in purchases) {
			_connection.Open ();
			string Query = "UPDATE `purchase` SET quantity=" + purchase.Quantity + ", equipment_id=" + purchase.Equipment_id + 
				", asset_id=" + purchase.Asset_id + " where id=" + purchase.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update purchase " + purchase.Id);
			_connection.Close ();
		}
	}
	
	public static void DeletePurchases (MySqlConnection _connection, List<Purchase> purchases){		
		foreach (Purchase purchase in purchases) {
			_connection.Open ();
			string Query = "DELETE FROM `purchase` WHERE id="+purchase.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete purchase " + purchase.Id);
			_connection.Close ();
		}
	}

}