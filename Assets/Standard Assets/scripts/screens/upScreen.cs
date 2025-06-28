 using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upScreen : MonoBehaviour {

	public AudioClip notEnoughMoney;

	GameObject defence;
	Camera cam;

	upgrades ups;
	Transform stats;
	GameObject price;

	void Start () {
		cam = Camera.main;
		defence = cam.GetComponent<upgradeDefence>().Def;
		
		ups = defence.GetComponent<upgrades>();
		stats = transform.Find ("stats");
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		
		cam.GetComponent<stopGame>().StopGame();
		cam.GetComponent<GUI_Buttons>().Enabled = false;

		transform.Find ("defenceType").GetComponent<Text>().text = ups.defName + ":";
		transform.Find ("level").GetComponent<Text>().text = "Level " + ups.level.ToString();


		switch (ups.level) {
			case 1:
				stats.GetChild (0).GetComponent<Text>().text = "Range: " + ups.up1[0];
				stats.GetChild (0).GetChild (0).GetComponent<Text>().text = "+ " + ups.up2[0];
				stats.GetChild (1).GetComponent<Text>().text = "Damage: " + ups.up1[1];
				stats.GetChild (1).GetChild (0).GetComponent<Text>().text = "+ " + ups.up2[1];
				stats.GetChild (2).GetComponent<Text>().text = "Cooldown: " + ups.up1[2];
				stats.GetChild (2).GetChild (0).GetComponent<Text>().text = "- " + ups.up2[2];
				transform.Find ("description").GetComponent<Text>().text = ups.dis[0];
				transform.Find ("price").GetComponent<Text>().text = "$" + ups.price[0];
				break;

			case 2:
				stats.GetChild (0).GetComponent<Text>().text = "Range: " + ups.up2[0];
				stats.GetChild (0).GetChild (0).GetComponent<Text>().text = "+ " + ups.up3[0];
				stats.GetChild (1).GetComponent<Text>().text = "Damage: " + ups.up2[1];
				stats.GetChild (1).GetChild (0).GetComponent<Text>().text = "+ " + ups.up3[1];
				stats.GetChild (2).GetComponent<Text>().text = "Cooldown: " + ups.up2[2];
				stats.GetChild (2).GetChild (0).GetComponent<Text>().text = "- " + ups.up3[2];
				transform.Find ("discription").GetComponent<Text>().text = ups.dis[1];
				transform.Find ("price").GetComponent<Text>().text = "$" + ups.price[1];
				break;

			case 3:
				stats.GetChild (0).GetComponent<Text>().text = "Range: " + ups.up3[0];
				stats.GetChild (0).GetChild (0).GetComponent<Text>().text = "+ " + ups.up4[0];
				stats.GetChild (1).GetComponent<Text>().text = "Damage: " + ups.up3[1];
				stats.GetChild (1).GetChild (0).GetComponent<Text>().text = "+ " + ups.up4[1];
				stats.GetChild (2).GetComponent<Text>().text = "Cooldown: " + ups.up3[2];
				stats.GetChild (2).GetChild (0).GetComponent<Text>().text = "- " + ups.up4[2];
				transform.Find ("discription").GetComponent<Text>().text = ups.dis[2];
				transform.Find ("price").GetComponent<Text>().text = "$" + ups.price[2];
				break;

			case 4:
				stats.GetChild (0).GetComponent<Text>().text = "Range: " + ups.up4[0];
				stats.GetChild (0).GetChild (0).GetComponent<Text>().text = "+ " + ups.up5[0];
				stats.GetChild (1).GetComponent<Text>().text = "Damage: " + ups.up4[1];
				stats.GetChild (1).GetChild (0).GetComponent<Text>().text = "+ " + ups.up5[1];
				stats.GetChild (2).GetComponent<Text>().text = "Cooldown: " + ups.up4[2];
				stats.GetChild (2).GetChild (0).GetComponent<Text>().text = "- " + ups.up5[2];
				transform.Find ("discription").GetComponent<Text>().text = ups.dis[3];
				transform.Find ("price").GetComponent<Text>().text = "$" + ups.price[3];
				break;

			case 5:
				stats.GetChild (0).GetComponent<Text>().text = "Range: " + ups.up5[0];
				stats.GetChild (0).GetChild (0).GetComponent<Text>().text = "+ " + ups.up5[0];
				stats.GetChild (1).GetComponent<Text>().text = "Damage: " + ups.up5[1];
			    stats.GetChild (1).GetChild (0).GetComponent<Text>().text = "+ " + ups.up5[1];
				stats.GetChild (2).GetComponent<Text>().text = "Cooldown: " + ups.up5[2];
				stats.GetChild (2).GetChild (0).GetComponent<Text>().text = "- " + ups.up5[2];
				transform.Find ("discription").GetComponent<Text>().text = ups.dis[4];	
				transform.Find ("price").GetComponent<Text>().text = "$" + ups.price[4];
				break;
		}
	}

	public void up () {
		if (int.Parse (ups.price[ups.level - 1]) <= cam.GetComponent<Money>().currentMoney || cam.GetComponent<Money>().infMoney) {
			cam.GetComponent<Money>().currentMoney -= int.Parse (ups.price[ups.level - 1]);

			defence.GetComponent<upgrades>().upgrade();

			cam.GetComponent<stopGame>().PlayGame();
			cam.GetComponent<GUI_Buttons>().Enabled = true;
				
			Destroy (this.gameObject); 
		}
		else {
			Time.timeScale = 1;
			AudioSource.PlayClipAtPoint(notEnoughMoney, GameObject.FindGameObjectWithTag ("player").transform.position);
			Time.timeScale = 0;
		}
	}

	public void Cancel () {
		cam.GetComponent<stopGame>().PlayGame();
		cam.GetComponent<GUI_Buttons>().Enabled = true;

		Destroy (this.gameObject);
	}
}
