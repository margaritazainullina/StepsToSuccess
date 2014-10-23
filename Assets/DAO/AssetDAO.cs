using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class AssetDAO {
	
	//returns list with all characters from db
	public static List<Asset> GetAssets (MySqlConnection _connection){		

		List<Asset> assets = new List<Asset>();	
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `asset`;";
		MySqlDataReader data = command.ExecuteReader();

		Asset asset = null;

		try
		{
		//read data from dataReader and form list of Character instances
			while (data.Read()){

				int value = Convert.ToInt32(data["value"]);
				Int64 id = Convert.ToInt64(data["id"]);
				DateTime asset_date = Convert.ToDateTime(data["asset_date"]);
				Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

				asset = new Asset(id, value,asset_date, enterprise_id);

				Debug.Log("Get asset id="+id+" and value="+value);
				assets.Add(asset);
			}
		}catch (Exception ex) { Debug.Log("Getting data exception:" + ex.Message); }
		finally 
		{
			_connection.Close ();
		}
		_connection.Close();

		return assets;
	}

	public static List<Asset> LoadAssets(MySqlConnection _connection, Enterprise enterprise){		
		
		List<Asset> assets = new List<Asset>();	
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM asset WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		Asset asset = null;
		
		try
		{
			//read data from dataReader and form list of Character instances
			while (data.Read()){
				
				int value = Convert.ToInt32(data["value"]);
				Int64 id = Convert.ToInt64(data["id"]);
				DateTime asset_date = Convert.ToDateTime(data["asset_date"]);
				Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
				
				asset = new Asset(id, value,asset_date, enterprise_id);
				
				Debug.Log("Get asset id="+id+" and value="+value);
				assets.Add(asset);
			}
		}catch (Exception ex) { Debug.Log("Getting data exception:" + ex.Message); }
		finally 
		{
			_connection.Close ();
		}
		_connection.Close();
		
		return assets;
	}

	//What if there's no element for id
	public static void InsertAssets (MySqlConnection _connection, List<Asset> assets){		//12 ()
		foreach (Asset asset in assets) {
			_connection.Open();

			string Query = "INSERT INTO `asset` values(" + asset.Id + "," + asset.Value + ",'" + Helper.ToMySQLDateTimeFormat(asset.Asset_date) 
				+ "'," + asset.Enterprise_id + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader();
			_connection.Close ();
			Debug.Log ("Insert asset id="+asset.Id+" and value="+asset.Value);
		}
	}

	public static void UpdateAssets (MySqlConnection _connection, List<Asset> assets){		
		foreach (Asset asset in assets) {
			_connection.Open ();
			string Query = "UPDATE `asset` SET value='" + asset.Value + "', asset_date='" + Helper.ToMySQLDateTimeFormat(asset.Asset_date) + "', enterprise_id=" + 
				asset.Enterprise_id +  " where id=" + asset.Id + ";";
			
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);

			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update asset id="+asset.Id+" and value="+asset.Value);
			_connection.Close ();
		}
	}
	
	public static void DeleteAssets (MySqlConnection _connection, List<Asset> assets){		
		foreach (Asset asset in assets) {
			_connection.Open ();
			string Query = "DELETE FROM `asset` WHERE id="+asset.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete asset id="+asset.Id+" and value="+asset.Value);
			_connection.Close ();
		}
	}
}