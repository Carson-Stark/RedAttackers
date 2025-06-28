using UnityEngine;
using System.Collections;

public class healthBar : MonoBehaviour {

	float startingHealth;
	float currenthealth;

	float healthToBar;

	void Start () {
		startingHealth = transform.parent.parent.GetComponent<enemyAI>().health;
	}

	void Update () {
		currenthealth = transform.parent.parent.GetComponent<enemyAI>().health;

		if (currenthealth != startingHealth && !transform.parent.GetComponent<Canvas>().enabled)
			transform.parent.GetComponent<Canvas>().enabled = true;

		if (currenthealth >= 0 && currenthealth != startingHealth) {
			healthToBar = currenthealth / startingHealth;
			transform.localScale = new Vector3 (healthToBar, transform.localScale.y, transform.localScale.z);
		}

		transform.parent.LookAt (Camera.main.transform);
		transform.parent.Rotate (0, 180, 0);
	}
}
