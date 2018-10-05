using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour {

    public Animator anim;

    public void camShake(string i)
    {
        if(i == "normal")
        {
            anim.SetTrigger("shake");
        }
        else if(i == "stronger")
        {
            anim.SetTrigger("shake_stronger");
        }
        else if(i == "omega")
        {
            anim.SetTrigger("shake_omega");
        }
        else if(i == "omega_stronger")
        {
            anim.SetTrigger("shake_omega_stronger");
        }
    }
    
}
