using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class CharacterDAO {
	
	//returns list with all characters from db
	public static void LoadCharacterByName (MySqlConnection _connection, string name){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `character` WHERE title='" + name + "';";
		MySqlDataReader data = command.ExecuteReader();
		

				//read data from dataReader and form list of Character instances
		while (data.Read()){
			Character.Instance.Title = (string)data["title"];
			Character.Instance.Gender =(string)data["gender"];
			Character.Instance.Id = Convert.ToInt64(data["id"]);
			Character.Instance.Level = Convert.ToInt32(data["level"]);
			 
			Debug.Log("Get character "+Character.Instance.Title);
		}
		_connection.Close ();
	}
	
	public static void InsertCharacter (MySqlConnection _connection){		

			_connection.Open ();
			string Query = "INSERT INTO character(title,gender,level,id_enterprise) values('" + Character.Instance.Title 
			+ "','" + Character.Instance.Gender + "'," + Character.Instance.Level + ");";
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
			
			command.ExecuteReader ();
			Debug.Log ("Insert character " + Character.Instance.Title);
			_connection.Close ();

	}

	public static void UpdateCharacter (MySqlConnection _connection){		
			_connection.Open ();
		string Query = "UPDATE `character` SET title='" + Character.Instance.Title + "', gender='" + Character.Instance.Gender 
			+ "', level=" + Character.Instance.Level  + " where id=" + Character.Instance.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
		Debug.Log ("Update character " + Character.Instance.Title);
			_connection.Close ();
	}
	
	public static void DeleteCharacters (MySqlConnection _connection, List<Character> characters){		
		foreach (Character c in characters) {
			_connection.Open ();
			string Query = "DELETE FROM `character` WHERE id="+c.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete character " + c.Title);
			_connection.Close ();
		}
	}
}