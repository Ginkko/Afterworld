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
	FirstPersonController firstPersonController;
	Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
	SpawnController spawnController;

//    PlayerMovement playerMovement;                              // Reference to the player's movement.
//    PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
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
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
		spawnController = GetComponent <SpawnController> ();
		firstPersonController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<FirstPersonController>();
//        playerMovement = GetComponent <PlayerMovement> ();
//        playerShooting = GetComponentInChildren <PlayerShooting> ();

		damageImage.color = Color.black;
        currentHealth = maxHealth;
		spawnPoint = "PlayerSpawn0";
		spawnController.spawn(spawnPoint);
		firstPersonController.enabled = true;
	}


    void Update ()
    {
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
				victoryAlert.CrossFadeAlpha(255, 3, true);
			}

		}


		else if (respawning)
		{
			firstPersonController.enabled = true;
			damageImage.color = Color.Lerp (Color.black, Color.clear,  timer / totalFadeLength );
//			Debug.Log(damageImage.color);
			if (timer > totalFadeLength)
			{
				Debug.Log ("Respawning finished");
				respawning = false;
				timer = 0;
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
//				Debug.Log ("Timer:" + timer);

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
        if (amount > 0)
		{
			damaged = true;
		}
        // Reduce the current health by the damage amount.
        currentHealth -= amount;
	

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

		if (currentHealth > maxHealth) 
		{
			currentHealth = maxHealth;
		}
//		Debug.Log ("Health: " + currentHealth);
//		Debug.Log ("Dead?: " + isDead);

        // Play the hurt sound effect.
//        playerAudio.Play ();

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
	}


    void Death ()
    {
		firstPersonController.enabled = false;
		audio.PlayOneShot(deathSfx, 1.0F);
        // Set the death flag so this function won't be called again.
		timer = 0;
		isDead = true;
//		Debug.Log ("isDead: " + isDead);
		currentHealth = 0;
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
//        playerAudio.clip = deathClip;
//        playerAudio.Play ();

    }
}
