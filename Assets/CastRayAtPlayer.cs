using UnityEngine;
using System.Collections;

public class CastRayAtPlayer : MonoBehaviour {

	public bool inLight;
	
	// Use this for initialization
	void Start () {
		inLight = false;
	}
	
	// Update is called once per frame
	void Update () {

		inLight = false;

		var player = GameObject.FindWithTag("Player");

		// Check if the player's head is in light

		RaycastHit torsoInfo;
		if(Physics.Linecast(transform.position, player.transform.position, out torsoInfo))
		{
			if (torsoInfo.collider.tag == "Player") 
			{
				inLight = true;
			}
		}

		// Check if the player's feet are in light
		Ray torsoToFeet = new Ray(player.transform.position, -Vector3.up);
		RaycastHit feetInfo;

		if (Physics.Raycast(torsoToFeet, out feetInfo))
		{
			RaycastHit sunbeamInfo;
			if(Physics.Linecast (transform.position, feetInfo.point, out sunbeamInfo))
			{
				if(V3Equal(sunbeamInfo.point, feetInfo.point)) 
				{
					inLight = true; 
				} 
			}
		}

	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.0001;
	}

}
