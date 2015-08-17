using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class DuckAmbientOnApproach : MonoBehaviour {
	public AudioMixer musicController;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var player = GameObject.FindWithTag("GameController");
		var distance = Vector3.Distance(transform.position, player.transform.position);
//		Debug.Log (distance);
		if (distance < 40) {
			float ambientVol = -(80 - distance * 2) - 12;
			musicController.SetFloat("Ambient", ambientVol);
		} 

	}
}
