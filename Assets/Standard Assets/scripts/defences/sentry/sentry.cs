using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sentry : MonoBehaviour {

	public Transform raycastPoint;
	public Transform trailSpawn;
	public GameObject bulletHole;
	public GameObject trail;
	public GameObject sparks;
	public AudioClip shootAudio;
	public float maxScatter;
	public float coolDown;
	public float damage;
	
	public string special;

	GameObject target;
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
		target = transform.parent.GetComponent<CannonTurret>().target;

		if (target != null){
			transform.LookAt (target.transform);
			Shoot();
			spinStopping = false;
			spinStopCount = 0.5f;
		}
		else {
			Destroy (T);
			spinStopping = true;
		}

		if (spinStopping) {
			spinStopCount -= Time.deltaTime;
			
			if (target != null)
				spinStopping = false;
			else if (spinStopCount <= 0) {
				GetComponent<Animator>().Play ("Still");
				spinStopping = false;
				spinStopCount = 0.5f;
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
			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.1f);
			
			Vector3 rayPoint = new Vector3(raycastPoint.position.x + Random.Range (-maxScatter, maxScatter), raycastPoint.position.y + Random.Range (-maxScatter, maxScatter), raycastPoint.position.z); //establishes random raypoint
			
			RaycastHit hit;
			if (Physics.Raycast(rayPoint, raycastPoint.forward, out hit, 50)){
				
				if (hit.transform.tag != "Finish" && hit.transform.tag != "Invisable" && hit.transform.tag != "Path"){ //if it dosen't hit a invisable object instantiate bullet hole
					GameObject hole = (GameObject)Instantiate (bulletHole , hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
					hole.transform.parent = hit.transform;
				}
				
				if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
					if (hit.transform.gameObject.GetComponent<enemyAI>() != null){
						hit.transform.gameObject.GetComponent<enemyAI>().health -= damage;
					}
				}

				GameObject S = (GameObject)Instantiate (sparks, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy (S, 0.1f);
				
				T = (GameObject)Instantiate(trail, trailSpawn.position, trailSpawn.rotation); //instantiate trail
				LineRenderer T_LR = T.GetComponent<LineRenderer>();
				T_LR.SetPosition(0, rayPoint);
				T_LR.SetPosition(1, hit.point);

				GetComponent<Animator>().Play("Spin");
				
				Debug.DrawLine(rayPoint, hit.point);
			}
			
			shot = true;
		}
	}
}
