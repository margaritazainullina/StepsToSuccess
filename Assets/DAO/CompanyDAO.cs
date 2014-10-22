using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class CompanyDAO {
	
	//returns list with all characters from db
	public static List<Company> GetCompanies (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `company`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Company> companies = new List<Company>();


		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = Convert.ToString(data["title"]);
			double share = Convert.ToDouble(data["share"]);
			Int64 id = Convert.ToInt64(data["id"]);
			int period = Convert.ToInt32(data["period"]);
			decimal investment = Convert.ToDecimal(data["investment"]);

			Company company = new Company(id, title, share, period, investment);
			Debug.Log("Get company "+title);
			companies.Add(company);
		}
		_connection.Close ();
		return companies;
	}
	public static Company GetCompanyById (int company_id, MySqlConnection _connection){
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `company` WHERE ID="+company_id;
		MySqlDataReader data = command.ExecuteReader();
		Company c = null;		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = Convert.ToString(data["title"]);
			double share = Convert.ToDouble(data["share"]);
			Int64 id = Convert.ToInt64(data["id"]);
			int period = Convert.ToInt32(data["period"]);
			decimal investment = Convert.ToDecimal(data["investment"]);
			
			Company company = new Company(id, title, share, period, investment);
			Debug.Log("Get company "+title);
			c = new Company(1,title,share, period, investment);
		}
		_connection.Close ();
		return c;
	}

	public static void InsertCompanies (MySqlConnection _connection, List<Company> companies){		
		foreach (Company company in companies) {
			_connection.Open ();
			string Query = "INSERT INTO `company` values(" + company.Id + ",'" + 
				company.Title + "'," + company.Share + "," + company.Period + "," + company.Investment + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert company " + company.Title);
			_connection.Close ();
		}
	}

	public static void UpdateCompanies (MySqlConnection _connection, List<Company> companies){		
		foreach (Company company in companies) {
			_connection.Open ();
			string Query = "UPDATE `company` SET title='" + company.Title + "', share=" + 
				company.Share + ", period="  +  company.Period + ", investment=" + company.Investment + " where id=" + company.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Update company " + company.Title);
			_connection.Close ();
		}
	}
	
	public static void DeleteCompanies (MySqlConnection _connection, List<Company> companies){		
		foreach (Company company in companies) {
			_connection.Open ();
			string Query = "DELETE FROM `company` WHERE id="+company.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Delete company " + company.Title);
			_connection.Close ();
		}
	}
}