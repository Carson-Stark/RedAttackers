using UnityEngine;
using System.Collections;

public class upgradeDefence : MonoBehaviour {

	public GameObject upScreen;
	public GameObject sentUpScreen;
	public GameObject gUpScreen;
	public Transform lineRendererSpawn;
	public GameObject BuildRight_LR;
	public GameObject BuildWrong_LR;
	
	public Material right;

	public GameObject Def;

	bool canUpgrade;

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		
		LineRenderer lineRenderer;
		GameObject LR;
		
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)) {
			if (hit.transform.tag == "Defence") {
				LR = (GameObject)Instantiate (BuildRight_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);

				if (hit.transform.parent != null) {
					if (hit.transform.parent.Find("selectBox") != null)
						hit.transform.parent.Find("selectBox").GetComponent<Renderer>().enabled = true;
					else
						hit.transform.parent.parent.Find ("selectBox").GetComponent<Renderer>().enabled = true;
				}
				else
					hit.transform.Find("selectBox").GetComponent<Renderer>().enabled = true;

				Def = hit.transform.gameObject;
				
				canUpgrade = true;
			}
			else {
				GameObject[] defences = GameObject.FindGameObjectsWithTag ("Defence");
				foreach (GameObject defence in defences){
					if (defence.transform.parent != null) {
						if (defence.transform.parent.Find("selectBox") != null)
							defence.transform.parent.Find("selectBox").GetComponent<Renderer>().enabled = false;
						else
							defence.transform.parent.parent.Find ("selectBox").GetComponent<Renderer>().enabled = false;
					}
					else
						defence.transform.Find("selectBox").GetComponent<Renderer>().enabled = false;
				}
				
				canUpgrade = false;
				
				LR = (GameObject)Instantiate (BuildWrong_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);
			}
			   
			lineRenderer = LR.GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, lineRendererSpawn.transform.position);
			lineRenderer.SetPosition(1, hit.point);
			
			Destroy (LR, 0.03f);
		}
		
		if (Input.GetMouseButtonDown(1) && canUpgrade) {
			Cancel (); 

			if (Def.GetComponent<upgrades>().defName == "Sentry")
				Instantiate (sentUpScreen, sentUpScreen.transform.position, sentUpScreen.transform.rotation);
			else 
				Instantiate (upScreen, upScreen.transform.position, upScreen.transform.rotation);
		}

		if (Input.GetKeyDown ("g")) {
			Cancel ();

			Def = GameObject.FindGameObjectWithTag ("Player").transform.Find ("PlaceHolder!!!!").gameObject;
			Instantiate (gUpScreen, gUpScreen.transform.position, gUpScreen.transform.rotation);
		}
		
		if (Input.GetKeyDown("c"))
			Cancel ();
	}

	void Cancel () {
		canUpgrade = false;
		
		GameObject[] defences = GameObject.FindGameObjectsWithTag ("Defence");
		foreach (GameObject defence in defences){
			if (defence.transform.parent != null) {
				if (defence.transform.parent.Find("selectBox") != null)
					defence.transform.parent.Find("selectBox").GetComponent<Renderer>().enabled = false;
				else
					defence.transform.parent.parent.Find ("selectBox").GetComponent<Renderer>().enabled = false;
			}
			else
				defence.transform.Find("selectBox").GetComponent<Renderer>().enabled = false;
		}

		GetComponent<GUI_Buttons>().upgrading = false;
		enabled = false;
	}
}