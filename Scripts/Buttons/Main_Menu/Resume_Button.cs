using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume_Button : Menu_button
{
    GameObject Player;
    void Awake(){
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Button_function()
    {
        base.Button_function(); //Keep orginal functionality of closing window saves space and time
        Player.GetComponent<Character_Controller>().enabled = true;
        Time.timeScale = 1;
    }
}
