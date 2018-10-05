using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {


    //BITTE KOMMENTIEREN DANKE JUNGS


	public CharacterController2D controller;
	public float runSpeed = 40f;
    public int playernumber;
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
        factorText.text = "" + factor + "x";
        
    }
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal_P"+playernumber) * runSpeed;

		if (Input.GetButtonDown("Jump_P" + playernumber))
		{
			jump = true;
		}
        if (Input.GetButtonDown("Fire_P"+playernumber))
        {
            var players = GameObject.FindGameObjectsWithTag("Player")
                                .Where(x => x != this.gameObject);
            foreach(var player in players)
            {
                Vector2 distance = transform.position-player.transform.position;
                if(distance.magnitude < 1)
                {
                    if (controller.GetFacingRight() && distance.x < 0)
                    {
                        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(500f * factor, 50f * factor));
                        factor += 0.5f  ;
                        factorText.text = "" + factor + "x";
                        HitSound.Play(0);

                        //Einführen von Screenshake - in den Klammern steht die Stärke des Shakes
                        if (factor<3.5f)
                        {
                            shake.camShake("normal");
                        }
                        else if(factor<7.5f)
                        {
                            shake.camShake("stronger");
                        }
                        else if(factor<13f)
                        {
                            shake.camShake("omega");
                        }
                        else if(factor>=13f)
                        {
                            shake.camShake("omega_stronger");
                        }
                    }
                    else if (!controller.GetFacingRight() && distance.x > 0)
                    {
                        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f * factor, 50f * factor));
                        factor += 0.5f;
                        factorText.text = "" + factor + "x";
                        HitSound.Play(0);

                        //Einführen von Screenshake - in den Klammern steht die Stärke des Shakes
                        if (factor < 3.5f)
                        {
                            shake.camShake("normal");
                        }
                        else if (factor < 7.5f)
                        {
                            shake.camShake("stronger");
                        }
                        else if (factor < 13f)
                        {
                            shake.camShake("omega");
                        }
                        else if (factor >= 13f)
                        {
                            shake.camShake("omega_stronger");
                        }
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
                player.GetComponent<PlayerMovement>().factor = 1;
                player.GetComponent<PlayerMovement>().factorText.text = "" + player.GetComponent<PlayerMovement>().factor + "x";

            }
        }
		jump = false;
	}
}
