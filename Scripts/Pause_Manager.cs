using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Manager : MonoBehaviour
{
    public GameObject Menu_to_Open;
    GameObject Player;
    void Awake(){
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)){
            Menu_to_Open.SetActive(!Menu_to_Open.activeInHierarchy);
            
            if(Menu_to_Open.activeInHierarchy == false){
                Player.GetComponent<Character_Controller>().enabled = true;
                Time.timeScale = 1;
            }else{
                Player.GetComponent<Character_Controller>().enabled = false;
                Time.timeScale = 0;
            }

        }    
    }
}
