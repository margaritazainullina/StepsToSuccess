using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		/*
		if(Input.GetMouseButton(0))
		{
			Application.LoadLevel("Scene1");

		}
		*/
	}

	void OnMouseDown()
	{
		Application.LoadLevel("Scene1");

	}
}
