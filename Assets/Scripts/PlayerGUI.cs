using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;
	
	void Start ()
	{
		currentHealth = maxHealth;
	}
	
	void Update()
	{
		currentHealth -= Time.deltaTime * 5;

	}
	
	void OnGUI()
	{
		
		GUILayout.BeginArea(new Rect(10f, 10f, Screen.width, Screen.height));	
		GUILayout.Label("Health:  " + (int)currentHealth + "/" + maxHealth);
		GUILayout.EndArea();
	}
}
