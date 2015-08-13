using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
	bool isCarryingIdol;
	public int currentLevel;

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		isCarryingIdol = false;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool idolStatus () {
		return isCarryingIdol;
	}

	public void idolCarryToggle () {
		isCarryingIdol = !isCarryingIdol;
		Debug.Log ("Holding Idol: " + isCarryingIdol);
	}

}
