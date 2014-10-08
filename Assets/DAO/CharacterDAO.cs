using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class CharacterDAO {
	
	//returns list with all characters from db
	public static List<Character> GetCharacters (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `character`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Character> characters = new List<Character>();



		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			string gender =(string)data["gender"];
			Int64 id = Convert.ToInt64(data["id"]);
			int level = Convert.ToInt32(data["level"]);
			Character character = new Character(id, title, gender, level);
			Debug.Log("Get character "+title);
			characters.Add(character);
		}
		_connection.Close ();
		return characters;
	}
	
	public static void InsertCharacters (MySqlConnection _connection, List<Character> characters){		
		foreach (Character c in characters) {
			_connection.Open ();
			string Query = "INSERT INTO `character` values(" + c.Id + ",'" + c.Title + "','" + c.Gender + "'," + c.Level + ");";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Insert character " + c.Title);
			_connection.Close ();
		}
	}
	public static void UpdateCharacters (MySqlConnection _connection, List<Character> characters){		
		foreach (Character c in characters) {
			_connection.Open ();
			string Query = "UPDATE `character` SET title='" + c.Title + "', gender='" + c.Gender + "', level=" + c.Level  + " where id=" + c.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Update character " + c.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteCharacters (MySqlConnection _connection, List<Character> characters){		
		foreach (Character c in characters) {
			_connection.Open ();
			string Query = "DELETE FROM `character` WHERE id="+c.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 Query = Helper.ReplaceQueryVoidWithNulls(Query);
			command.ExecuteReader ();
			Debug.Log ("Delete character " + c.Title);
			_connection.Close ();
		}
	}
}