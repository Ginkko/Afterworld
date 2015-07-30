using UnityEngine;
using System.Collections;

public class LevelSpawn : MonoBehaviour {
	public GameObject levelGroup;
	bool fadingIn;
	bool spawning;
	GameObject oldRayCaster;
	GameObject oldLight;
	public Light oldDirLight;
	public Light newDirLight;
	// Use this for initialization
	void Start () {
		oldRayCaster = GameObject.Find("raycaster");
		oldLight = GameObject.Find("SunLevel0");
	}

	public void spawn ()
	{
		spawning = true;
		Debug.Log ("Spawning: " + spawning);
	}

	// Update is called once per frame
	void Update ()
	{
		if (spawning)
		{


			if (oldDirLight.intensity > .1)
			{
//				Debug.Log("Light Intensity: " + oldDirLight.intensity);
				oldDirLight.intensity = Mathf.Lerp(oldDirLight.intensity, 0f, 1f * Time.deltaTime);
				
				
				if (oldDirLight.intensity < .1)
				{
					oldDirLight.intensity = 0;
					Destroy(oldRayCaster);
					Instantiate(levelGroup);
					fadingIn = true;
				}
			}
			
			if (fadingIn) 
			{
				newDirLight.intensity = Mathf.Lerp(newDirLight.intensity, 8f, .1f * Time.deltaTime);
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
