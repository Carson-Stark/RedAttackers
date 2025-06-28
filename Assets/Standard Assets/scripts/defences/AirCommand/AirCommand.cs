using UnityEngine;
using System.Collections;

public class AirCommand : MonoBehaviour {

	public GameObject projectile;
	public Transform spawnPoint;
	public float coolDown;
	
	GameObject target;
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
		FindTarget();
		
		if (target != null)
			Launch();
	}
	
	void FindTarget () {
		if (target == null){
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			if (enemies != null && enemies.Length > 0){
				int enemyNumber = Random.Range (0, enemies.Length);
				target = enemies[enemyNumber];
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
			GameObject missle = (GameObject)Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
			missle.GetComponent<AirCommandMissle>().target = target;
			
			shot = true;
		}
	}
}
