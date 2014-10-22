//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

namespace Model
{
	public class Enterprise
	{
		public Int64 Id { get; set; }
		public string Title { get; set; }
		public decimal Balance { get; set; }
		public double Stationary { get; set; }
		public Int16? Type { get; set; }
		public Int64 Taxation_id { get; set; }

		public virtual Taxation Taxation { get; set; }

		public virtual ICollection<Asset> Assets {get; set;}
		public virtual ICollection<Competitor> Competitors {get; set;}
		public virtual ICollection<Employee> Employees {get; set;}
		public virtual ICollection<Enterprise_docs> Enterprise_docs {get; set;}
		public virtual ICollection<Equipment> Equipment {get; set;}
		public virtual ICollection<Revenue> Revenues {get; set;}
		public virtual ICollection<Enterprise_equipment> Enterprise_equipment {get; set;}

		public Enterprise (Int64 id, string title, decimal balance, double stationary,
		                   Int16? type, Int64 taxation_id)
		{
			Id = id;
			Title = title;
			Balance = balance;
			Stationary = stationary;
			Type = type;
			Taxation_id = taxation_id;
		}

		//5 types of enterprise creation - 
		//private assets, investment, bank credit, private assets+investment, private assets+bank credit
		public void CreateEnterpriseWithPrivateAssets (decimal personal, MySqlConnection connection){
			Balance = personal;			
			Asset a = new Asset (1, personal, DateTime.Now, Id);
			AssetDAO.InsertAssets(connection, new List<Asset>{a});
		}

		public void CreateEnterpriseWithInvestment (decimal investment, Company investor, MySqlConnection connection){
			RecieveInvestment(investor, connection);
		}

		public void RecieveInvestment(Company investor, MySqlConnection connection){
			Balance += investor.Investment;
			Asset a = new Asset (1, investor.Investment, DateTime.Now, Id);
			AssetDAO.InsertAssets(connection, new List<Asset>{a});
		}		

		public void CreateEnterpriseWithCredit (decimal credit, Company bank, MySqlConnection connection){
			RecieveCredit(bank, connection);
		}

		public void RecieveCredit(Company bank, MySqlConnection connection){
			Balance += bank.Investment;
			Asset a = new Asset (1, bank.Investment, DateTime.Now, Id);
			AssetDAO.InsertAssets(connection, new List<Asset>{a});
	    }

		public void  CreateEnterpriseWithPrivateAssetsAndInvestment(decimal personal , decimal investment, Company investor, MySqlConnection connection){
			CreateEnterpriseWithInvestment (investment, investor, connection);
			CreateEnterpriseWithPrivateAssets (personal, connection);
		}

		public void CreateEnterpriseWithPrivateAssetsAndCredit (decimal personal, decimal credit, Company bank, MySqlConnection connection){
			CreateEnterpriseWithCredit (credit, bank, connection);
			CreateEnterpriseWithPrivateAssets (personal, connection);
		}

		public decimal TotalIncomeForPeriod(int days){
			//calculation of annual income
			//Доход = выручка от продаж за выбранный период  - себестоимость продукции, услуг за выбранный период (в стоимостном выражении)
			//не путать с выручкой! 
			//доходы/расходы (себестоиость, выручка, аренда, амортизация, вообще все)- в Asset, выручка за реализацию продукции в Revenue
			decimal revenue = 0;
			foreach (Asset a in Assets) {
				if (a.Asset_date <= DateTime.Today.AddDays (days)) {	
					revenue += a.Value;
				}
			}
			return revenue;
		}

		//setting taxation type for the enterprise
		//if return false - this type can't be applied
		public Boolean SetTaxationType(int type, MySqlConnection connection){
						List<Taxation> taxations = TaxationDAO.GetTaxations (connection);
			decimal revenue = TotalIncomeForPeriod (365);
						Boolean b = true;
						
						switch (type) {
						case 1:
								{
										//type of production of the enterprise doesn't match ones stated in taxation type
										b = false;
										break;
								}
						case 2:
								{
										//if selected wrong taxation for a legal/private body
										if (this.Type == 1)
												b = false;
										//if enterprise exceeds number of its employees
										if (this.Employees.Count > 10)
												b = false;
										//if enterprise revenue exceeds sated in taxation type
										if (revenue > 1000000)
												b = false;
				
										break;
								}
						case 3:
								{
										//type of production of the enterprise doesn't match ones stated in taxation type
										return false;
								}
						case 4:
								{
										if (this.Type == 0)
												b = false;
										if (this.Employees.Count > 50)
												b = false;
										if (revenue > 5000000)
												b = false;
										break;
								}
						case 5:
								{	
										//type of production of the enterprise doesn't match ones stated in taxation type
										b = false;
										break;
								}
						case 6:
								{	
										if (this.Type == 1)
												b = false;
										if (this.Employees.Count > 50)
												b = false;
										if (revenue > 20000000)
												b = false;
										break;
								}
						}

						this.Type = (Int16?)type;
						return true;
				}
			

		public String CompleteDocuments(MySqlConnection connection){
			//if legal body - complete form #1, if private - form #10
			String form = "";
			String document = "";

			if (Type == 1) {
				form = System.IO.File.ReadAllText (@"Assets\Documents\registration_form_1.txt");
				String ss = @"\{\d+\}";
				char[] a= ss.ToCharArray();
				string[] s = form.Split(a);
				document = s[0]+this.Title+
					s[1]+"\"ООО\""+
						s[2]+"просп. Ленина, 9А, Харків, Україна"+
						s[3]+this.Balance+
						s[4]+DateTime.Now.ToString ("dd.MM.yyyy")+
						s[5]+"Иванов Иван Иванович"+
						s[6]+DateTime.Now.ToString ("dd.MM.yyyy");
			}
			else{
				form = System.IO.File.ReadAllText (@"Assets\Documents\registration_form_10.txt");
				String ss = @"\{\d+\}";
				char[] a= ss.ToCharArray();
				string[] s = form.Split(a);
				document = 
					s[0]+"Иванов Иван Иванович"+
						s[1]+"11111111"+
						s[2]+"MT 111111"+
						s[3]+"Україна"+
						s[4]+"Иванов Иван Иванович"+
						s[5]+"просп. Ленина, 9А, Харків, Україна"+
						s[6]+"72.22.0"+
						s[7]+"Інші види діяльності у сфері розроблення програмного забезпечення"+
						s[8]+"Иванов Иван Иванович"+
						s[9]+DateTime.Now.ToString ("dd.MM.yyyy");
			}
			Debug.Log("Enterprise.CompleteDocuments(): "+document);
			return document;}		

		public void PaySalary(MySqlConnection connection, Employee employee, int hours_worked, DateTime date)
		{
			Salary_payment salary_payment = new Salary_payment (0, DateTime.Now, hours_worked, 
			                                                    (decimal?)(hours_worked * employee.Qualification), employee.Id);
			Salary_paymentDAO.InsertSalary_payments (connection, new List<Salary_payment> () { salary_payment });

			List<Project> projects = ProjectDAO.GetProjects (connection);
			string Query;


			foreach (Project project in projects) 
			{
				Query = "SELECT sum(Salary_payment.salary) as salary FROM Salary_payment, Employee, Team_member, Project" +
					"WHERE Employee.Id=Salary_payment.Employee_id AND" +
						"Employee.Id = Team_member.Employee_id AND Team_member.Project_id = Project.id " +
						"AND Salary_payment.`date`='" + date + "' AND Project.id=" + project.Id + ";";
				MySqlCommand command = connection.CreateCommand();
				command.CommandText = Query;
				MySqlDataReader data = command.ExecuteReader();

				while (data.Read()){
					project.Expenditures += Convert.ToDecimal(data["salary"]);
				}

				this.Balance -= project.Expenditures;
				PayUST (connection, project.Expenditures);
			}
			ProjectDAO.UpdateProjects (connection, projects);
		}
		
		public void PayUST(MySqlConnection connection, decimal amountOfSalaryPaidToday){
						//Balance -= ∑(Salary_payment. Salary)*0,036-зарплата_предпринимателю*0,347(???)
						if (Type == 0) {
								//if personal body
								Balance -= amountOfSalaryPaidToday * (decimal)0.036;
						} else {
								Balance -= amountOfSalaryPaidToday * (decimal)0.376;			
						}
				}

		public void LoanDisbursement(MySqlConnection connection, MySqlConnection _connection){
			//budget -= (investment*share)/period
			foreach (Asset a in Assets) {
				//if a loan hasn't been disbursed yet
				if(a.Service.Period<a.Service.PeriodsPaid){
					a.Service.PeriodsPaid++;
					Balance -= (a.Service.Company.Investment*(decimal)a.Service.Company.Share)/(decimal)a.Service.Period;
				}
			}
		}

		public void SharePayout(MySqlConnection connection){
			//budget -= ∑ (revenue.Value за последний месяц)*share
			foreach (Asset a in Assets) {
				decimal income = TotalIncomeForPeriod(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
				Balance -= income*(decimal)a.Service.Company.Share;
			}
		}
	}
}