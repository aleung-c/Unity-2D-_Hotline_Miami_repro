using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject Player;
	public GameObject EnemyManager;
	public GameObject RoomManager;
	public GameObject  UI_end;
	private List <enemy> enemy_list;
	private bool		_game_over = false;
	private bool		_game_win = false;
	public AudioSource	death_sound;
	public AudioSource	win_sound;
	// Use this for initialization
	void Start () {
		enemy_list = EnemyManager.GetComponent<EnemyManager> ().get_updated_list ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.GetComponent<hero> ().isAlive == false && _game_win == false) { // Game Over
			UI_end.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			enemy_list = EnemyManager.GetComponent<EnemyManager> ().get_current_list ();
			foreach (enemy enemy in enemy_list) {
				enemy.action = false;
			}

			if (_game_over == false)
				death_sound.Play();
			_game_over = true;
			_game_win = false;
			if (Input.GetKeyDown("return")) {
				Application.LoadLevel("level0");
			}
		}
		enemy_list = EnemyManager.GetComponent<EnemyManager> ().get_current_list ();
		int enemy_nb = enemy_list.Count ();
		if (enemy_nb == 0 && _game_over == false) { // win
			_game_over = false;
			if (_game_win == false)
				win_sound.Play ();
			_game_win = true;
			UI_end.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			UI_end.transform.Find("main_text").GetComponent<Text> ().text = "Level Clear";
			Color32 new_color;
			new_color.r = 228;
			new_color.g = 190;
			new_color.b = 79;
			new_color.a = 100;
			UI_end.GetComponent<Image> ().color = new_color;
			if (Input.GetKeyDown("return")) {
				Application.LoadLevel("Title_screen");
			}
		}
	}
}
