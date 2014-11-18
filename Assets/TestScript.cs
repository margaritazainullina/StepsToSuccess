using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;



public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//mb connection's singletone too?
		string source ="Server=localhost;"+
				"Database=sts;"+
				"User ID=root;"+
				"Pooling=false;"+
				"Password=";

		MySqlConnection connection = new MySqlConnection(source);

		/*****************LOADING THE GAME***********************/

		CharacterDAO.LoadCharacterByName(connection, "Rita");

		Enterprise enterprise = EnterpriseDAO.LoadEnterprise(connection);

		List<Employee> employees = EmployeeDAO.LoadEmployees(connection, enterprise);
		List<Competitor> competitors = CompetitorDAO.LoadCompetitors(connection, enterprise);
		List<Enterprise_docs> enterprise_docs = Enterprise_docsDAO.LoadEnterprise_docs(connection, enterprise);
		List<Asset> assets = AssetDAO.LoadAssets(connection, enterprise);
		List<Enterprise_equipment> enterprise_equipment = Enterprise_equipmentDAO.LoadEnterprise_equipment(connection, enterprise);
		List<Project> projects = ProjectDAO.LoadProjects(connection, enterprise);
		List<Service> services = ServiceDAO.LoadServices(connection, enterprise);

		List<Taxation> taxations = TaxationDAO.GetTaxations (connection);
		List<Role> roles = RoleDAO.GetRoles(connection);
		List<Equipment> equipment = EquipmentDAO.GetEquipment(connection);
		List<Document> documents = DocumentDAO.GetDocuments(connection);
		List<Company> companies = CompanyDAO.GetCompanies (connection);

		List<Team_member> team_members = Team_memberDAO.LoadTeam_members(connection, enterprise);
		List<Salary_payment> salary_payments = Salary_paymentDAO.LoadSalary_payments(connection, enterprise);
		List<Product> product = ProductDAO.LoadProducts(connection, enterprise);
		Project_stage project_stage = Project_stageDAO.LoadProject_stages(connection, enterprise);

		enterprise.Projects = projects;
		enterprise.Employees = employees;
		enterprise.Enterprise_docs = enterprise_docs;
		enterprise.Competitors = competitors;
		enterprise.Assets = assets;
		enterprise.Services = services;
 		enterprise.Enterprise_equipment = enterprise_equipment;
		enterprise.Salary_payments = salary_payments;

		foreach (Taxation taxation in taxations)
		{
			if(taxation.Id == enterprise.Taxation_id)
			{
				enterprise.Taxation = taxation;
				break;
			}
		}

		foreach (Enterprise_equipment enterprise_equip in enterprise_equipment)
		{
			enterprise_equip.Equipment = EquipmentDAO.GetEquipmentById(connection, enterprise_equip.Equipment_id);
		}
		foreach (Service service in services)
		{
			service.Company = CompanyDAO.GetCompanyById(connection, service.Company_id);
		}
		foreach (Enterprise_docs enterprise_doc in enterprise_docs)
		{
			enterprise_doc.Documents = DocumentDAO.GetDocumentsById(connection, enterprise_doc.Document_id);
		}
		foreach (Employee employee in employees)
		{
			employee.Role = RoleDAO.GetRolesById(connection, employee.Role_id);
		}

		foreach (Project project in projects)
		{
			project.Team_members = Team_memberDAO.GetTeam_membersById(connection, project);
		}
		foreach (Employee employee in employees)
		{
			employee.Team_members = Team_memberDAO.GetTeam_membersById(connection, employee);
		}

		foreach (Project project in projects)
		{
			project.Product = ProductDAO.GetProductByProjectId(connection, project);
		}
		foreach (Project project in projects)
		{
			project.Project_stage = Project_stageDAO.GetProject_stageByProjectId(connection, project);
		}

		Character.Instance.Enterprise = enterprise;

		//enterprise.Employees[0].Role;

		/*****************LOADING THE GAME***********************/

		//FileStream fs = new FileStream ("stssavegame", FileMode.Open);
		//character = null;
		//character = LoadDataFromFile (fs);
		//Debug.Log ("!" + character.Title + "ent " + character.Enterprise.Title + " employee " + character.Enterprise.Employees [0].Title);

		/*****************SAVING THE GAME************************/
		//FileStream fs = new FileStream ("stssavegame", FileMode.Create);

		//SaveDataToFile(character, fs);

		CharacterDAO.UpdateCharacter (connection);
		EnterpriseDAO.UpdateEnterprise(connection);

		EmployeeDAO.UpdateEmployees (connection);
		CompetitorDAO.UpdateCompetitors (connection);
		Enterprise_docsDAO.UpdateEnterprise_docs (connection);
		AssetDAO.UpdateAssets (connection);
		Enterprise_equipmentDAO.UpdateEnterprise_equipment (connection);
		ProjectDAO.UpdateProjects (connection);
		ServiceDAO.UpdateServices (connection);
		TaxationDAO.UpdateTaxation (connection);
		//RoleDAO.UpdateRoles (connection, character.Enterprise.Employees.); won't change 
		/*EquipmentDAO.UpdateEquipment (connection, character.Enterprise.Employees);*/
		DocumentDAO.UpdateDocuments (connection);
		CompanyDAO.UpdateCompanies (connection);
		Salary_paymentDAO.UpdateAssets (connection);
		ProductDAO.UpdateProducts (connection);
		Project_stageDAO.UpdateProject_stages (connection);
	}
}
