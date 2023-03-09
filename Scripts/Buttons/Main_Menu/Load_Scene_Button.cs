using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene_Button : Button_Manager
{
    public int What_Scene_To_Load;
    public override void Button_function(){
        Time.timeScale = 1;
        SceneManager.LoadScene(What_Scene_To_Load);
    }
}
