using UnityEngine;
using System.Collections;

public class LightDetector : MonoBehaviour {
	
	public bool inLight;
	public float healSpeed;
	public float damageSpeed;

	GameObject player;
	PlayerHealth playerHealth;
	// Use this for initialization

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}

	void Start () {
		inLight = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		inLight = false;
		
		var rayCaster = GameObject.FindWithTag("Sun");
		
		// Check if the player's head is in light
		
		RaycastHit torsoInfo;
		if(Physics.Linecast(transform.position, rayCaster.transform.position, out torsoInfo))
		{
			if (torsoInfo.collider.tag == "Sun") 
			{
				inLight = true;
			}
		}
		
//		 Check if the player's feet are in light
		Ray torsoToFeet = new Ray(transform.position, -Vector3.up);
		RaycastHit feetInfo;
			
		if (Physics.Raycast(torsoToFeet, out feetInfo))
		{
			RaycastHit sunbeamInfo;
			if(Physics.Linecast (rayCaster.transform.position, feetInfo.point, out sunbeamInfo))
			{
				if(V3Equal(sunbeamInfo.point, feetInfo.point)) 
				{
					inLight = true; 
				} 
			}
		}

		if (inLight)
		{

			playerHealth.TakeDamage(Time.deltaTime * healSpeed);

		}
		else
		{
			playerHealth.TakeDamage(Time.deltaTime * damageSpeed);

		}


	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.0001;
	}
	
}