using UnityEngine;
using System.Collections;

public class BackgroundFadeManager : MonoBehaviour {

	public GameObject	hero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(hero.transform.position.x, hero.transform.position.y - 8f);
	}
}