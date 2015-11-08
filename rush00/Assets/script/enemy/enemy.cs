using UnityEngine;
using System.Collections;

public class enemy : character {
	public bool			standby = true;
	private bool		_player_in_collider = false;
	public bool			alerted = false;
	public bool			is_patroller = false;
	public bool			action = true;

	[HideInInspector]
	// initial pos for reset;
	public Vector2		initial_pos;

	// player last known position
	private Vector2		_last_known_pos = Vector2.zero;
	// eneme states //
	public bool			dead = false;
	private float		speed = 0.8F;
	private float		speed_alert = 2.5F;
	public float		attack_delay = 30.0f;

	// player object for finding. set at start with manager.
	public GameObject	player;

	//[HideInInspector] // automatically set to know the cur player room.
	public GameObject room_obj;

	// Enemy patrolling;
	private bool	patrol_set = false;
	public int		patrol_dir = 0;
	private float	patrol_distance = 0.0f;
	private Vector2 patrol_point1;
	private Vector2 patrol_point2;
	private bool	going_to_1 = false;
	private bool	going_to_2 = false;

	public bool		canShoot = true;
	public float	fireRate = 0.3f;

	public AudioSource fire_sound;
	public AudioSource death_sound;

	private float		reactionTimer = 0.0f;
	private float		reactionMax = 0.5f;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Hero") { // hero dans le champs de vision.
			_player_in_collider = true;
			Vector2 obj_pos = transform.position;
			Vector2 direction = new Vector2(collider.gameObject.transform.position.x - obj_pos.x, collider.gameObject.transform.position.y - transform.position.y);

			RaycastHit2D hit = Physics2D.Raycast(obj_pos, direction, Mathf.Infinity, transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
			if (hit && hit.collider.gameObject.name == "Hero") { // aucun mur trouver
				_last_known_pos = player.transform.position;
				alerted = true;
				standby = false;
				patrol_set = false; // prepare new patrol when player spotted.
				speed = speed_alert;

			}

		}
	}

	// lorsque l'enemy entends un coup de feu. va le faire avancer a la pos du hero ou la porte correspondante.
	public void set_alert () {
		alerted = true;
		speed = speed_alert;
		_last_known_pos = player.transform.position;
	}

	public void set_dead () {
		dead = true;
		death_sound.Play();
		SpriteRenderer sprite = transform.FindChild ("head_enemy").GetComponent<SpriteRenderer>();
		sprite.color = Color.red;
		transform.parent.GetComponent<EnemyManager> ().enemy_list.Remove (this);
		GameObject.Destroy(this.gameObject);
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero") {
			Vector2 obj_pos = transform.position;
			Vector2 direction = new Vector2(collider.gameObject.transform.position.x - obj_pos.x, collider.gameObject.transform.position.y - transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(obj_pos, direction, Mathf.Infinity, transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
			if (hit && hit.collider.gameObject.name == "Hero") {
				alerted = true;
				standby = false;
				patrol_set = false; // prepare new patrol when player spotted.
				speed = 2.5F;
				_last_known_pos = player.transform.position;
			}
		}

	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero") {
			_player_in_collider = false;

		}
	}

	public void move_to_door() {
		//rotate enemy toward player
		Vector2 target_pos;
		if (player.GetComponent<hero> ().room_obj.GetComponent<room>().room_id > room_obj.GetComponent<room>().room_id)
			target_pos = room_obj.GetComponent<room>().next_door_pos;
		else
			target_pos = room_obj.GetComponent<room>().prev_door_pos;
		float angle = Mathf.Atan2(target_pos.x - transform.position.x, target_pos.y - transform.position.y) * 180 / Mathf.PI;
		transform.rotation = Quaternion.Euler(0, 0, -angle + 180.0F);
		
		// move enemy toward door;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target_pos, speed * Time.deltaTime);
	}

	public void move_to(Vector2 _last_known_pos) {
		//rotate enemy toward player
		Vector2 target_pos = _last_known_pos;
		float angle = Mathf.Atan2(target_pos.x - transform.position.x, target_pos.y - transform.position.y) * 180 / Mathf.PI;
		transform.rotation = Quaternion.Euler(0, 0, -angle + 180.0F);

		// move enemy toward player;
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _last_known_pos, speed * Time.deltaTime);
	}

	// for enemy patrolling;
	public void enemy_patrol() {
		// set ennemy patrol route;
		if (patrol_set == false) {
			patrol_dir = Random.Range (0, 1);
			patrol_distance = Random.Range (1.0F, 5.0F);

			if (patrol_dir == 0) { // x patrol.
				patrol_point1 = new Vector2(transform.position.x - patrol_distance, transform.position.y);
				patrol_point2 = new Vector2(transform.position.x + patrol_distance, transform.position.y);

				Vector2 direction = new Vector2(patrol_point1.x - transform.position.x, patrol_point1.y - transform.position.y);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position,patrol_point1) , transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
				if (hit && hit.collider.tag == "wall")
					patrol_point1.x = hit.transform.position.x + 1.0F;
				direction = new Vector2(patrol_point2.x - transform.position.x, patrol_point2.y - transform.position.y);
				hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position,patrol_point2) , transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
				if (hit && hit.collider.tag == "wall")
					patrol_point2.x = hit.transform.position.x - 1.0F;
			}
			else if (patrol_dir == 1) {
				patrol_point1 = new Vector2(transform.position.x, transform.position.y - patrol_distance);
				patrol_point2 = new Vector2(transform.position.x, transform.position.y + patrol_distance);

				Vector2 direction = new Vector2(patrol_point1.x - transform.position.x, patrol_point1.y - transform.position.y);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position,patrol_point1) , transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
				if (hit && hit.collider.tag == "wall")
					patrol_point1.y = hit.transform.position.x + 1.0F;
				direction = new Vector2(patrol_point2.x - transform.position.x, patrol_point2.y - transform.position.y);
				hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position,patrol_point2) , transform.parent.GetComponent<EnemyManager> ().LayerMaskForCollision );
				if (hit && hit.collider.tag == "wall")
					patrol_point2.y = hit.transform.position.x - 1.0F;
			}
			patrol_set = true;
			going_to_1 = true;
		}
		// enemy patrolling. switch entre les deux points.
		if (going_to_1) {
			if ((Vector2)transform.position == patrol_point1 ) {
				going_to_1 = false;
				going_to_2 = true;
			}
			else
				move_to (patrol_point1);
		} else if (going_to_2) {
			if ((Vector2)transform.position == patrol_point2) {
				going_to_1 = true;
				going_to_2 = false;
			}
			else
				move_to (patrol_point2);
		}
	}
	

	public void check_attack() {
		if (canShoot) {
			reactionTimer += Time.deltaTime;
			if (reactionTimer >= reactionMax)
			{
				fire_sound.Play ();
			
				StartCoroutine (canShootCoroutine ());
				GameObject b = (GameObject)Instantiate (currentWeapon.GetComponent<weapon> ().bullet.gameObject, new Vector2 (transform.position.x, transform.position.y), currentWeapon.GetComponent<weapon> ().bullet.gameObject.transform.localRotation);
			
				b.GetComponent<bullet> ().isShot = true;
				b.GetComponent<bullet> ().transform.parent = gameObject.transform;
			
				b.GetComponent<bullet> ().transform.position = transform.position;
				b.GetComponent<bullet> ().transform.localRotation = Quaternion.Euler (0, 0, -90);
				b.GetComponent<bullet> ().transform.localPosition = new Vector2 (b.GetComponent<bullet> ().transform.localPosition.x, b.GetComponent<bullet> ().transform.localPosition.y);
				b.GetComponent<bullet> ().transform.localScale = new Vector3 (1f, 1f, 1f);
				b.GetComponent<bullet> ().transform.parent = null;
				StartCoroutine (goBullet (b));
			}
		}
	}

	IEnumerator goBullet(GameObject b)
	{
		Rigidbody2D r = b.GetComponent<Rigidbody2D>();
		b.GetComponent<SpriteRenderer>().enabled = true;
		r.AddForce(-transform.up * b.GetComponent<bullet>().getSpeed(), ForceMode2D.Impulse);
		yield return new WaitForSeconds(2.0f);
		GameObject.Destroy(b);
	}

	IEnumerator canShootCoroutine()
	{
		canShoot = false;
		yield return new WaitForSeconds(fireRate);
		canShoot = true;
	}

	// Use this for initialization
	void Start () {
		animLegs  = this.foot.GetComponent<Animator>();
		_last_known_pos = player.gameObject.transform.position;
		initial_pos = transform.position;
		animLegs.SetBool("walking", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (action == true) {
			if (alerted == true && dead == false) {
				if (player.GetComponent<hero> ().room_obj != this.room_obj) // si enemy et hero sont dans des salles differentes
					move_to_door ();
				else {
					move_to (_last_known_pos);
					check_attack ();
				}

			} else if (alerted == false && standby == true && dead == false && is_patroller == true) {
				enemy_patrol ();
			}
		}
	}
}
