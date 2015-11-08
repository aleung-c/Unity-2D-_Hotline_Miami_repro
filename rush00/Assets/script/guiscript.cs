using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class guiscript : MonoBehaviour {

	public GameObject	hero;
	public Text			nbAmmo;
	public Image		spriteAmmo;

	// Use this for initialization
	void Start () {
		nbAmmo.text = "";
		spriteAmmo.GetComponent<Image>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (hero.GetComponent<hero>().currentWeapon != null)
		{
			int nb = hero.GetComponent<hero>().currentWeapon.GetComponent<weapon>().getNbAmmos();
			if (nb != -1)
				nbAmmo.text = " x " + nb.ToString();
			else
				nbAmmo.text = "Infinite";
			spriteAmmo.sprite = hero.GetComponent<hero>().currentWeapon.GetComponent<weapon>().bullet.GetComponent<SpriteRenderer>().sprite;
			spriteAmmo.GetComponent<Image>().enabled = true;
		}else{
			nbAmmo.text = "No Weapon";
			spriteAmmo.GetComponent<Image>().enabled = false;
		}
	}
}
