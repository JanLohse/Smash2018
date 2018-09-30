using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public float runSpeed = 40f;
    public int playernumber;
    public float factor = 1;
    public AudioSource HitSound;

    float horizontalMove = 0f;
	bool jump = false;
   

    // Update is called once per frame
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
                            factor += 0.5f;
                        }
                        else if (!controller.GetFacingRight() && distance.x > 0)
                        {
                            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f * factor, 50f * factor));
                            factor += 0.5f;
                        }
                        
                        
                    }
                    else
                    {
                    HitSound.Play(0);
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
            }
        }
		jump = false;
	}
}
