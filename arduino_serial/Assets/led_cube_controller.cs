using UnityEngine;
using System.Collections;

public class led_cube_controller : MonoBehaviour {
	public arduino_Serial_controller serialHandler;

	// Use this for initialization
	void Start () {
		serialHandler = GetComponent<arduino_Serial_controller> ();
		serialHandler.OnDataReceived += OnDataReceived;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) { 
			serialHandler.Write("0");
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			serialHandler.Write ("1");
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			serialHandler.Write ("2");
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			serialHandler.Write ("3");
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			serialHandler.Write ("J");
		} 
	} 

	void OnDataReceived(string message){
		Debug.Log (message);
	}
}
