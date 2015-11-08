using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title_screen : MonoBehaviour {

	public Text text_1;
	public Text text_2;
	public Text text_3;
	public Text text_4;
	public Text text_5;

	private int rot = 0;
	int rotMin = -45;
	int rotMax = 45;
	int	direction = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return")) {
			Application.LoadLevel("level0");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
		rot += direction;
		if (rot >= rotMax)
			direction = -1;
		if (rot <= rotMin)
			direction = 1;
		text_1.rectTransform.localRotation = Quaternion.Euler(text_1.rectTransform.localRotation.x, text_1.rectTransform.localRotation.y, text_1.rectTransform.localRotation.z + rot);
		text_2.rectTransform.localRotation = Quaternion.Euler(text_2.rectTransform.localRotation.x, text_2.rectTransform.localRotation.y, text_2.rectTransform.localRotation.z + rot);
		text_3.rectTransform.localRotation = Quaternion.Euler(text_3.rectTransform.localRotation.x, text_3.rectTransform.localRotation.y, text_3.rectTransform.localRotation.z + rot);
		text_4.rectTransform.localRotation = Quaternion.Euler(text_4.rectTransform.localRotation.x, text_4.rectTransform.localRotation.y, text_4.rectTransform.localRotation.z + rot);
		text_5.rectTransform.localRotation = Quaternion.Euler(text_5.rectTransform.localRotation.x, text_5.rectTransform.localRotation.y, text_5.rectTransform.localRotation.z + rot);


	}
}
