using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicController : MonoBehaviour {
	public AudioMixer musicController;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = GetComponent<PlayerHealth> ();
	}	
	
	// Update is called once per frame
	void Update () {
		float lightVol = (playerHealth.currentHealth / playerHealth.maxHealth) * 80 - 80;
		float darkVol = (80 + lightVol * 2) * -1;
		if (lightVol > -3)
		{
			lightVol = -3;
		}
		if (darkVol > 0)
		{
			darkVol = 0;
		}
		musicController.SetFloat("Lightness", lightVol);
		musicController.SetFloat("Darkness", darkVol);
	
	}
}
