using UnityEngine;
using System.Collections;

public class saber : weapon {

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		fireRate = 0.1f;
		nbAmos = -1;
		isWhiteWeapon = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
