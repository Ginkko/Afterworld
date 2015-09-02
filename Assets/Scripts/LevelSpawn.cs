using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class LevelSpawn : MonoBehaviour {
	public GameObject Altar;
	public GameObject levelGroup1;
	public GameObject levelGroup2;
	public AudioClip sunShiftSfx;
	AudioSource audio;
	int levelId;
	bool fadingOut;
	bool fadingIn;
	bool spawning;
	GameObject oldRayCaster;
	Light oldDirLight;
	Light newDirLight;
	PlayerHealth playerHealth;

	void Start() 
	{
		audio = GetComponent<AudioSource>();
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

	// Use this for initialization
	void Awake () {

	}

	public void spawn (int level)
	{
		levelId = level;
		Debug.Log ("Spawning level" + levelId);
		if (levelId == 0)
		{

			var oldRayString = string.Format("raycaster{0}", levelId);
			var oldLightString = string.Format("SunLevel{0}", levelId);

			oldRayCaster = GameObject.Find (oldRayString);
			oldDirLight = GameObject.Find (oldLightString).GetComponent<Light> ();
			newDirLight = oldDirLight;


		}

		else if (levelId == 3)
		{
			playerHealth.Victory();	
		}


		else
		
		{
			var oldRayString = string.Format("raycaster{0}", levelId - 1);
			var oldLightString = string.Format("SunLevel{0}", levelId - 1);
			var newLightString = string.Format ("SunLevel{0}", levelId);

			oldRayCaster = GameObject.Find (oldRayString);
			oldDirLight = GameObject.Find (oldLightString).GetComponent<Light> ();
			newDirLight = GameObject.Find (newLightString).GetComponent<Light> ();

//			Debug.Log ("Old Raycaster: " + oldRayCaster.name);
//			Debug.Log ("Old DirLight: " + oldDirLight.name);
//			Debug.Log ("Old newDirLight: " + newDirLight.name);

//			oldRayCaster = GameObject.Find ("raycaster{levelId - 1}");
//			Debug.Log (oldRayCaster.name);
//			oldDirLight = GameObject.Find ("SunLevel{levelId - 1}").GetComponent<Light> ();

		}

		spawning = true;
		fadingOut = true;
		audio.PlayOneShot(sunShiftSfx, 1.0F);

	}

	// Update is called once per frame
	void Update ()
	{
		if (spawning)
		{
			if (fadingOut)
			{
				if (oldDirLight.intensity > .1)
				{
//					Debug.Log("Light Intensity: " + oldDirLight.intensity);
					oldDirLight.intensity = Mathf.Lerp(oldDirLight.intensity, 0f, .5f * Time.deltaTime);
					

					if (oldDirLight.intensity < .1)
					{
						oldDirLight.intensity = 0;
						Destroy(oldRayCaster);
						fadingOut = false;

						if (levelId == 0)
						{
							Instantiate(Altar);
						}
						else if (levelId == 1)
						{
							Instantiate(levelGroup1);
						}
						else if (levelId == 2)
						{
							Instantiate(levelGroup2);
							Debug.Log ("Instantiate level2");
						}

						fadingIn = true;
					}
				}
			}
			if (fadingIn) 
			{
				newDirLight.intensity = Mathf.Lerp(newDirLight.intensity, 8f, .5f * Time.deltaTime);
//				Debug.Log ("New light intensity: " + newDirLight.intensity);
				if (newDirLight.intensity > 7.9)
				{
					newDirLight.intensity = 8;
					fadingIn = false;
					spawning = false;
				}
				
			}
		}

	}
}
