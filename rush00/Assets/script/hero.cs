using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hero : character {

	public Camera	cam;
	private bool pickup = false;
	public bool	isAlive = true;

	public AudioSource takeWeapon;
	public AudioSource dropWeapon;
	public AudioSource noAmmoShoot;

	public GameObject room_obj;
	public GameObject Enemy_manager;

	// Use this for initialization
	void Start () {

		currentWeapon = null;
		animLegs  = this.foot.GetComponent<Animator>();
		rb = this.GetComponent<Rigidbody2D>();
		maxSpeed = 6.0f;
		cam.transform.position = new Vector3 (transform.position.x, transform.position.y, cam.transform.position.z);
	}

	void FixedUpdate()
	{
		if (isAlive)
		{
			moveX = Input.GetAxis("Horizontal"); 
			moveY = Input.GetAxis("Vertical"); 
			rb.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
		}
	}

	// Update is called once per frame
	void Update () {

		if (isAlive) {
			pickup = false;

			// REPLACE THE SPRITES AND THE CAMERA
			head.transform.position = transform.position;
			body.transform.position = transform.position;
			foot.transform.position = transform.position;

			cam.transform.position = new Vector3 (transform.position.x, transform.position.y, cam.transform.position.z);

			if (currentWeapon != null) {
				currentWeapon.transform.position = transform.position;
				currentWeapon.GetComponent<weapon> ().transform.localRotation = Quaternion.identity;
				currentWeapon.GetComponent<weapon> ().transform.localPosition = new Vector2 (currentWeapon.GetComponent<weapon> ().spriteInHand.transform.localPosition.x + 0.1f, currentWeapon.GetComponent<weapon> ().spriteInHand.transform.localPosition.y - 0.4f);
			}

			// ANIMATION LEGS
			if (moveX != 0 || moveY != 0)
				animLegs.SetBool ("walking", true);
			else
				animLegs.SetBool ("walking", false);

			// GET ANGLE FROM MOUSE POSITION
			rotateBodyToMouse ();

			if (Input.GetKey ("e") || Input.GetMouseButton (2))
				pickup = true;
			if (Input.GetKeyUp ("e"))
				pickup = false;
			if (Input.GetMouseButton (1) && currentWeapon != null)
				releaseWeapon ();
			if (Input.GetMouseButton (0) && currentWeapon != null && !currentWeapon.GetComponent<weapon> ().isWhiteWeapon) { // fire weapon
				currentWeapon.GetComponent<weapon> ().shoot ();
				make_sound ();
			}
			if (Input.GetMouseButtonDown (0) && currentWeapon != null && currentWeapon.GetComponent<weapon> ().isWhiteWeapon) // white weapon
				currentWeapon.GetComponent<weapon> ().shoot ();
		}
	}

	public void make_sound () {
		CircleCollider2D circle_sound = transform.Find("son").GetComponent<CircleCollider2D> ();
		List <enemy> enemy_list = Enemy_manager.GetComponent<EnemyManager> ().get_current_list ();

		foreach (enemy enemy in enemy_list) {
			if (circle_sound.bounds.Contains(enemy.transform.position)) {
				enemy.set_alert();
			}
		}
	}

	void releaseWeapon()
	{
		dropWeapon.Play();
		currentWeapon.GetComponent<weapon>().spriteInHand.GetComponent<SpriteRenderer>().enabled = false;
		currentWeapon.GetComponent<SpriteRenderer>().enabled = true;

		currentWeapon.GetComponent<weapon>().transform.parent = null;
		currentWeapon.GetComponent<weapon>().slide();
		//GameObject.Instantiate(currentWeapon, transform.position, Quaternion.identity);
		currentWeapon = null;
	}

	void rotateBodyToMouse()
	{
		Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition);
		float angleToMouse = Mathf.Atan2 (direction.x - rb.position.x, direction.y - rb.position.y);
		angleToMouse = angleToMouse * 180 / Mathf.PI;

		this.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse + 180);
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.layer == 15 ) { // si collider est sur le layer enemy bullet
			isAlive = false;
			GameObject.Destroy(currentWeapon);
		}

		if (col.tag == "weapon" && pickup && currentWeapon == null) {
			takeWeapon.Play();
			currentWeapon = col.gameObject;

			currentWeapon.GetComponent<weapon>().spriteInHand.GetComponent<SpriteRenderer>().enabled = true;
			currentWeapon.GetComponent<SpriteRenderer>().enabled = false;

			currentWeapon.GetComponent<weapon>().transform.parent = gameObject.transform;

			currentWeapon.GetComponent<weapon>().transform.position = transform.position;
			currentWeapon.GetComponent<weapon>().transform.localRotation = Quaternion.identity;
			currentWeapon.GetComponent<weapon>().transform.localPosition = new Vector2(currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.x + 0.1f, currentWeapon.GetComponent<weapon>().spriteInHand.transform.localPosition.y);
			currentWeapon.GetComponent<weapon>().transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}
}
