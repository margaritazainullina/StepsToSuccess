using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class DocumentDAO {
	
	//returns list with all characters from db
	public static List<Document> GetDocuments (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `document`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Document> documents = new List<Document>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			string type =(string)data["type"];
			Int64 id = Convert.ToInt64(data["id"]);
			int path = Convert.ToInt32(data["path"]);

			Document document = new Document(id, title, type, path);
			Debug.Log("Get character "+title);
			documents.Add(document);
		}
		_connection.Close ();
		return documents;
	}
	
	public static void InsertDocuments (MySqlConnection _connection, List<Document> documents){		
		foreach (Document document in documents) {
			_connection.Open ();
			string Query = "INSERT INTO `document` values(" + document.Id + ",'" + document.Title + "','" + 
				document.Type + "'," + document.Path + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert document " + document.Title);
			_connection.Close ();
		}
	}
	public static void UpdateDocuments (MySqlConnection _connection, List<Document> documents){		
		foreach (Document document in documents) {
			_connection.Open ();
			string Query = "UPDATE `document` SET title='" + document.Title + "', type='" + document.Type + 
				"', path=" + document.Path  + " where id=" + document.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update document " + document.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteDocuments (MySqlConnection _connection, List<Document> documents){		
		foreach (Document document in documents) {
			_connection.Open ();
			string Query = "DELETE FROM `document` WHERE id="+document.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete document " + document.Title);
			_connection.Close ();
		}
	}
}