using UnityEngine;
using System.Collections;

public class enableScript : MonoBehaviour {

	public string defenceName;

	public bool enable; //used by other scripts
	
	// Update is called once per frame
	void Update () {
		if (enable){ //if we need to enable a script...
			switch (defenceName){ //enable the "defence" script on the object

				case "Turret":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<Turret>().enabled = true;
						else if (c.name == "TurretArm")
							c.GetComponent<turretArm>().enabled = true;
					}
					break;
			
				case "CannonTurret":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<CannonTurret>().enabled = true;
					}
					break;
			
				case "SniperTurret":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<SniperTurret>().enabled = true;
					}
					break;
			
				case "Flamethrower":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<Flamethrower>().enabled = true;
						else if (c.name == "TurretArm")
							c.GetComponent<turretArm>().enabled = true;
					}
					break;
			
				case "GranadeLauncher":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<GranadeLauncher>().enabled = true;
						else if (c.name == "TurretArm")
							c.GetComponent<turretArm>().enabled = true;
					}
					break;
			
				case "AirCommand":
					GetComponent<AirCommand>().enabled = true;
					foreach (Transform child in transform){
						foreach (Transform child2 in child.transform)  {
							if (child2.GetComponent<Turret>() != null)
								child2.GetComponent<Turret>().enabled = true;
						}
					}
					break;
			
				case "RocketLauncher":
					foreach (Transform c in transform){
						if (c.name == "head")
							c.GetComponent<RocketLauncher>().enabled = true;
						else if (c.name == "TurretArm")
							c.GetComponent<turretArm>().enabled = true;
					}
					break;

				case "Sentry":
					foreach (Transform c in transform){
						if (c.name == "head") {
							c.GetComponent<CannonTurret>().enabled = true;
							c.GetChild (0).GetComponent<sentry>().enabled = true;
							c.GetChild (1).GetComponent<sentry>().enabled = true;
							transform.Find ("TurretArm").GetComponent<turretArm>().enabled = true;
						}	
					}
					break;
			
				default :
					Debug.LogError("The sent defence name did not match any defences in 'enableScript' , the switch defaulted");
					break;

			}

			Destroy(GetComponent<enableScript>()); //after done destroy this script
		}
	}
}
