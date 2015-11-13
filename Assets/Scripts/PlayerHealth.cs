using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{
	public AudioClip hurtSfx;
	public AudioClip deathSfx;
	AudioSource audio;

  public int maxHealth = 100;                            // The amount of health the player starts the game with.
  public float currentHealth;                                   // The current health the player has.
  public Slider healthSlider;                                 // Reference to the UI's health bar.
  public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
  public float damageFlashSpeed;                               // The speed the damageImage will fade at.
	public float deathFadeSpeed;
	public float totalFadeLength;
  public Color flashColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public Color deathColor;
	public Color victoryColor;
	public float timeBetweenDamageFeedback = 2f;
	public Image victoryAlert;
	public Image instructions;
	FirstPersonController firstPersonController;
	SpawnController spawnController;

	public bool isDead = false;                                                // Whether the player is dead.
	public bool damaged = false;                                               // True when the player gets damaged.
	float timer;
	string spawnPoint;
	public bool respawning = true;
	public bool isVictorious = false;

	void Start ()
	{
		audio = GetComponent<AudioSource>();

	}

    void Awake ()
    {
    // Setting up the references.
		spawnController = GetComponent <SpawnController> ();
		firstPersonController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<FirstPersonController>();
		damageImage.color = Color.black;
    currentHealth = maxHealth;
		spawnPoint = "PlayerSpawn0";
		spawnController.spawn(spawnPoint);
		firstPersonController.enabled = true;
	}


    void Update ()
    {

			if(Input.GetButtonDown("Cancel"))
			{
				Application.Quit();
			}

			timer += Time.deltaTime;

			if (isVictorious)
			{
				firstPersonController.enabled = false;
				Debug.Log ("V I C T O R Y");
				Color damageImageSnapshot = damageImage.color;
				damageImage.color = Color.Lerp (damageImageSnapshot, victoryColor, timer / totalFadeLength );
				if (timer > totalFadeLength)
				{
					damageImage.color = victoryColor;
					Debug.Log (" WINNING COMPLETED ");
					Color a = Color.white;
					a.a = 255;
					victoryAlert.color = a;

				}

			}


		else if (respawning)
		{
			firstPersonController.enabled = true;
			damageImage.color = Color.Lerp (Color.black, Color.clear,  timer / totalFadeLength );
			if (timer > totalFadeLength)
			{
				Debug.Log ("Respawning finished");
				respawning = false;
				timer = 0;
				instructions.color = Color.clear;
			}

		}

		else if (isDead)
		{

			Debug.Log ("Dying");
			Color damageImageSnapshot = damageImage.color;
			damageImage.color = Color.Lerp (damageImageSnapshot, deathColor, timer / totalFadeLength);
			Debug.Log ("Death fade timer: " + timer);
			if (timer > totalFadeLength)
			{
				respawning = true;
				isDead = false;
				currentHealth = maxHealth;
				damaged = false;
				timer = 0;
				spawnController.spawn(spawnPoint);

			}
		}

		else
		{


				if(timer > timeBetweenDamageFeedback)
				{

					timer = 0f;
					// If the player has just been damaged...

					if(damaged)
			        {
			            // ... set the colour of the damageImage to the flash colour.
			            damageImage.color = flashColor;
						audio.PlayOneShot(hurtSfx, 1.0F);
			        }
			        // Otherwise...
			        damaged = false;

				}
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
		}

	}



    public void TakeDamage (float amount)
    {
        // Set the damaged flag so the screen will flash.

        if (amount > 0) {
			damaged = true;
		} else if (amount < 0) {
			damaged = false;
		}

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}


        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
			Death ();
		}
   	 }

	public void Victory ()
	{
		isVictorious = true;
		timer = 0;
	}


    void Death ()
    {
		firstPersonController.enabled = false;
		audio.PlayOneShot(deathSfx, 1.0F);

		timer = 0;
		isDead = true;

    currentHealth = 0;

    }
}
