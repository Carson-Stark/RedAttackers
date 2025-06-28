using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public AudioClip hitAudio;

	public bool machineGun;

	public Transform trailSpawn;
	public GameObject MGbulletHole;
	public GameObject MGtrail;
	public AudioClip shootAudio;
	public float coolDown;
	public float damage;
	public float maxScatter;
	public string special;

	Ray ray;

	GameObject trail;
	bool canShoot;
	bool shot;
	float coolCount;

	// Use this for initialization
	void Awake () {
		canShoot = true;
		shot = false;
		coolCount = coolDown;

		maxScatter = maxScatter + (Screen.height / 500);
	}
	
	// Update is called once per frame
	void Update () {
		if (shot || !canShoot)  //switchs crosshairs trans.
			Camera.main.GetComponent<crossHair>().trans = false;
		else
			Camera.main.GetComponent<crossHair>().trans = true;

		if (shot){ //can't shoot if just shot
			AudioSource.PlayClipAtPoint(shootAudio, transform.position, 0.08f);
			shot = false;
			canShoot = false;
		}
		if (!shot && trail != null) //destroys trail when not shooting
			Destroy(trail);

		if (!canShoot) //timer for gun cooldown
			coolCount -= Time.deltaTime;
		if (coolCount <= 0){
			canShoot = true;
			coolCount = coolDown;
		}

		if (Input.GetMouseButton(0)){
			if (canShoot){
				if (machineGun){ //if a machine gun
					ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + Random.Range (-maxScatter, maxScatter), Screen.height / 2 + Random.Range (-maxScatter, maxScatter), 0)); //establishes random raypoint
					Debug.DrawRay (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2 + Random.Range (-maxScatter, maxScatter), Screen.height / 2 + Random.Range (-maxScatter, maxScatter), 0)), Camera.main.transform.forward, Color.green); 

					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 50)){

						Debug.DrawLine(Camera.main.transform.position, hit.point);

						if (hit.transform.tag != "Finish" && hit.transform.tag != "Invisable" && hit.transform.tag != "Path"){ //if it dosen't hit a invisable object instantiate bullet hole
							GameObject bulletHole = (GameObject)Instantiate (MGbulletHole , hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
							bulletHole.transform.parent = hit.transform;
						}

						if (hit.transform.tag == "Enemy"){ //if hits enemy take from health
							if (hit.transform.gameObject.GetComponent<enemyAI>() != null){
								hit.transform.gameObject.GetComponent<enemyAI>().health -= damage;
								AudioSource.PlayClipAtPoint(hitAudio, transform.position, 10);
							}
						}
					}

					trail = (GameObject)Instantiate(MGtrail, trailSpawn.position, trailSpawn.rotation); //instantiate trail
					trail.GetComponent<LineRenderer>().SetPosition (0, trailSpawn.position);
					trail.GetComponent<LineRenderer>().SetPosition (1, hit.point);
					trail.transform.parent = transform;
				}

				shot = true;
			}
		}
	}
}
