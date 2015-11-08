using UnityEngine;
using System.Collections;

public class sniper : weapon {

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		fireRate = 1.0f;
		nbAmos = 20;
		isWhiteWeapon = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
