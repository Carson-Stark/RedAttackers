using UnityEngine;
using System.Collections;

public class GranadeLauncher : MonoBehaviour {
	
	public GameObject projectile;
	public Transform spawnPoint;
	public Transform rayPoint;
	public float projectileForwardForce;
	public float projectileUpwardForce;
	public float sightRaduis;
	public float coolDown;

	public GameObject target; //used by other scripts

	GameObject blast;
	float coolCount;
	bool canShoot;
	bool shot;
	
	void Awake () {
		coolCount = coolDown;
		canShoot = true;
		shot = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		LookForEnemy();
		
		if (target != null){
			Shoot();
			transform.LookAt(target.transform.position);
		}
	}

	void LookForEnemy () {
		if (target == null){
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			
			GameObject closest = null;
			float distance = Mathf.Infinity;
			if (enemies != null){
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
			
			
			Collider[] enemiesInRaduis = Physics.OverlapSphere(transform.position, sightRaduis);
			foreach (Collider foundEn in enemiesInRaduis){
				if (foundEn.gameObject == closest)
					target = closest;
			}
		}
		else {
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

	void Shoot () {
		if (shot){ //can't shoot if just shot
			shot = false;
			canShoot = false;
		}
		
		if (!canShoot) //timer for gun cooldown
			coolCount -= Time.deltaTime;
		if (coolCount <= 0){
			canShoot = true;
			coolCount = coolDown;
		}
		
		if (canShoot){
			RaycastHit hit;
			if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, 50)){
				
				if (hit.transform.tag == "Enemy"){ //if hits enemy create granade
					if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
						GameObject granade = (GameObject)Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
						granade.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForwardForce * hit.distance);
						//granade.GetComponent<Rigidbody>().AddForce(transform.up * projectileUpwardForce * hit.distance);

						transform.Find ("launcher").GetComponent<Animation>().Play();
					}
				}

			}

			shot = true;
		}
	}
	
	void OnDrawGizmosSelected () {
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
