using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class DBConnection {
	static string source;		
	public static MySqlConnection connection;
	// Use this for initialization
	public static void ConnectBase(string _source)
	{
		connection = new MySqlConnection (_source);
	}
	public static void connect () {
		source ="Server=localhost;"+
		    	"Database=sts;"+
				"User ID=root;"+
				"Pooling=false;"+
				"Password=";
		ConnectBase(source);
		Debug.Log("Connected to db.");
	}	

}