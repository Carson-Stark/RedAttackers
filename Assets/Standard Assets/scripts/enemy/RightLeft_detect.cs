using UnityEngine;
using System.Collections;

public class RightLeft_detect : MonoBehaviour {

	public bool left;
	public bool right;

	void OnTriggerStay (Collider other){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies != null){
			foreach (GameObject en in enemies){
				if (en.GetComponent<enemyAI>() != null){
					if (left){ //if inside left side tell enemies player is in left
						en.GetComponent<enemyAI>().right = false;
						en.GetComponent<enemyAI>().left = true;
					}
					else if (right){ //if inside right side tell enemies player is in right
						en.GetComponent<enemyAI>().left = false;
						en.GetComponent<enemyAI>().right = true;
					}
				}
			}	
		}
	}
}
