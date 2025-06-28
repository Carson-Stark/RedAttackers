using UnityEngine;
using System.Collections;
using System.Reflection;

public class buildScreen : MonoBehaviour {

	public GameObject[] modernDefences;

	public AudioClip ButtonClick;
	public AudioClip notEnoughMoney;
	public GameObject BuildRight_LR;
	public GameObject BuildWrong_LR;
	public Material wrong;
	public Material right;

	Camera cam;
	GameObject lineRendererSpawn;
	GameObject chosenDefence;
	GameObject defence;

	GameObject player;

	GameObject LR;
	bool cantPlace;
	bool building;
	bool novis;
	int price;

	void Awake () {
		building = false;
		cantPlace = false;
		novis = false;
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");

		lineRendererSpawn = GameObject.Find("lineRendererSpawn");

		cam = Camera.main; 

		cam.GetComponent<stopGame>().StopGame();
	}
	
	// Update is called once per frame
	void Update () {
		cam.GetComponent<GUI_Buttons>().building = building;

		if (building){ //if we are placing a defence...

			moveDefence(); //move the defence based on the user's input
			
			if (Input.GetKeyDown ("c")) //cancel the turret placement if user presses "c"
				Cancel();
			
			if (!cantPlace){ //if we can place the defence and we right clicked...
				if (Input.GetMouseButtonDown(1)){
					building = false;

					defence.transform.Find ("selectBox").GetComponent<Renderer>().enabled = false; //reenable all functions disabled while placing
					if (!novis)
						defence.transform.Find ("visualRaduis").GetComponent<Renderer>().enabled = false; //reenable all functions disabled while placing

					defence.GetComponent<enableScript>().enable = true;
					foreach (Transform child in defence.transform) {
						if (child.GetComponent<enableScript>() != null)
							child.GetComponent<enableScript>().enable = true;
					}

					if (defence.GetComponent<Collider>() != null)
						defence.GetComponent<Collider>().enabled = true;

					foreach (Transform child in defence.transform) { //enable all colliders in object
						if (child.GetComponent<Collider>() != null && child.name != "selectBox")  
							child.GetComponent<Collider>().enabled = true;
							
						foreach (Transform child2 in child.transform) {
							if (child2.GetComponent<Collider>() != null)
								child2.GetComponent<Collider>().enabled = true;

							foreach (Transform child3 in child2.transform) {
									if (child3.GetComponent<Collider>() != null)
										child3.GetComponent<Collider>().enabled = true;
							}
						}
					}

					cam.GetComponent<GUI_Buttons>().Enabled = true;

					Destroy(transform.parent.gameObject, 1); //destroy canvas / screen
				}
			}
			
		}
	}

	void moveDefence () { //place chosen defence at raycast hit point, then send line from gun to that point
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		
		LineRenderer lineRenderer;
		
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)){
			if (hit.transform.tag == "Floor"){
				LR = (GameObject)Instantiate (BuildRight_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);
				defence.transform.Find("selectBox").GetComponent<Renderer>().material = right;
				cantPlace = false;
			}
			else {
				LR = (GameObject)Instantiate (BuildWrong_LR, lineRendererSpawn.transform.position, lineRendererSpawn.transform.rotation);
				defence.transform.Find("selectBox").GetComponent<Renderer>().material = wrong;
				cantPlace = true;
			}
			
			defence.transform.position = hit.point;
			
			lineRenderer = LR.GetComponent<LineRenderer>();
			lineRenderer.SetPosition(0, lineRendererSpawn.transform.position);
			lineRenderer.SetPosition(1, hit.point);
			
			Destroy (LR, 0.03f);
		}
	}

	public void modernDefenceButtonClicked (int defenceNum) { //determinee witch defence user chose

		chosenDefence = modernDefences[defenceNum];

		if (defenceNum == 2)
			novis = true;

		if (chosenDefence != null){
			if (cam.GetComponent<Money>().infMoney || price <= cam.GetComponent<Money>().currentMoney){ //start defence placement if the user has enough money

				cam.GetComponent<stopGame>().PlayGame();

				AudioSource.PlayClipAtPoint(ButtonClick, player.transform.position, 4); //play click

				defence = (GameObject)Instantiate(chosenDefence, chosenDefence.transform.position, chosenDefence.transform.rotation);
			
				building = true;

				transform.parent.GetComponent<Canvas>().enabled = false;
				Destroy(GameObject.FindGameObjectWithTag("Event"));
		
				cam.GetComponent<GUI_Buttons>().Enabled = true;

				cam.GetComponent<Money>().currentMoney -= price;
				cam.GetComponent<Money>().DefenceValue += price;
			}
			else{ //play negitive sound if user does not have enough money
				Time.timeScale = 1;
				AudioSource.PlayClipAtPoint(notEnoughMoney, player.transform.position);
				Time.timeScale = 0;
			}
		}
	}

	public void DefenceButtonClicked (int Defprice){
		price = Defprice;
	}

	public void buildCancel () { //fuction called when CANCEl on screen is pressed;
		cam.GetComponent<stopGame>().PlayGame();

		AudioSource.PlayClipAtPoint(ButtonClick, player.transform.position, 4); //play click

		cam.GetComponent<GUI_Buttons>().Enabled = true;

		Destroy(transform.parent.gameObject);
	}

	void Cancel () { //fuction called to cancel defence placement
		Destroy(defence);
		Destroy(LR);
		building = false;  

		cam.GetComponent<GUI_Buttons>().Enabled = true;

		cam.GetComponent<Money>().currentMoney += price; //refund
		cam.GetComponent<Money>().DefenceValue -= price;
		Destroy(transform.parent.gameObject, 1);
	}
}
