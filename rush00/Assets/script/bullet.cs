using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public Rigidbody2D	rb;
	private float		speed = 15f;
	public bool			isShot = false;
	private	float		margin = 0.5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag != "weapon" && col.gameObject.tag != "enemy" && col.gameObject.tag != "room_layout" &&  isShot)
		{
			//Debug.Log("collision with : " + col.gameObject);
			//Debug.Log("collision with name : " + col.gameObject.name);
			GameObject.Destroy(this.gameObject);
		}
		else if (col.gameObject.tag == "enemy" && GetComponent<Collider2D>().bounds.Contains(col.gameObject.transform.position) && isShot)
		{
			col.gameObject.GetComponent<enemy> ().set_dead();
			GameObject.Destroy(this.gameObject);
		}

	}

	public float getSpeed()
	{
		return speed;
	}

}
