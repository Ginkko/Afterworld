using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AltarInteract : MonoBehaviour {
	public AudioClip idolDepositSfx;
	AudioSource audio;
	GameObject player;
	LevelSpawn levelSpawn;
	PlayerInventory inventory;

	void Start() 
	{
		audio = GetComponent<AudioSource>();
	}

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
			audio.PlayOneShot(idolDepositSfx, 1.0F);
			inventory.idolCarryToggle();
			inventory.currentLevel += 1;
			Debug.Log ("Current Level (from altar): " + inventory.currentLevel);
			levelSpawn.spawn(inventory.currentLevel);
		}
	}

}
