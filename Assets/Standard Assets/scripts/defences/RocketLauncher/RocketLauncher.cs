using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketLauncher : MonoBehaviour {
	
	public GameObject projectile;
	public Transform[] spawnPoints;
	public Transform rayPoint;
	public AudioClip rocketLaunch;
	public float projectileForwardForce;
	public float projectileUpwardForce;
	public float sightRaduis;
	public float coolDown;
	public float launchDelay;
	public int launchesPerCoolDown;

	public GameObject target; //used by other scripts

	GameObject blast;
	float coolCount;
	float delayCount;
	float distance;
	bool canShoot;
	bool countDown;
	bool shot;
	int launchsNotFired;
	int side;
	
	void Awake () {
		coolCount = coolDown;
		canShoot = true;
		countDown = false;
		delayCount = launchDelay;
		shot = false;
		launchsNotFired = launchesPerCoolDown;
		side = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		FindTarget();

		if (launchsNotFired <= 0)
			countDown = false;
		
		if (target != null){
			transform.LookAt(target.transform);
			Launch();
		}

		if (countDown)
			delayCount -= Time.deltaTime;
		
		if (delayCount <= 0 && launchsNotFired > 0){
			fireMissle();
			launchsNotFired--;
			delayCount = launchDelay;
		}
	}
			
			

	void FindTarget () {
		if (target == null){

			List <GameObject> enemiesInRaduis = new List<GameObject>();

			Collider[] objectsInRaduis = Physics.OverlapSphere(transform.position, sightRaduis);
			foreach (Collider foundOb in objectsInRaduis){
				if (foundOb.tag == "Enemy")
					enemiesInRaduis.Add (foundOb.gameObject);
			}

			if (enemiesInRaduis.Count > 0){
				int rnd = Random.Range(0, enemiesInRaduis.Count - 1);
				target = enemiesInRaduis[rnd];

				Vector3 diff = target.transform.position - transform.position;
				float curDistance = diff.sqrMagnitude;
				distance = curDistance;
			}

		}
	}
	
	void Launch () {
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
			if (target != null){
				fireMissle();
				AudioSource.PlayClipAtPoint(rocketLaunch, transform.position, 0.3f);
				launchsNotFired = launchesPerCoolDown - 1;
				delayCount = launchDelay;
				countDown = true;
				shot = true;
			}
		}
	}

	void fireMissle () {
		GameObject missle = (GameObject)Instantiate(projectile, spawnPoints[side].position, spawnPoints[side].rotation);

		if (missle != null){
  			missle.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForwardForce * distance);
			missle.GetComponent<Rigidbody>().AddForce(transform.up * projectileUpwardForce * distance);
			if (target != null)
				missle.GetComponent<Rocket>().target = target.transform;
		}

		side++;
		if (side == spawnPoints.Length)
			side = 0;
	}

	void OnDrawGizmosSelected () {
		Gizmos.DrawWireSphere(transform.position, sightRaduis);
	}	
}
