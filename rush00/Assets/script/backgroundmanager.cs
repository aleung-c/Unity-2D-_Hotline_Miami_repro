using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class backgroundmanager : MonoBehaviour 
{
	public Color[] listColor = new Color[]{};
	private float duration = 5.0f;
	private static int index = 0;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer < duration)
			Camera.main.backgroundColor = Color.Lerp (listColor[index], listColor[index + 1], timer/duration);
		else
		{
			timer = 0.0f;
			index += 1;
			if (index > listColor.Length - 2)
				index = 0;
		}
	}
}