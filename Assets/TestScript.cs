using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		string source ="Server=localhost;"+
				"Database=sts;"+
				"User ID=root;"+
				"Pooling=false;"+
				"Password=";

		MySqlConnection connection = new MySqlConnection(source);

		AssetTest (connection);
		CharacterTest (connection);
		CompanyTest (connection);
		CompetitorTest (connection);
		DocumentTest (connection);
		EmployeeTest (connection);
		EnterpriseTest (connection);
		/*Enterprise_docsTest (connection);


		Enterprise_equipmentTest (connection);
		EquipmentTest (connection);
		Human_resourcesTest (connection);
		ProductTest (connection);
		ProjectTest (connection);
		Project_stageTest (connection);
		PurchaseTest (connection);		
		RevenueTest (connection);
		RoleTest (connection);
		Salary_paymentTest (connection);
		ServiceTest (connection);		
		TaxationTest (connection);
		Team_memberTest (connection);*/
	}

	void AssetTest(MySqlConnection connection)
	{
		List<Asset> assets = new List<Asset>();
		
		assets.Add(new Asset(2, 5, new DateTime(2014,09,25), 1));
		
		AssetDAO.InsertAssets (connection, assets);
		
		List<Asset> assets2 = AssetDAO.GetAssets(connection);
		
		assets2[1].Value = 888;
		
		AssetDAO.UpdateAssets (connection, assets2);
		
		assets2 = AssetDAO.GetAssets(connection);
		
		AssetDAO.DeleteAssets (connection, assets);
		
		assets2 = AssetDAO.GetAssets(connection);
	}

	void CharacterTest(MySqlConnection connection)
	{
		List<Character> characters = new List<Character>();
		
		characters.Add(new Character(2, "Dan the awesome","male",3));
		
		CharacterDAO.InsertCharacters (connection, characters);
		
		List<Character> characters2 = CharacterDAO.GetCharacters(connection);
		
		characters2[1].Title = "Dan";
		
		CharacterDAO.UpdateCharacters (connection, characters2);
		
		characters2 = CharacterDAO.GetCharacters(connection);
		
		CharacterDAO.DeleteCharacters (connection, characters);
		
		characters2 = CharacterDAO.GetCharacters(connection);
	}

	void CompanyTest(MySqlConnection connection)
	{
		List<Company> companies = new List<Company>();
		
		companies.Add(new Company(2,"DigiSoft", 2.5, 4, 20.50M));
		
		CompanyDAO.InsertCompanies (connection, companies);
		
		List<Company> companies2 = CompanyDAO.GetCompanies(connection);
		
		companies2[1].Title = "Dan";
		
		CompanyDAO.UpdateCompanies (connection, companies2);
		
		companies2 = CompanyDAO.GetCompanies(connection);
		
		CompanyDAO.DeleteCompanies (connection, companies);
		
		companies2 = CompanyDAO.GetCompanies(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void DocumentTest(MySqlConnection connection)
	{
		List<Document> documents = new List<Document>();
		
		documents.Add(new Document(2, "Dandoc", "special", 9));
		
		DocumentDAO.InsertDocuments (connection, documents);
		
		List<Document> documents2 = DocumentDAO.GetDocuments(connection);
		
		documents2[1].Title = "newDanDoc";
		
		DocumentDAO.UpdateDocuments (connection, documents2);
		
		documents2 = DocumentDAO.GetDocuments(connection);
		
		DocumentDAO.DeleteDocuments (connection, documents);
		
		documents2 = DocumentDAO.GetDocuments(connection);
	}

	void EmployeeTest(MySqlConnection connection)
	{
		List<Employee> employees = new List<Employee>();
		
		employees.Add(new Employee(2, "SlaveZ", 8, 100.75M, 1, 1));
		
		EmployeeDAO.InsertEmployees (connection, employees);
		
		List<Employee> employees2 = EmployeeDAO.GetEmployees(connection);
		
		employees2[1].Title = "NewSlave";
		
		EmployeeDAO.UpdateEmployees (connection, employees2);
		
		employees2 = EmployeeDAO.GetEmployees(connection);
		
		EmployeeDAO.DeleteEmployees (connection, employees);
		
		employees2 = EmployeeDAO.GetEmployees(connection);
	}

	void EnterpriseTest(MySqlConnection connection)
	{
		List<Enterprise> enterprises = new List<Enterprise>();
		
		enterprises.Add(new Enterprise(2, "MyEnterprise", 500.23M, 2.5, 4, 1));
		
		EnterpriseDAO.InsertEnterprises (connection, enterprises);
		
		List<Enterprise> enterprises2 = EnterpriseDAO.GetEnterprises(connection);
		
		enterprises2[1].Title = "NewInterpise";
		
		EnterpriseDAO.UpdateEnterprises (connection, enterprises2);
		
		enterprises2 = EnterpriseDAO.GetEnterprises(connection);
		
		EnterpriseDAO.DeleteEnterprises (connection, enterprises);
		
		enterprises2 = EnterpriseDAO.GetEnterprises(connection);
	}

	void Enterprise_docsTest(MySqlConnection connection)
	{
		List<Enterprise_docs> enterprise_docs = new List<Enterprise_docs>();
		
		enterprise_docs.Add(new Enterprise_docs(true, false, new DateTime(2014,10,01),1,1));
		
		Enterprise_docsDAO.InsertEnterprise_docs (connection, enterprise_docs);
		
		List<Enterprise_docs> enterprise_docs2 = Enterprise_docsDAO.GetEnterprise_docs(connection);
		
		enterprise_docs2[1].Is_active = true;
		
		Enterprise_docsDAO.UpdateEnterprise_docs(connection, enterprise_docs2);
		
		enterprise_docs2 = Enterprise_docsDAO.GetEnterprise_docs(connection);
		
		Enterprise_docsDAO.DeleteEnterprise_docs(connection, enterprise_docs);
		
		enterprise_docs2 = Enterprise_docsDAO.GetEnterprise_docs(connection);
	}

	/*
	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}

	void CompetitorTest(MySqlConnection connection)
	{
		List<Competitor> competitors = new List<Competitor>();
		
		competitors.Add(new Competitor(2, "ComptitorFirm", 2.5, 1));
		
		CompetitorDAO.InsertCompetitors (connection, competitors);
		
		List<Competitor> competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		competitors2[1].Title = "ComptitorFirmBrandNew";
		
		CompetitorDAO.UpdateCompetitors (connection, competitors2);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
		
		CompetitorDAO.DeleteCompetitors (connection, competitors);
		
		competitors2 = CompetitorDAO.GetCompetitors(connection);
	}
	 */

}
