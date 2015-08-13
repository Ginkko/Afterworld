using UnityEngine;
using System.Collections;

public class AltarInteract : MonoBehaviour {
	GameObject player;
	LevelSpawn levelSpawn;
	PlayerInventory inventory;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		inventory = player.GetComponent<PlayerInventory> ();
		levelSpawn = GameObject.Find ("LevelSpawnMarker").GetComponent<LevelSpawn> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)

	{
		if (inventory.idolStatus())
		{
			inventory.idolCarryToggle();
			inventory.currentLevel ++;
			Debug.Log ("Current Level: " + inventory.currentLevel);

			levelSpawn.spawn(inventory.currentLevel);
		}
	}

}
