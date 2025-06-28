using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public AudioClip hitAudio;
	public Transform raycastPoint;
	public Transform trailSpawn;
	public GameObject bulletHole;
	public GameObject trail;
	public AudioClip shootAudio;
	public float maxScatter;
	public float coolDown;
	public float damage;
	public float sightRaduis;

	public GameObject target; //used by other scripts
	GameObject T;
	bool canShoot;
	bool shot;
	float coolCount;
	float spinStopCount;
	bool spinStopping;

	void Awake () {
		canShoot = true;
		shot = false;
		coolCount = coolDown;
		spinStopCount = 0.1f;
		spinStopping = false;
	}

	void FixedUpdate () {
		LookForEnemy();

		if (target != null){
			Shoot();
			transform.LookAt(target.transform.position);
			spinStopping = false;
			spinStopCount = 0.5f;
		}
		else
			spinStopping = true;

		if (spinStopping) {
			spinStopCount -= Time.deltaTime;
			
			if (target != null)
				spinStopping = false;
			else if (spinStopCount <= 0) {
				transform.Find ("TurretBarrel").GetComponent<Animator>().Play ("Still");
				spinStopping = false;
				spinStopCount = 0.5f;
			}
		}
	}

	void LookForEnemy () {
		if (target == null){
			Destroy (T);

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
		if (!shot && T != null) //destroys trail when not shooting
			Destroy(T);
		
		if (!canShoot) //timer for gun cooldown
			coolCount -= Time.deltaTime;
		if (coolCount <= 0){
			canShoot = true;
			coolCount = coolDown;
		}

		if (canShoot){
			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.1f);

			Vector3 rayPoint = new Vector3(raycastPoint.position.x + Random.Range (-maxScatter, maxScatter), raycastPoint.position.y + Random.Range (-maxScatter, maxScatter), raycastPoint.position.z); //establishes random raypoint
	
			RaycastHit hit;
			if (Physics.Raycast(rayPoint, raycastPoint.forward, out hit, 50)){
					
				if (hit.transform.tag != "Finish" && hit.transform.tag != "Invisable" && hit.transform.tag != "Path"){ //if it dosen't hit a invisable object instantiate bullet hole
					GameObject hole = (GameObject)Instantiate (bulletHole , hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
					hole.transform.parent = hit.transform;
				}
				
				if (hit.transform.tag == "Enemy"){
					if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
						if (hit.transform.gameObject.GetComponent<enemyAI>() != null){
							hit.transform.gameObject.GetComponent<enemyAI>().health -= damage;
							AudioSource.PlayClipAtPoint(hitAudio, transform.position, 10);
						}
					}
				}

				T = (GameObject)Instantiate(trail, trailSpawn.position, trailSpawn.rotation); //instantiate trail
				LineRenderer T_LR = T.GetComponent<LineRenderer>();
				T_LR.SetPosition(0, rayPoint);
				T_LR.SetPosition(1, hit.point);

				transform.Find ("TurretBarrel").GetComponent<Animator>().Play("Spin");

				Debug.DrawLine(rayPoint, hit.point);
			}
			shot = true;
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
