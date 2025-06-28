using UnityEngine;
using System.Collections;

public class removeDefence : MonoBehaviour {

	public GameObject explosion;
	public AudioClip explosionAudio;

	public Transform lineRendererSpawn;
	public GameObject BuildRight_LR;
	public GameObject BuildWrong_LR;
	
	public Material right;

	bool canRemove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		
		LineRenderer lineRenderer;
		GameObject LR;

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)){
			if (hit.transform.tag == "Defence"){
				LR = (GameObject)Instantiate (BuildRight_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);

				if (hit.transform.parent != null) {
					if (hit.transform.parent.Find("selectBox") != null)
						hit.transform.parent.Find("selectBox").GetComponent<Renderer>().enabled = true;
					else
						hit.transform.parent.parent.Find ("selectBox").GetComponent<Renderer>().enabled = true;
				}
				else
					hit.transform.Find("selectBox").GetComponent<Renderer>().enabled = true;


				canRemove = true;
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

				canRemove = false;

				LR = (GameObject)Instantiate (BuildWrong_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);
			}
			
			lineRenderer = LR.GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, lineRendererSpawn.transform.position);
			lineRenderer.SetPosition(1, hit.point);
			
			Destroy (LR, 0.03f);
		}

		if (Input.GetMouseButtonDown(1) && canRemove){
			try {
				Destroy (hit.transform.parent.parent.gameObject);
			}
			catch {
				try {
					Destroy (hit.transform.parent.gameObject);
				}
				catch {
					Destroy (hit.transform.gameObject);
				}
			}

			GameObject Ex = (GameObject)Instantiate (explosion, hit.transform.position, hit.transform.rotation);
			AudioSource.PlayClipAtPoint (explosionAudio, hit.transform.position, 0.2f);
			Destroy (Ex, 3);

			GetComponent<GUI_Buttons>().removing = false;
			enabled = false;
		}

		if (Input.GetKeyDown("c"))
			Cancel ();
	}

	void Cancel () {
		canRemove = false;

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

		GetComponent<GUI_Buttons>().removing = false;
		enabled = false;
	}
}
