using UnityEngine;
using System.Collections;

public class upgrades : MonoBehaviour {

	public GameObject levelDisplay;

	public string defName;
	public int level;

	public string[] dis;
	public string[] price;

	public string[] up1;
	public string[] up2;
	public string[] up3;
	public string[] up4;
	public string[] up5;
	/*public string[] up6;
	public string[] up7;
	public string[] up8;
	public string[] up9;
	public string[] up10;*/

	public void upgrade () {

		switch (defName) {
			case "Turret":
				switch (level) {
					case 1:
						GetComponent<Turret>().sightRaduis += float.Parse (up2[0]);
						GetComponent<Turret>().damage += float.Parse (up2[1]);
						GetComponent<Turret>().coolDown -= float.Parse (up2[2]);
						up2[0] = transform.GetComponent<Turret>().sightRaduis.ToString();
						up2[1] = transform.GetComponent<Turret>().damage.ToString();
						up2[2] = transform.GetComponent<Turret>().coolDown.ToString();
						break;
				}	
				break;

			case "Cannon Turret":
				switch (level) {
					case 1:
						GetComponent<CannonTurret>().sightRaduis += float.Parse (up2[0]);
						GetComponent<CannonTurret>().damage += float.Parse (up2[1]);
						GetComponent<CannonTurret>().coolDown -= float.Parse (up2[2]);
						up2[0] = transform.GetComponent<CannonTurret>().sightRaduis.ToString();
						up2[1] = transform.GetComponent<CannonTurret>().damage.ToString();
						up2[2] = transform.GetComponent<CannonTurret>().coolDown.ToString();
						break;
				}	
				break;

			case "Sniper Turret":
				switch (level) {
					case 1:
						GetComponent<SniperTurret>().damage += float.Parse (up2[1]);
						GetComponent<SniperTurret>().coolDown -= float.Parse (up2[2]);
						up2[1] = GetComponent<SniperTurret>().damage.ToString();
						up2[2] = GetComponent<SniperTurret>().coolDown.ToString();
						break;
				}	
				break;

			case "Flamethrower":
				switch (level) {
					case 1:
						GetComponent<Flamethrower>().sightRaduis += float.Parse (up2[0]);
						GetComponent<Flamethrower>().damage += float.Parse (up2[1]);
						GetComponent<Flamethrower>().coolDown -= float.Parse (up2[2]);
						up2[0] = GetComponent<Flamethrower>().sightRaduis.ToString();
						up2[1] = GetComponent<Flamethrower>().damage.ToString();
						up2[2] = GetComponent<Flamethrower>().coolDown.ToString();
						break;
				}	
				break;

			case "Granade Launcher":
				switch (level) {
					case 1:
					GetComponent<GranadeLauncher>().sightRaduis += float.Parse (up2[0]);
					GetComponent<GranadeLauncher>().projectile.GetComponent<Granade>().damage += float.Parse (up2[1]);
					GetComponent<GranadeLauncher>().coolDown -= float.Parse (up2[2]);
					up2[0] = GetComponent<GranadeLauncher>().sightRaduis.ToString();
					up2[1] = GetComponent<GranadeLauncher>().projectile.GetComponent<Granade>().damage.ToString();
					up2[2] = GetComponent<GranadeLauncher>().coolDown.ToString();
					break;
				}	
				break;

			case "Air Command":
				switch (level) {
					case 1:
					GetComponent<AirCommand>().projectile.GetComponent<AirCommandMissle>().damage += float.Parse (up2[1]);
					GetComponent<AirCommand>().coolDown -= float.Parse (up2[2]);
					up2[1] = GetComponent<AirCommand>().projectile.GetComponent<AirCommandMissle>().damage.ToString();
					up2[2] = GetComponent<AirCommand>().coolDown.ToString();
					break;
				}	
				break;

			case "Rocket Launcher":
				switch (level) {
					case 1:
						GetComponent<RocketLauncher>().sightRaduis += float.Parse (up2[0]);
						GetComponent<RocketLauncher>().projectile.GetComponent<Rocket>().damage += float.Parse (up2[1]);
						GetComponent<RocketLauncher>().coolDown -= float.Parse (up2[2]);
						up2[0] = GetComponent<RocketLauncher>().sightRaduis.ToString();
						up2[1] = GetComponent<RocketLauncher>().projectile.GetComponent<Rocket>().damage.ToString();
						up2[2] = GetComponent<RocketLauncher>().coolDown.ToString();
						break;
				}	
				break;

			case "Sentry":
				switch (level) {
					case 1:
						GetComponent<CannonTurret>().sightRaduis += int.Parse (up2[0]);
						GetComponent<CannonTurret>().damage += int.Parse (up2[1]);
						GetComponent<CannonTurret>().coolDown -= int.Parse (up2[2]);
						foreach (sentry script in GetComponentsInChildren<sentry>()) {
							script.damage += int.Parse (up2[3]);
							script.coolDown -= int.Parse (up2[4]);
						}
						up2[0] = GetComponent<CannonTurret>().sightRaduis.ToString();
						up2[1] = GetComponent<CannonTurret>().damage.ToString();
						up2[2] = GetComponent<CannonTurret>().coolDown.ToString();
						up2[3] = (GetComponentInChildren<sentry>().damage * 2).ToString();
						up2[4] = GetComponentInChildren<sentry>().coolDown.ToString();
						break;
					}	
					break;

			case "Gun":
				switch (level) {
					case 1:
						GetComponent<Gun>().maxScatter -= float.Parse (up2[0]);
						GetComponent<Gun>().damage += float.Parse (up2[1]);
						GetComponent<Gun>().coolDown -= float.Parse (up2[2]);
						up2[0] = GetComponent<Gun>().maxScatter.ToString();
						up2[1] = GetComponent<Gun>().damage.ToString();
						up2[2] = GetComponent<Gun>().coolDown.ToString();
						break;
				}
				break;
		}

		level++;
		levelDisplay.GetComponent<TextMesh>().text = level.ToString();
	}
}
