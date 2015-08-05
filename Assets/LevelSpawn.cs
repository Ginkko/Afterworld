using UnityEngine;
using System.Collections;

public class LevelSpawn : MonoBehaviour {
	public GameObject Altar;
	public GameObject levelGroup1;
	int levelId;
	bool fadingOut = true;
	bool fadingIn;
	bool spawning;
	GameObject oldRayCaster;
	Light oldDirLight;
	Light newDirLight;
	// Use this for initialization
	void Awake () {

	}

	public void spawn (int levelId)
	{
		if (levelId == 0)
		{

			var oldRayString = string.Format("raycaster{0}", levelId);
			var oldLightString = string.Format("SunLevel{0}", levelId);

			oldRayCaster = GameObject.Find (oldRayString);
			oldDirLight = GameObject.Find (oldLightString).GetComponent<Light> ();
			newDirLight = oldDirLight;
			spawning = true;
			Debug.Log ("Spawning level" + levelId);
		}
		else
		
		{
			oldRayCaster = GameObject.Find ("raycaster{levelId - 1}");
			oldDirLight = GameObject.Find ("SunLevel{levelId - 1}").GetComponent<Light> ();
			newDirLight = GameObject.Find ("SunLevel{levelId}").GetComponent<Light> ();
			spawning = true;
			Debug.Log ("Spawning level" + levelId);
		}
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
					Debug.Log("Light Intensity: " + oldDirLight.intensity);
					oldDirLight.intensity = Mathf.Lerp(oldDirLight.intensity, 0f, 1f * Time.deltaTime);
					
					
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
						fadingIn = true;
					}
				}
			}
			if (fadingIn) 
			{
				newDirLight.intensity = Mathf.Lerp(newDirLight.intensity, 8f, 1f * Time.deltaTime);
				Debug.Log ("New light intensity: " + newDirLight.intensity);
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
