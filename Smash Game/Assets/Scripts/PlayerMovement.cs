using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour {


    //BITTE KOMMENTIEREN DANKE JUNGS


	public CharacterController2D controller;
	public float runSpeed = 40f;
    public int playernumber;
    public float damage = 1;
    public float factor = 1;
    public Text factorText;
    public AudioSource HitSound;
    public AudioSource MissSound;
    private screenshake shake;

	float horizontalMove = 0f;
	bool jump = false;
    // Update is called once per frame



    void Start()
    {
        //Screenshake
        shake = GameObject.FindGameObjectWithTag("screenshake").GetComponent<screenshake>();
    }

    private void Awake()
    {
        factorText.text = "" + damage + "x";
        
    }

    void Update ()
    {

		horizontalMove = Input.GetAxisRaw("Horizontal_P"+playernumber) * runSpeed;

		if (Input.GetButtonDown("Jump_P" + playernumber))
		{
			jump = true;
		}
        if (Input.GetButtonDown("Fire_P" + playernumber))
        {
            Hit();
        }
	}
        

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        if (controller.isDead())
        {
            var players = GameObject.FindGameObjectsWithTag("Player")
                                .Where(x => x != this.gameObject);
            foreach (var player in players)
            {
                player.GetComponent<PlayerMovement>().damage = 1;
                player.GetComponent<PlayerMovement>().factorText.text = "" + player.GetComponent<PlayerMovement>().damage + "x";

            }
        }
		jump = false;
	}

    private void Hit ()
    {
        var players = GameObject.FindGameObjectsWithTag("Player")
                            .Where(x => x != this.gameObject);
        foreach (var player in players)
        {
            Vector2 distance = transform.position - player.transform.position;
            if (distance.magnitude < 1)
            {
                if (controller.GetFacingRight() && distance.x < 0)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f * (float) Math.Sqrt(damage) * factor, 50f * (float) Math.Sqrt(damage) * factor));
                    damage += 0.5f;
                    factorText.text = "" + Math.Round(Math.Sqrt(damage) * factor, 1) + "x";
                    HitSound.Play(0);
                    shakeScreen();
                }
                else if (!controller.GetFacingRight() && distance.x > 0)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f * (float) Math.Sqrt(damage) * factor, 50f * (float) Math.Sqrt(damage) * factor));
                    damage += 0.5f;
                    factorText.text = "" + Math.Round(Math.Sqrt(damage) * factor, 1) + "x";
                    HitSound.Play(0);
                    shakeScreen();
                }
                else
                {
                    MissSound.Play(0);
                }
            }
            else
            {
                MissSound.Play(0);
            }
        }
    }

    private void shakeScreen()
    {
        if (damage < 3.5f)
        {
            shake.camShake("normal");
        }
        else if (damage < 7.5f)
        {
            shake.camShake("stronger");
        }
        else if (damage < 13f)
        {
            shake.camShake("omega");
        }
        else if (damage >= 13f)
        {
            shake.camShake("omega_stronger");
        }
    }
}

