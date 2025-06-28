using UnityEngine;
using System.Collections;

public class drone : MonoBehaviour {

	public AudioClip hitAudio;
	public Transform[] raycastPoints;
	public Transform trailSpawn;
	public GameObject bulletHole;
	public GameObject trail;
	public AudioClip shootAudio;
	public float maxScatter;
	public float coolDown;
	public float damage;
	public float sightRaduis;
	
	GameObject target;
	GameObject T;
	bool canShoot;
	bool shot;
	float coolCount;
	int side;
	
	void Awake () {
		canShoot = true;
		shot = false;
		coolCount = coolDown;
		side = 0;
	}
	
	void Update () {
		LookForEnemy();
		
		if (target != null){
			Shoot();
			transform.LookAt(target.transform.position);
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
						target = enemy;
						distance = curDistance;
					}
				}
			}

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
			side++;
			if (side == raycastPoints.Length)
				side = 0;

			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.5f);
			
			Vector3 rayPoint = new Vector3(raycastPoints[side].position.x + Random.Range (-maxScatter, maxScatter), raycastPoints[side].position.y + Random.Range (-maxScatter, maxScatter), raycastPoints[side].position.z); //establishes random raypoint
			
			RaycastHit hit;
			if (Physics.Raycast(rayPoint, raycastPoints[side].forward, out hit, 50)){
				
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
				
				Debug.DrawLine(rayPoint, hit.point);
			}
			shot = true;
		}
	}
	
	void OnDrawGizmosSelected () {
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}
}
