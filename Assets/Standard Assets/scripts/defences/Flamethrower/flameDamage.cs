using UnityEngine;
using System.Collections;

public class flameDamage : MonoBehaviour {

	public float damageDelay;
	public float damage;

	float count;

	void Awake(){
		count = damageDelay;
	}

	// Update is called once per frame
	void FixedUpdate () {
		count -= Time.deltaTime;

		if (count <= 0){ //if time between taking health has passed
			if (transform.parent.GetComponent<enemyAI>() != null){
				transform.parent.GetComponent<enemyAI>().health -= damage; //take health
				count = damageDelay;
			}
		}
	}
}
