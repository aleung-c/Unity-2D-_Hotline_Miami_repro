using UnityEngine;
using System.Collections;

public class submachine : weapon {

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		fireRate = 0.15f;
		nbAmos = 80;
		isWhiteWeapon = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
