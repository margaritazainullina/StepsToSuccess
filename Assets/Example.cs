using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class Example : MonoBehaviour {
	private string source;
	private MySqlConnection connection;
	// Use this for initialization
	void ConnectBase(string _source)
	{
		connection = new MySqlConnection (_source);
		connection.Open ();
	}
	void Start () {
		source="Server=localhost;"+
			"Database=unitybase;"+
				"User ID=root;"+
				"Pooling=false;"+
				"Password=";
		ConnectBase (source);
		ReadCredentials (connection);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ReadCredentials (MySqlConnection _connection){
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "Select * from login";
		MySqlDataReader data = command.ExecuteReader();
		
		while (data.Read()){
			string login = (string)data["login"];
			string password=(string)data["password"];
			
			Debug.Log("User "+login);
			Debug.Log("Password "+password);
			
		}
}
}