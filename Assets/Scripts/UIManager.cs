using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public void loadLevel(string name) {
		Application.LoadLevel (name);
	}

	public void quit() {
		Application.Quit ();
	}
	
}
