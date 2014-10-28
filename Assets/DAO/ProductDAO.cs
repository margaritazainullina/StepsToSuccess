using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class ProductDAO {
	
	//returns list with all characters from db
	public static List<Product> GetProducts (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `product`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Product> products = new List<Product>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);
			double quality = Convert.ToDouble(data["quality"]);
			decimal prime_cost = Convert.ToDecimal(data["prime_cost"]);
			Int64 project_id = Convert.ToInt64(data["project_id"]);

			Product product = new Product(id, title, price, quality, prime_cost, project_id);
			Debug.Log("Get product "+title);
			products.Add(product);
		}
		_connection.Close ();
		return products;
	}

	public static Product GetProductByProjectId (MySqlConnection _connection, Project project){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM product WHERE project_id=" + project.Id + ";";
		MySqlDataReader data = command.ExecuteReader();
		
		Product product = new Product();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);
			double quality = Convert.ToDouble(data["quality"]);
			decimal prime_cost = Convert.ToDecimal(data["prime_cost"]);
			Int64 project_id = Convert.ToInt64(data["project_id"]);
			
			product = new Product(id, title, price, quality, prime_cost, project_id);
			Debug.Log("Get product "+title);
		}
		_connection.Close ();
		return product;
	}

	public static List<Product> LoadProducts (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT product.* FROM product, project WHERE " +
			"product.project_id=project.id AND project.enterprise_id ="+ enterprise.Id +";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Product> products = new List<Product>();
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = (string)data["title"];
			Int64 id = Convert.ToInt64(data["id"]);
			decimal price = Convert.ToDecimal(data["price"]);
			double quality = Convert.ToDouble(data["quality"]);
			decimal prime_cost = Convert.ToDecimal(data["prime_cost"]);
			Int64 project_id = Convert.ToInt64(data["project_id"]);
			
			Product product = new Product(id, title, price, quality, prime_cost, project_id);
			Debug.Log("Get product "+title);
			products.Add(product);
		}
		_connection.Close ();
		return products;
	}

	public static void InsertProducts (MySqlConnection _connection, List<Product> products){		
		foreach (Product product in products) {
			_connection.Open ();
			string Query = "INSERT INTO `product` values(" + product.Id + ",'" + product.Title + "'," + 
				product.Price + "," + product.Quality + "," + product.Prime_cost + "," + product.Project_id + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert product " + product.Title);
			_connection.Close ();
		}
	}
	public static void UpdateProducts (MySqlConnection _connection, List<Product> products){		
		foreach (Product product in products) {
			_connection.Open ();
			string Query = "UPDATE `product` SET title='" + product.Title + "', price=" + product.Price + 
				", quality=" + product.Quality + ", prime_cost=" + product.Prime_cost +", project_id=" + product.Project_id + " where id=" + product.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Update product " + product.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteProducts (MySqlConnection _connection, List<Product> products){		
		foreach (Product product in products) {
			_connection.Open ();
			string Query = "DELETE FROM `product` WHERE id="+product.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete product " + product.Title);
			_connection.Close ();
		}
	}

}