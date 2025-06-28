using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject enemy;
	public float spawnDelay;
	public float resumeDelay;
	public int EnemyNumber; //for other scripts
	public int wave; //for other scripts
	public int enemyStanding; //for other scripts
	public int waveDelay;

	public int multiplier; //for other scripts
	public string d; //for other scripts

	int DNumber;

	public int enemyPath; //for other scripts
	bool path1;

	bool spawningEnemy;
	int enemysNotSpawned;
	float spawnCountDown;
	public float waveCountDown; //for other scripts

	bool res;

	int showTut;
	
	void Awake () {
		res = DataManager.resuming;

		d = DataManager.diff;
		
		if (d == "Beginner"){ //set multiplier based on difficulty
			multiplier = 20;
			DNumber = 0;
		}
		else if (d == "Easy"){
			multiplier = 15;
			DNumber = 1;
		}
		else if (d == "Intermediate"){
			multiplier = 13;
			DNumber = 2;
		}
		else if (d == "Hard"){
			multiplier = 8;
			DNumber = 3;
		}
		else {
			multiplier = 3;	
			DNumber = 4;
		}

		spawningEnemy = false;

		EnemyNumber = 1;

		if (res){
			DataManager.PlayClick();

			wave = PlayerPrefs.GetInt("waveNumber", 1) - 1;
			Debug.Log ("wave: " + (wave + 1));
			waveCountDown = resumeDelay;

			if (wave < multiplier){ //calculate how much enemies will be in wave
				for (int count = -1; count < wave + 1; count++){
					EnemyNumber += count;
					Debug.Log (EnemyNumber + ", " + count);
				}
			}
	
			enemyStanding = 0;
		}
		else{
			wave = 1;
			waveCountDown = waveDelay;
			enemyStanding = 1;
			Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		}
	
		enemysNotSpawned = 0;
		showTut = 0;

		spawnCountDown = spawnDelay;

		enemyPath = 1;
		path1 = true;	
		showTut = 0;
	}

	void FixedUpdate(){


		if (wave == multiplier && showTut < 1){
			Camera.main.GetComponent<tutorial>().enabled = true;
			Camera.main.GetComponent<tutorial>().show("multiplierTut");

			showTut++;
		}

		if (spawningEnemy)
			spawnCountDown -= Time.deltaTime;

		if (spawnCountDown <= 0 && enemysNotSpawned > 0){ //if spawn delay has been passed spawn enemy
			spawnEnemy();
			enemysNotSpawned--;
			spawnCountDown = spawnDelay;
		}

		if (enemyStanding == 0){
			waveCountDown -= Time.deltaTime;
		}

		if (Input.GetKeyUp(KeyCode.Return)  && enemyStanding == 0 || waveCountDown <= 0){  //if all the enemies have died and wave delay has been passsed start wave
			wave++;

			string arrayName = DataManager.level + "HS";
			int[] HS = PlayerPrefsX.GetIntArray(arrayName, 0, 5);
			if (HS[DNumber] < wave){
				HS[DNumber] = wave;
				PlayerPrefsX.SetIntArray(arrayName, HS);
			}

			Camera.main.GetComponent<GUI_Buttons>().saving = true;
			PlayerPrefs.SetInt("waveNumber", wave);
			PlayerPrefs.SetInt ("Money", Camera.main.GetComponent<Money>().DefenceValue + Camera.main.GetComponent<Money>().currentMoney);
			PlayerPrefs.SetString("Level", DataManager.level);
			PlayerPrefs.SetString("Diff", DataManager.diff);

			waveCountDown = waveDelay;

			if (wave >= multiplier){ //calculate how much enemies will be in wave
				int count = 2;
				while (wave == (multiplier * count)){
					count++;
				}
				EnemyNumber = EnemyNumber * count;
			}
			else
				EnemyNumber += wave;

			enemyStanding = EnemyNumber;

			StartWave(); //start the wave
		}
	}

	void StartWave(){ //set up varibles for wave, spawn first enemy
		spawnEnemy();
		enemysNotSpawned = EnemyNumber - 1;
		spawnCountDown = spawnDelay;
		spawningEnemy = true;
	}

	void spawnEnemy (){ //spawn enemy and set apropiate path
		if (path1){
			enemyPath = 1;
			path1 = !path1;
		}
		else{
			enemyPath = 2;
			path1 = !path1; 
		}

		GameObject instEnemy = (GameObject)Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		instEnemy.GetComponent<enemyAI>().health += EnemyNumber / 2;
	}
}
