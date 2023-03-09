using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_button : Button_Manager
{
    public GameObject Menu_to_Open;

    public override void Button_function(){
        Menu_to_Open.SetActive(!Menu_to_Open.activeInHierarchy);
        Text_Mesh_Pro.color = Color.white;
    }
}
