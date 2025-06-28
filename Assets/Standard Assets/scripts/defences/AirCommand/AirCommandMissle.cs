using UnityEngine;
using System.Collections;

public class AirCommandMissle : MonoBehaviour {

	public GameObject explosion;
	public AudioClip explosionAudio;
	public float power;
	public float blastRaduis;
	public float weakerBlastRaduis;
	public float damage;
	public float speed;

	public GameObject target; //used for other scripts

	bool firstTime;

	void Awake (){
		firstTime = true;
	}
	
	void FixedUpdate () {
		if (target != null)
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed);
		else
			GetComponent<Rigidbody>().useGravity = true;
	}

	void OnTriggerEnter (Collider other){

		if (firstTime){

			if (other.tag != "Invisable"){

				GameObject blast = (GameObject)Instantiate(explosion, transform.position, other.transform.rotation);
				AudioSource.PlayClipAtPoint(explosionAudio, transform.position, 0.3f);
				
				Collider[] objectsHit = Physics.OverlapSphere(transform.position, blastRaduis);
				foreach (Collider _object in objectsHit){

					if (_object.transform.tag == "Enemy"){

						if (_object.GetComponent<enemyAI>() != null){
;
							if (_object.GetComponent<enemyAI>().health <= damage){
								_object.GetComponent<enemyAI>().blasted = true;
								_object.GetComponent<Rigidbody>().useGravity = true;
								_object.GetComponent<Rigidbody>().isKinematic = false;

								if (_object != null)
									_object.GetComponent<Rigidbody>().AddExplosionForce(power, new Vector3 (target.transform.position.x, target.transform.position.y - 3, target.transform.position.z), blastRaduis + 10, 0.1f);
							}

							_object.GetComponent<enemyAI>().health -= damage;
						}

					}

				}

				Collider[] objectsHit2 = Physics.OverlapSphere(transform.position, weakerBlastRaduis);
				foreach (Collider object_ in objectsHit2){
					if (object_.transform.tag == "Enemy")
						object_.GetComponent<enemyAI>().health -= damage / 6.66f;
				}

				Destroy (blast, 1);

				GetComponent<MeshRenderer>().enabled = false;
				transform.DetachChildren();
				Destroy(this.gameObject, 1);

				firstTime = false;
			}

		}

	}

}
