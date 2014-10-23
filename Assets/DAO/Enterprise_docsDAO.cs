using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class Enterprise_docsDAO {
	
	//returns list with all characters from db
	public static List<Enterprise_docs> GetEnterprise_docs (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `enterprise_docs`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Enterprise_docs> enterprise_docs = new List<Enterprise_docs>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			bool availability = Convert.ToBoolean(data["availability"]);
			bool is_active = Convert.ToBoolean(data["is_active"]);
			Int64 document_id = Convert.ToInt64(data["document_id"]);
			DateTime expiration_date = Convert.ToDateTime(data["expiration_date"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

			Enterprise_docs enterprise_doc = new Enterprise_docs(id, availability, is_active, expiration_date, document_id, enterprise_id);
			Debug.Log("Get enterprise_doc "+id);
			enterprise_docs.Add(enterprise_doc);
		}
		_connection.Close ();
		return enterprise_docs;
	}

	public static List<Enterprise_docs> LoadEnterprise_docs(MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM enterprise_docs WHERE enterprise_id=" + enterprise.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Enterprise_docs> enterprise_docs = new List<Enterprise_docs>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			bool availability = Convert.ToBoolean(data["availability"]);
			bool is_active = Convert.ToBoolean(data["is_active"]);
			Int64 document_id = Convert.ToInt64(data["document_id"]);
			DateTime expiration_date = Convert.ToDateTime(data["expiration_date"]);
			Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
			
			Enterprise_docs enterprise_doc = new Enterprise_docs(id, availability, is_active, expiration_date, document_id, enterprise_id);
			Debug.Log("Get enterprise_doc "+id);
			enterprise_docs.Add(enterprise_doc);
		}
		_connection.Close ();
		return enterprise_docs;
	}

	public static void InsertEnterprise_docs (MySqlConnection _connection, List<Enterprise_docs> enterprise_docs){		
		foreach (Enterprise_docs enterprise_doc in enterprise_docs) {
			_connection.Open ();
			string Query = "INSERT INTO `enterprise_docs` values(" + enterprise_doc.Id + "," + enterprise_doc.Document_id + "," + enterprise_doc.Availability + 
				"," + enterprise_doc.Is_active + ",'" + Helper.ToMySQLDateTimeFormat(enterprise_doc.Expiration_date) + "'," + enterprise_doc.Enterprise_id + ");";
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert enterprise_doc " + enterprise_doc.Document_id);
			_connection.Close ();
		}
	}

	public static void UpdateEnterprise_docs (MySqlConnection _connection, List<Enterprise_docs> enterprise_docs){		
		foreach (Enterprise_docs enterprise_doc in enterprise_docs) {
			_connection.Open ();
			string Query = "UPDATE `enterprise_docs` SET document_id=" + enterprise_doc.Document_id + ", availability=" + enterprise_doc.Availability + ", is_active=" + 
				enterprise_doc.Is_active + ", expiration_date='" + Helper.ToMySQLDateTimeFormat(enterprise_doc.Expiration_date) + "', enterprise_id=" + 
					enterprise_doc.Enterprise_id + " where id=" + enterprise_doc.Id + ";";
			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Update character " + enterprise_doc.Document_id);
			_connection.Close ();
		}
	}
	
	public static void DeleteEnterprise_docs (MySqlConnection _connection, List<Enterprise_docs> enterprise_docs){		
		foreach (Enterprise_docs enterprise_doc in enterprise_docs) {
			_connection.Open ();
			string Query = "DELETE FROM `enterprise_docs` WHERE id="+enterprise_doc.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete enterprise_doc " + enterprise_doc.Document_id);
			_connection.Close ();
		}
	}

}