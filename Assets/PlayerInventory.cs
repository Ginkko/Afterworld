using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
	bool isCarryingIdol;

	// Use this for initialization
	void Start () {
		isCarryingIdol = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void idolCarryToggle () {
		isCarryingIdol = !isCarryingIdol;
		Debug.Log ("Holding Idol: " + isCarryingIdol);
	}

}
