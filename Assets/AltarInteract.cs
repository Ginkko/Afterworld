using UnityEngine;
using System.Collections;

public class AltarInteract : MonoBehaviour {
	GameObject player;
	LevelSpawn levelSpawn;
	PlayerInventory inventory;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		inventory = player.GetComponent<PlayerInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)

	{
		if (inventory.idolStatus())
		{
			inventory.idolCarryToggle();
			
			
		}
	}

}
