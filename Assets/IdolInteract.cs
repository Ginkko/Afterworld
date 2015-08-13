using UnityEngine;
using System.Collections;

public class IdolInteract : MonoBehaviour {
	GameObject player;
	LevelSpawn levelSpawn;
	PlayerInventory inventory;

	// Use this for initialization
	void Awake () {

		levelSpawn = GameObject.Find ("LevelSpawnMarker").GetComponent<LevelSpawn> ();
		player = GameObject.FindGameObjectWithTag("Player");
		inventory = player.GetComponent<PlayerInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)

	{

		if (inventory.currentLevel == 0)

		{
			Debug.Log ("Current Level (from idol): " + inventory.currentLevel);
			levelSpawn.spawn(inventory.currentLevel);
		}

		Destroy (gameObject);
		inventory.idolCarryToggle();
	}
	
}
