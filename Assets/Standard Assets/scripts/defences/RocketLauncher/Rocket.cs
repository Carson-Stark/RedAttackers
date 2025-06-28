using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public GameObject explosion;
	public AudioClip explosionAudio;
	public float blastRaduis;
	public float damage;
	public float speed;

	public Transform target; //used by other scripts

	void FixedUpdate () {
		if (target != null)
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed);
	}

	void OnTriggerEnter (Collider other){
		if (other.transform.tag != "Invisable"){
			AudioSource.PlayClipAtPoint(explosionAudio, transform.position, 0.2f);
			
			GameObject blast = (GameObject)Instantiate (explosion, transform.position, other.transform.rotation);
			
			Collider[] objectsHit = Physics.OverlapSphere(transform.position, blastRaduis);
			foreach (Collider _object in objectsHit){
				if (_object.transform.tag == "Enemy"){
					if (_object.GetComponent<enemyAI>() != null)
						_object.GetComponent<enemyAI>().health -= damage;
				}
			}
			
			Destroy (blast, 1);
			Destroy(this.gameObject);
		}
	}
}
