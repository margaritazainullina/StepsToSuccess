using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class Salary_paymentDAO {
	
	//returns list with all characters from db
	public static List<Salary_payment> GetSalary_payments (MySqlConnection _connection){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT * FROM `salary_payment`";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Salary_payment> salary_payments = new List<Salary_payment>();

		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			DateTime date = Convert.ToDateTime(data["date"]);
			int? hours_worked = Helper.GetValueOrNull<int>(Convert.ToString(data["hours_worked"]));
			decimal? salary = Helper.GetValueOrNull<decimal>(Convert.ToString(data["salary"]));
			Int64 employee_id = Convert.ToInt64(data["employee_id"]);

			Salary_payment salary_payment = new Salary_payment(id, date, hours_worked, salary, employee_id, false);
			Debug.Log("Get salary_payment id="+id);
			salary_payments.Add(salary_payment);
		}
		_connection.Close ();
		return salary_payments;
	}

	public static List<Salary_payment> LoadSalary_payments (MySqlConnection _connection, Enterprise enterprise){		
		_connection.Open ();
		//retrieve from db
		MySqlCommand command = _connection.CreateCommand();
		command.CommandText = "SELECT salary_payment.* FROM employee, team_member, salary_payment WHERE " +
			"Salary_payment.employee_id=employee.id AND employee.enterprise_id ="+ enterprise.Id +";";
		MySqlDataReader data = command.ExecuteReader();
		
		List<Salary_payment> salary_payments = new List<Salary_payment>();
		
		//read data from dataReader and form list of Character instances
		while (data.Read()){
			Int64 id = Convert.ToInt64(data["id"]);
			DateTime date = Convert.ToDateTime(data["date"]);
			int? hours_worked = Helper.GetValueOrNull<int>(Convert.ToString(data["hours_worked"]));
			decimal? salary = Helper.GetValueOrNull<decimal>(Convert.ToString(data["salary"]));
			Int64 employee_id = Convert.ToInt64(data["employee_id"]);
			
			Salary_payment salary_payment = new Salary_payment(id, date, hours_worked, salary, employee_id, false);
			Debug.Log("Get salary_payment id="+id);
			salary_payments.Add(salary_payment);
		}
		_connection.Close ();
		return salary_payments;
	}

	public static void InsertSalary_payments (MySqlConnection _connection, List<Salary_payment> salary_payments){		
		foreach (Salary_payment salary_payment in salary_payments) {
			_connection.Open ();
			string Query = "INSERT INTO salary_payment(`date`,hours_worked, salary,employee_id) values('" + Helper.ToMySQLDateTimeFormat(salary_payment.Date) + "'," + salary_payment.Hours_worked + "," + 
				salary_payment.Salary + "," + salary_payment.Employee_id + ");";
			
			
			Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
			
			MySqlCommand command = new MySqlCommand (Query, _connection);

			command.ExecuteReader ();
			Debug.Log ("Insert salary_payment id="+salary_payment.Id);
			_connection.Close ();
		}
	}
	public static void UpdateAssets (MySqlConnection _connection){		
		foreach (Employee employee in Character.Instance.Enterprise.Employees) 
		{
			if(employee.Salary_payments == null) continue;
			foreach (Salary_payment salary_payment in employee.Salary_payments) {
					if (salary_payment.isNew)
							continue;
					_connection.Open ();
					string Query = "UPDATE `salary_payment` SET date='" + Helper.ToMySQLDateTimeFormat (salary_payment.Date) + "', hours_worked=" + salary_payment.Hours_worked + 
							", salary=" + salary_payment.Salary + ", employee_id=" + salary_payment.Employee_id + " where id=" + salary_payment.Id + ";";


					Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);

					MySqlCommand command = new MySqlCommand (Query, _connection);

					command.ExecuteReader ();
					Debug.Log ("Update salary_payment id=" + salary_payment.Id);
					_connection.Close ();
			}
		}
	}
	
	public static void DeleteAssets (MySqlConnection _connection, List<Salary_payment> salary_payments){		
		foreach (Salary_payment salary_payment in salary_payments) {
			_connection.Open ();
			string Query = "DELETE FROM `salary_payment` WHERE id="+salary_payment.Id+ ";";
			MySqlCommand command = new MySqlCommand (Query, _connection);
 
			command.ExecuteReader ();
			Debug.Log ("Delete salary_payment id="+salary_payment.Id);
			_connection.Close ();
		}
	}

}