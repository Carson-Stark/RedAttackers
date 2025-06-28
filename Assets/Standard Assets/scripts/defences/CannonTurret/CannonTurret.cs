using UnityEngine;
using System.Collections;

public class CannonTurret : MonoBehaviour {

	public AudioClip hitAudio;
	public Transform raycastPoint1;
	public Transform raycastPoint2;
	public Transform trailSpawn1;
	public Transform trailSpawn2;
	public GameObject bulletHole;
	public GameObject trail;
	public AudioClip shootAudio;
	public float maxScatter;
	public float coolDown;
	public float damage;
	public float sightRaduis;
	
	public string special;
	
	public GameObject target; //used for other scripts

	GameObject T;
	bool canShoot;
	bool shot;
	float coolCount;
	float side;
	
	void Awake () {
		canShoot = true;
		shot = false;
		coolCount = coolDown;
		side = 1;
	}
	
	void FixedUpdate () {
		LookForEnemy();
		
		if (target != null) {
			transform.LookAt(target.transform.position);
			Shoot();
		}
		else
			Destroy (T);
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

	void Shoot () {
		if (shot){ //can't shoot if just shot
			shot = false;
			canShoot = false;
		}	
		
		if (!shot && T != null) //destroys trail when not shooting
			Destroy(T);
		
		if (!canShoot) //timer for gun cooldown
			coolCount -= Time.deltaTime;
		if (coolCount <= 0){
			canShoot = true;
			coolCount = coolDown;
		}
		
		if (canShoot){
			Vector3 rayPoint;
			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.1f);

			if (side == 1)
				rayPoint = new Vector3(raycastPoint1.position.x + Random.Range (-maxScatter, maxScatter), raycastPoint1.position.y + Random.Range (-maxScatter, maxScatter), raycastPoint1.position.z); //establishes random raypoint
			else
				rayPoint = new Vector3(raycastPoint2.position.x + Random.Range (-maxScatter, maxScatter), raycastPoint2.position.y + Random.Range (-maxScatter, maxScatter), raycastPoint2.position.z); //establishes random raypoint
			
			RaycastHit hit;
			if (Physics.Raycast(rayPoint, transform.forward, out hit, 50)){
				
				if (hit.transform.tag != "Finish" && hit.transform.tag != "Invisable" && hit.transform.tag != "Path"){ //if it dosen't hit a invisable object instantiate bullet hole
					GameObject hole = (GameObject)Instantiate (bulletHole , hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
					hole.transform.parent = hit.transform;
				}
				
				if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
					if (hit.transform.gameObject.GetComponent<enemyAI>() != null){
						hit.transform.gameObject.GetComponent<enemyAI>().health -= damage;
						AudioSource.PlayClipAtPoint(hitAudio, transform.position, 10);
					}
				}

				if (side == 1) {
					T = (GameObject)Instantiate(trail, trailSpawn1.position, trailSpawn1.rotation); //instantiate trail
					transform.Find ("gun1").GetComponent<Animation>().Play();
					side = 2;
				}
				else {
					T = (GameObject)Instantiate(trail, trailSpawn2.position, trailSpawn2.rotation); //instantiate trail
					transform.Find ("gun2").GetComponent<Animation>().Play();
					Debug.Log ("gun2");
					side = 1;
				}

				LineRenderer T_LR = T.GetComponent<LineRenderer>();
				T_LR.SetPosition(0, rayPoint);
				T_LR.SetPosition(1, hit.point);
				
				Debug.DrawLine(rayPoint, hit.point);
			}
			
			shot = true;
		}
	}
	
	void OnDrawGizmosSelected () { //draw raduis (GIZMO)
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
