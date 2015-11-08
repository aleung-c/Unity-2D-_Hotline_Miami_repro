using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomManager : MonoBehaviour {
	public List <room> 	rooms_list = new List<room>();
	public GameObject	enemyManager;
	public GameObject	player;

	// Use this for initialization
	void Start () {
		List <enemy> enemy_list = enemyManager.GetComponent<EnemyManager> ().get_updated_list ();
		rooms_list = FindObjectsOfType <room> ().ToList();
		foreach (room room in rooms_list) { // pour chaque room;
			Collider2D room_box = room.gameObject.GetComponent<Collider2D>();
			if (room_box.bounds.Contains(player.transform.position)) { // check hero et room actuelle;
				player.GetComponent<hero>().room_obj = room.gameObject;
				//Debug.Log("hero set in room " + room.room_id);
			}
			foreach (enemy enemy in enemy_list) { // check chaque enemy et room actuelle;
				if (room_box.bounds.Contains(enemy.gameObject.transform.position)) {
					enemy.room_obj = room.gameObject;
					//Debug.Log("ennemy set in room " + room.room_id);
				}
			}
			enemyManager.GetComponent<EnemyManager> ().set_updated_list (enemy_list);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
