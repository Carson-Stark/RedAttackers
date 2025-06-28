using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RevolverTurret_head : MonoBehaviour {
	
	public float sightRaduis;
	
	public string special;
	
	public GameObject target; //used for other scripts

	void Awake () {
		GetComponentInChildren<Animator>().Play("Still");
	}
	
	void FixedUpdate () {
		LookForEnemy();

		if (target != null)
			transform.LookAt(target.transform.position);
	}
	
	void LookForEnemy () {
		if (target == null){
			
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			
			GameObject closest = null;
			float distance = Mathf.Infinity;
			if (enemies != null){ //find closest enemy
				Vector3 position = transform.position;
				foreach (GameObject enemy in enemies) {
					Vector3 diff = enemy.transform.position - position;
					float curDistance = diff.sqrMagnitude;
					if (curDistance < distance) {
						closest = enemy;
						distance = curDistance;
					}
				}
			}
			
			
			Collider[] enemiesInRaduis = Physics.OverlapSphere(transform.position, sightRaduis); //check is closest enemy is in range
			foreach (Collider foundEn in enemiesInRaduis){
				if (foundEn.gameObject == closest)
					target = closest;
			}
		}
		else { //if there is a target check if it has left the raduis
			Collider[] enemiesInRaduis = Physics.OverlapSphere(transform.position, sightRaduis);
			
			int enemiesNotTarget = 0;
			foreach (Collider foundEn in enemiesInRaduis){
				if (foundEn.gameObject != target)
					enemiesNotTarget++;
			}
			
			if (enemiesNotTarget == enemiesInRaduis.Length)
				target = null;
		}
	}
	
	void OnDrawGizmosSelected () { //draw raduis (GIZMO)
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
