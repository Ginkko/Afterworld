using UnityEngine;
using System.Collections;

public class IdolInteract : MonoBehaviour {
	GameObject level;
	GameObject player;
	LevelSpawn levelSpawn;
	PlayerInventory inventory;

	// Use this for initialization
	void Start () {
		level = GameObject.FindGameObjectWithTag ("Level1");
		levelSpawn = level.GetComponent<LevelSpawn> ();
		player = GameObject.FindGameObjectWithTag("Player");
		inventory = player.GetComponent<PlayerInventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)

	{
		levelSpawn.spawn();
		Destroy (gameObject);
		inventory.idolCarryToggle();
	}
	
}
