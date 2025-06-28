using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {
	
	public Transform raycastPoint;
	public Transform flameSpawn;
	public GameObject flame;
	public GameObject caughtFire;
	public AudioClip shootAudio;
	public float coolDown;
	public float damage;
	public float sightRaduis;
	public float damageRadius;

	public string special;

	public GameObject target; //used by other scripts

	GameObject fire;
	bool canShoot;
	bool shot;
	float coolCount;

	void Awake () {
		canShoot = true;
		shot = false;
		coolCount = coolDown;
	}

	// Update is called once per frame
	void FixedUpdate () {
		LookForEnemy();
		
		if (target != null){
			Shoot();
			transform.LookAt(target.transform.position); //look at target
		}
	}

	void LookForEnemy () {
		if (target == null){
			Destroy (fire);
			
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
			
			
			Collider[] enemiesInRaduis = Physics.OverlapSphere(transform.position, sightRaduis); //check if closest enemy is in range
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

	void Shoot () {
		if (shot){ //can't shoot if just shot
			shot = false;
			canShoot = false;
		}
		if (!shot && fire != null) //destroys trail when not shooting
			Destroy(fire, 0.5f);
		
		if (!canShoot) //timer for gun cooldown
			coolCount -= Time.deltaTime;
		if (coolCount <= 0){
			canShoot = true;
			coolCount = coolDown;
		}
		
		if (canShoot){
			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.3f); //play shoot audio
			
			RaycastHit hit;
			if (Physics.SphereCast(raycastPoint.position, damageRadius, raycastPoint.forward, out hit, 50)){
				
				if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
					if (hit.transform.gameObject.GetComponent<enemyAI>() != null){
						hit.transform.gameObject.GetComponent<enemyAI>().health -= damage;
					}

					if (hit.transform.childCount <= 3){
						GameObject caught = (GameObject)Instantiate(caughtFire, new Vector3 (hit.transform.position.x, hit.transform.position.y + 0.2f, hit.transform.position.z), caughtFire.transform.rotation); //catch fire on enemy
						caught.transform.parent = hit.transform;
					}
				}
				
				fire = (GameObject)Instantiate(flame, flameSpawn.position, flameSpawn.rotation); //instantiate trail
				
				Debug.DrawLine(raycastPoint.position, hit.point);
			}

			shot = true;
		}
	}

	void OnDrawGizmosSelected () { //draw raduis (GIZMO)
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
