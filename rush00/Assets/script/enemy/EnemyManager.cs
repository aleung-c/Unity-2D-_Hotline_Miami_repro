using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class EnemyManager : MonoBehaviour {
	public List <enemy> 	enemy_list = new List<enemy>();

	public GameObject		Player;

	public LayerMask 	 LayerMaskForCollision = 0;
	// Use this for initialization
	void Start () {
		enemy_list = FindObjectsOfType <enemy> ().ToList();
		foreach (enemy enemy in enemy_list) {
			enemy.player = Player;
		}
	}

	public List <enemy> get_current_list() { // retour simple.
		return (enemy_list);
	}

	public List <enemy> get_updated_list() { // demande un peu de ressources.
		enemy_list = FindObjectsOfType <enemy> ().ToList();
		return (enemy_list);
	}

	public void reset_enemy () {
		foreach (enemy enemy in enemy_list) {
			enemy.gameObject.transform.position = enemy.initial_pos;
			enemy.dead = false;
			enemy.alerted = false;
			enemy.standby = true;
		}
	}

	public void set_updated_list(List <enemy> new_list) {
		enemy_list = new_list;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
