using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class EnterpriseDAO { //SINGLETONE
	
	//returns list with all characters from db
	public static Enterprise LoadEnterprise(MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `enterprise` WHERE id=" + Character.Instance.Id;
		MySqlDataReader data = command.ExecuteReader();
		
		List<Enterprise> enterprises = new List<Enterprise>();

		Enterprise enterprise = null;

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			string title = Convert.ToString(data["title"]);
			decimal balance = Convert.ToDecimal(data["balance"]);
			double stationary =Convert.ToDouble(data["stationary"]);
			Int64 id = Convert.ToInt64(data["id"]);
			Int16? type = Helper.GetValueOrNull<Int16>(Convert.ToString(data["type"]));
			Int64 taxation_id = Convert.ToInt64(data["taxation_id"]);

			enterprise = new Enterprise(id, title, balance, stationary, type, taxation_id);
			Debug.Log("Get enterprise "+title);
			enterprises.Add(enterprise);
		}

		_connection.Close ();
		return enterprise;
	}

	public static void InsertEnterprises (MySqlConnection _connection, List<Enterprise> enterprises){		
		foreach (Enterprise enterprise in enterprises) {
			_connection.Open ();
			string Query = "INSERT INTO enterprise(title,balance,stationary,type,taxation_id) values('" + enterprise.Title + "'," + enterprise.Balance + "," + 
				enterprise.Stationary + "," + enterprise.Type + "," + enterprise.Taxation_id + ");";

			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Insert enterprise " + enterprise.Title);
			_connection.Close ();
		}
	}

	public static void UpdateEnterprise (MySqlConnection _connection){		
			_connection.Open ();
		string Query = "UPDATE `enterprise` SET title='" + Character.Instance.Enterprise.Title + "', balance=" + Character.Instance.Enterprise.Balance + ", stationary=" + Character.Instance.Enterprise.Stationary + 
			", type=" + Character.Instance.Enterprise.Type + ", taxation_id=" + Character.Instance.Enterprise.Taxation_id + " where id=" + Character.Instance.Enterprise.Id + ";";

			Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
		Debug.Log ("Update enterprise " + Character.Instance.Enterprise.Title);
			_connection.Close ();
	}
	
	public static void DeleteEnterprises (MySqlConnection _connection, List<Enterprise> enterprises){		
		foreach (Enterprise enterprise in enterprises) {
			_connection.Open ();
			string Query = "DELETE FROM `enterprise` WHERE id="+enterprise.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete enterprise " + enterprise.Title);
			_connection.Close ();
		}
	}

	public static Enterprise GetEnterpriseByProject(MySqlConnection connection, Project project){	
		Enterprise enterprise = null;

		try
		{
			connection.Open ();
			string Query = "Select Distinct Enterprise.* FROM enterprise, team_member, employee, project " +
				"WHERE Project.Id = Team_member.project_id AND Team_member.Employee_id = Employee.Id " +
				"AND Enterprise.Id = Employee.Enterprise_id AND Project.Id=" + project.Id + ";";
			MySqlCommand command = new MySqlCommand (Query, connection);
			
			MySqlDataReader data = command.ExecuteReader();
			
			while (data.Read()) {
				Int64 id = Convert.ToInt32(data ["id"]);
				int hours_worked = Convert.ToInt32(data ["hours_worked"]);
				double qualification = Convert.ToInt32 (data ["qualification"]);
				string title = Convert.ToString(data["title"]);
				decimal balance = Convert.ToDecimal(data["balance"]);
				double stationary =Convert.ToDouble(data["stationary"]);
				Int16? type = Helper.GetValueOrNull<Int16>(Convert.ToString(data["type"]));
				Int64 taxation_id = Convert.ToInt64(data["taxation_id"]);

				enterprise = new Enterprise(id,title,balance,stationary,type,taxation_id);
			}
			//MB create objects of employee to use update employeedao and set there qualification
			
		}catch(Exception ex) {
			return null;
		}
		finally
		{
			connection.Close();
		}
		return enterprise;
	}

}