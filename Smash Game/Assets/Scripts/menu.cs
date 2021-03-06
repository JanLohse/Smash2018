﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
    public bool option_menu = false;
    private bool played_menu = false;

    public GameObject option_screen;
    public AudioSource ButtonSound;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update(){
        CheckOptionScreen();
    }
    void CheckOptionScreen(){
        if(option_menu == true){
            option_screen.SetActive (true);
            if (!played_menu)
            {
                ButtonSound.Play();
                played_menu = true;
            }
        } else{
            option_screen.SetActive(false);
            if (played_menu)
            {
                ButtonSound.Play();
                played_menu = false;
            }
        }
 }
     public void StartGame(){
        ButtonSound.Play();
        SceneManager.LoadScene (1);
    }

    public void QuitGame() {
        ButtonSound.Play();
        Application.Quit();
    }

    public void Option()
    {
        ButtonSound.Play();
        option_menu = true;
        }

    public void CloseOptionScreeen(){
        option_menu = false;
    }
}
