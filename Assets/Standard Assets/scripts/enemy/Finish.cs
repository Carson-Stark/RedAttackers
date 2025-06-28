using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	public GameObject failScreen;
	public float health;

	void OnTriggerEnter (Collider other){
		if (other.transform.tag == "Enemy" && !other.GetComponent<enemyAI>().blasted){ //detect when an enemy gets to finish
			Destroy(other.gameObject);
			health--;
			GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().enemyStanding--;
		}
	}

	void Update(){
		if (health <= 0){ //if lives are out end game
			if (GameObject.FindGameObjectWithTag ("Screen") != null){
				Destroy (GameObject.FindGameObjectWithTag ("Screen"));
			}

			Instantiate (failScreen, failScreen.transform.position, failScreen.transform.rotation);

			Camera.main.GetComponent<stopGame>().StopGame();

			Destroy(GetComponent<Finish>());
		}
	}
}
