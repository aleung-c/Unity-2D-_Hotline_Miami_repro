using UnityEngine;
using System.Collections;

public class cricket : weapon {

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		fireRate = 0.5f;
		nbAmos = 20;
		isWhiteWeapon = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
