using UnityEngine;
using System.Collections;

public class room : MonoBehaviour {
	public int				room_id;
	public GameObject		next_room;
	public GameObject		prev_room;
	public	Vector2			next_door_pos;
	public	Vector2			prev_door_pos;
	//public GameObject		door_pos;

	// Use this for initialization
	void Start () {
		if (next_room != null) {
			GameObject next_door_obj = gameObject.transform.FindChild ("next_door_pos").gameObject;
			if (next_door_obj)
				next_door_pos = next_door_obj.transform.position;

			//Debug.Log (" next_door_pos = " + next_door_pos.x + "x " + next_door_pos.y + "y.");
		}

		if (prev_room != null) {
			GameObject prev_door_obj = gameObject.transform.FindChild ("prev_door_pos").gameObject;
			if (prev_door_obj)
				prev_door_pos = prev_door_obj.transform.position;
		}
	}

	public Vector2 get_room_next_pos() {
		if (next_room) {
			return next_door_pos;
		}
		return Vector2.zero;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Hero") {
			//Debug.Log("hero change to room " + room_id);
			collider.gameObject.GetComponent<hero> ().room_obj = gameObject;
		}
		if (collider.gameObject.tag == "enemy" && gameObject.GetComponent<Collider2D> ().bounds.Contains(collider.gameObject.transform.position)) {
			//Debug.Log("enemy change to room " + room_id);
			collider.gameObject.GetComponent<enemy> ().room_obj = gameObject;
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag == "enemy" && gameObject.GetComponent<Collider2D> ().bounds.Contains(collider.gameObject.transform.position)) {
			//Debug.Log("enemy change to room " + room_id);
			collider.gameObject.GetComponent<enemy> ().room_obj = gameObject;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
