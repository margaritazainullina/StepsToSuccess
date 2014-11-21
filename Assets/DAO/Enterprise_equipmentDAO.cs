using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class Enterprise_equipmentDAO {
	
	//returns list with all characters from db
	public static List<Enterprise_equipment> GetEnterprise_equipment (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `enterprise_equipment`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Enterprise_equipment> enterprise_equipment = new List<Enterprise_equipment>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){

			DateTime purchase_date = Convert.ToDateTime(data["purchase_date"]);
			int? quantity = Helper.GetValueOrNull<int>(Convert.ToString(data["quantity"]));
			int? lease_term = Helper.GetValueOrNull<int>(Convert.ToString(data["lease_term"]));
            bool? isRunning = Helper.GetValueOrNull<bool>(Convert.ToString(data["isRunning"]));
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			Int64 equipment_id = Convert.ToInt64(data["equipment_id"]);

			Enterprise_equipment enterprise_equip = new Enterprise_equipment(purchase_date, quantity, lease_term, isRunning, 
			                                                                 enterprise_id, equipment_id, false);

			Debug.Log("Get enterprise_equip Enterprise_id="+enterprise_id+" and Equipment_id="+equipment_id);
			enterprise_equipment.Add(enterprise_equip);
		}
		_connection.Close ();
		return enterprise_equipment;
	}

	public static List<Enterprise_equipment> LoadEnterprise_equipment(MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM enterprise_equipment WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Enterprise_equipment> enterprise_equipment = new List<Enterprise_equipment>();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			
			DateTime purchase_date = Convert.ToDateTime(data["purchase_date"]);
			int? quantity = Helper.GetValueOrNull<int>(Convert.ToString(data["quantity"]));
			int? lease_term = Helper.GetValueOrNull<int>(Convert.ToString(data["lease_term"]));
			bool? isRunning = Helper.GetValueOrNull<bool>(Convert.ToString(data["isRunning"]));
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			Int64 equipment_id = Convert.ToInt64(data["equipment_id"]);
			
			Enterprise_equipment enterprise_equip = new Enterprise_equipment(purchase_date, quantity, lease_term, isRunning, 
			                                                                 enterprise_id, equipment_id, false);
			
			Debug.Log("Get enterprise_equip Enterprise_id="+enterprise_id+" and Equipment_id="+equipment_id);
			enterprise_equipment.Add(enterprise_equip);
		}
		_connection.Close ();
		return enterprise_equipment;
	}

	public static void InsertEnterprise_equipment (MySqlConnection _connection){		
		foreach (Enterprise_equipment enterprise_equip in Character.Instance.Enterprise.Enterprise_equipment) {
			if(!enterprise_equip.isNew) continue;
			_connection.Open ();
			string Query = "INSERT INTO `enterprise_equipment` values(" + enterprise_equip.Enterprise_id + "," + enterprise_equip.Equipment_id + ",'" + 
				Helper.ToMySQLDateTimeFormat(enterprise_equip.Purchase_date) + "'," + enterprise_equip.Quantity + "," + enterprise_equip.Lease_term + "," + enterprise_equip.IsRunning + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert enterprise_equip Enterprise_id="+enterprise_equip.Enterprise_id+" and Equipment_id="+enterprise_equip.Equipment_id);
			_connection.Close ();
		}
	}

	public static void UpdateEnterprise_equipment (MySqlConnection _connection){		
		foreach (Enterprise_equipment enterprise_equip in Character.Instance.Enterprise.Enterprise_equipment) {
			if(enterprise_equip.isNew) continue;
			_connection.Open ();
			string Query = "UPDATE `enterprise_equipment` SET purchase_date='" + Helper.ToMySQLDateTimeFormat(enterprise_equip.Purchase_date) + "', quantity=" + enterprise_equip.Quantity + 
				", lease_term=" + enterprise_equip.Lease_term + ", isRunning=" + enterprise_equip.IsRunning  + " where enterprise_id=" + enterprise_equip.Enterprise_id + " AND equipment_id=" + enterprise_equip.Equipment_id + ";";
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Update enterprise_equip Enterprise_id="+enterprise_equip.Enterprise_id+" and Equipment_id="+enterprise_equip.Equipment_id);
			_connection.Close ();
		}
	}

	public  static  void DeleteEnterprise_equipment (MySqlConnection _connection, List<Enterprise_equipment> enterprise_equipment){		
		foreach (Enterprise_equipment enterprise_equip in enterprise_equipment) {
			_connection.Open ();
			string Query = "DELETE FROM `enterprise_equipment` WHERE enterprise_id=" + enterprise_equip.Enterprise_id + " AND equipment_id=" + enterprise_equip.Equipment_id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete asset enterprise_equip Enterprise_id="+enterprise_equip.Enterprise_id+" and Equipment_id="+enterprise_equip.Equipment_id);
			_connection.Close ();
		}
	}

}