using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number_grid : MonoBehaviour {

	public GameObject number;
	private List< Vector3> points;
	private List< GameObject> numbers;
	private bool keypress = false;
	private float v_lock = 0.0f;
	private float degrees = 0.0f;
	private float horizontal_degrees = 0.0f;

	void Start () {
		points = new List< Vector3> ();
		numbers = new List< GameObject> ();

		for (int i = 1; i < 10; i++) {
			points.Add (new Vector3 ( i * 2.0f, 1f, 0));   	// positive x values
			points.Add (new Vector3 ( -i * 2.0f, 1f, 0));  	// negative x values
			points.Add (new Vector3 ( 0, 1f, i * 2.0f)); 	// positive y values
			points.Add (new Vector3 ( 0, 1f, -i * 2.0f));	// negative y-values
		}

		foreach (Vector3 point in points) {
			GameObject load_number = Instantiate (number, point, Quaternion.identity);
			load_number.GetComponent< TextMesh> ().text = point.x != 0.0f ? point.x.ToString() : point.z.ToString();

			load_number.transform.SetParent (transform);
			numbers.Add (load_number);
		}
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftArrow) && !keypress) {
			degrees += 45.0f;
			foreach (GameObject number in numbers)
				number.transform.Rotate (Vector3.up, 45f);
			keypress = true;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) && !keypress) {
			degrees -= 45.0f;
			foreach (GameObject number in numbers)
				number.transform.Rotate (Vector3.down, 45f);
			keypress = true;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && v_lock < 36f && !keypress) {
			v_lock += 12f;
			foreach (GameObject number in numbers)
				number.transform.Rotate (Vector3.right, 12f);
			keypress = true;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow) && v_lock > 0f && !keypress) {
			v_lock -= 12f;
			foreach (GameObject number in numbers)
				number.transform.Rotate (Vector3.left, 12f);
			keypress = true;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			foreach (GameObject number in numbers)
				number.transform.rotation = Quaternion.identity;
		}
			
		keypress = false;
	}
		
}