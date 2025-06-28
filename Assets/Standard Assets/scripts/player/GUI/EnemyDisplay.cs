using UnityEngine;
using System.Collections;

public class EnemyDisplay : MonoBehaviour {

	int enemyLeft;
	int enemyTotal;
	int waveCountDown;
	
	void Awake () {
		enemyLeft = 1;
		enemyTotal = 1;
	}
	
	// Update is called once per frame
	void Update () {
		SpawnEnemy spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemy>(); //finds stats
		waveCountDown = (int)spawnScript.waveCountDown;

		enemyLeft = spawnScript.enemyStanding;
		enemyTotal = spawnScript.EnemyNumber;

		if (enemyLeft > 0) //displays text
			GetComponent<TextMesh>().text = enemyLeft + " / " + enemyTotal;
		else
			GetComponent<TextMesh>().text = waveCountDown.ToString();
	}
}
