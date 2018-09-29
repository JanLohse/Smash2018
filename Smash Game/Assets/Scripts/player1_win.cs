using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player1_win : MonoBehaviour {
    public AudioSource ButtonSound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Back_To_Menu()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }
}
