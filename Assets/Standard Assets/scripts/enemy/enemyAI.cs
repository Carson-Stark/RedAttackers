using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public Transform[] Path1;
	public Transform[] Path2;
	public int enemyEarnings;
	public float health;
	public float speed;
	public float blastForce;

	public bool right; //used in other scripts
	public bool left; //used in other scripts

	public bool blasted; //used in other scripts

	string diff;
	bool firstUpdate = true;
	bool canMove = true;
	int path = 1;
	int nextWaypoint = 0;

	void Start () {
		diff = GameObject.FindGameObjectWithTag ("Spawn").GetComponent<SpawnEnemy>().d;
		if (diff == "Beginner"){
			enemyEarnings = 100;
		}
		else if (diff == "Easy"){
			enemyEarnings = 50;
		}
		else if (diff == "Intermediate"){
			enemyEarnings = 40;
		}
		else if (diff == "Hard"){
			enemyEarnings = 35;
		}
		else {
			enemyEarnings = 20;
		}

		blasted = false;
	}
	
	void FixedUpdate () {
		if (canMove){

			if (firstUpdate){
				path = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().enemyPath; //set path
				firstUpdate = false;
			}

			switch (path){ //move enemy down apropate path;
				case 1:
					if (transform.position == Path1[nextWaypoint].position)
						nextWaypoint++;
					transform.position = Vector3.MoveTowards(transform.position, Path1[nextWaypoint].position, speed * Time.deltaTime);
					break;	
					
				case 2:
					if (transform.position == Path2[nextWaypoint].position)
						nextWaypoint++;
					transform.position = Vector3.MoveTowards(transform.position, Path2[nextWaypoint].position, speed * Time.deltaTime);
					break;
			}

			if (health <= 0) { //destroy enemy when out of health
				GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>().enemyStanding--;
				Camera.main.GetComponent<Money>().currentMoney += enemyEarnings;
				if (!blasted)
					Destroy(this.gameObject);
				else{
					canMove = false;
					Destroy (transform.GetChild (0).gameObject);
					Destroy (this.gameObject, 1);
				}
			}

		}
	}

	void OnCollisionEnter (Collision hit){ //push player to center of level if touched
		if (hit.transform.tag == "Player" && !blasted){
			if (right)
				hit.rigidbody.AddForce(-hit.transform.right * blastForce);
			else if (left)
				hit.rigidbody.AddForce(hit.transform.right * blastForce);
		}
	}
}
