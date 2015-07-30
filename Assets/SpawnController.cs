using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
	string spawnPoint;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawn(string spawnPoint)
	{
		GameObject spawnObject = GameObject.Find(spawnPoint);
		GameObject player = GameObject.Find("FPSController");
		player.transform.position = spawnObject.transform.position;
	}

}
