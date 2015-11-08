using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title_screen : MonoBehaviour {

	public Text text_1;
	public Text text_2;
	public Text text_3;
	public Text text_4;
	public Text text_5;

	public Text loading;
	public Text loading1;

	private float rot = 0;
	int rotMin = -10;
	int rotMax = 10;
	float	direction = 1f;

	float	scaleMax = 1.5f;
	float	scaleMin = 1.2f;
	float	directionScale = 1.0f;
	float	scale = 1.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return")) {
			loading.text = "Loading...";
			loading1.text = "Loading...";
			Application.LoadLevel("level0");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
		rot += direction * Time.deltaTime * 3;
		if (rot >= rotMax)
			direction = -1;
		if (rot <= rotMin)
			direction = 1;

		text_1.rectTransform.localRotation = Quaternion.Euler(text_1.rectTransform.localRotation.x, text_1.rectTransform.localRotation.y, text_1.rectTransform.localRotation.z + rot);
		text_2.rectTransform.localRotation = Quaternion.Euler(text_2.rectTransform.localRotation.x, text_2.rectTransform.localRotation.y, text_2.rectTransform.localRotation.z + rot);
		text_3.rectTransform.localRotation = Quaternion.Euler(text_3.rectTransform.localRotation.x, text_3.rectTransform.localRotation.y, text_3.rectTransform.localRotation.z + rot);
		text_4.rectTransform.localRotation = Quaternion.Euler(text_4.rectTransform.localRotation.x, text_4.rectTransform.localRotation.y, text_4.rectTransform.localRotation.z + rot);
		text_5.rectTransform.localRotation = Quaternion.Euler(text_5.rectTransform.localRotation.x, text_5.rectTransform.localRotation.y, text_5.rectTransform.localRotation.z + rot);

		scale += directionScale * Time.deltaTime / 10;
		if (scale >= scaleMax)
			directionScale = -1;
		if (scale <= scaleMin)
			directionScale = 1;

		text_1.rectTransform.localScale = new Vector2(scale, scale);
		text_2.rectTransform.localScale = new Vector2(scale + 0.1f, scale + 0.1f);
		text_3.rectTransform.localScale = new Vector2(scale + 0.2f, scale + 0.2f);
		text_4.rectTransform.localScale = new Vector2(scale + 0.3f, scale + 0.3f);
		text_5.rectTransform.localScale = new Vector2(scale + 0.4f, scale + 0.4f);
	}
}
