using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public void loadLevel(string name) {
		Application.LoadLevel (name);
	}

	public void quit() {
		Application.Quit ();
	}
	
	void update () {
		if (Input.GetKeyDown("joystick button 0")) {
			Application.LoadLevel("game");
		}
		else if (Input.GetKeyDown("joystick button 1")) {
			Application.Quit ();
		}

	}


}

