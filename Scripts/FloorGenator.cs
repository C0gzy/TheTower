using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenator : MonoBehaviour
{
    public int Max_floor_Count = 10;
    public GameObject[] FLoor_Themes;
    public List<GameObject> Next_Floor_Themes;
    public int Current_Floor_Value = 0;
    private GameObject floor;
    private GameObject Player;
    private GameObject StairsUp;

    void Gen_Floors(){
        for (int i = 0 ; i < Max_floor_Count ; i++){
            GameObject Current_Floor = FLoor_Themes[Random.Range(0 , FLoor_Themes.Length)];
            for (int k = -5 ; k < 0 ; k++){
                if(i + k < 0){ // first five floors may be the same due to this
                    break;
                }
                else if (Next_Floor_Themes[i + k] == Current_Floor){
                    Current_Floor = FLoor_Themes[Random.Range(0 , FLoor_Themes.Length)];
                    k = -5;
                    continue;
                }

            }
            Next_Floor_Themes.Add(Current_Floor);
        }

    }



    void Start() {
        Player = GameObject.FindWithTag("Player");
        Gen_Floors();
        Create_Current_Floor();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.L)){
            Current_Floor_Value ++;
            Create_Current_Floor();
        }

        if ((StairsUp != null) && (StairsUp.transform.position - Player.transform.position).magnitude <= 2){
            Current_Floor_Value ++;
            Create_Current_Floor();
        }
    }

    void Create_Current_Floor(){
        if (floor == null){
            floor = Instantiate(Next_Floor_Themes[Current_Floor_Value], new Vector3(0,0,0) , Quaternion.identity);
            Player.transform.position = floor.gameObject.transform.Find("Player_Spawn").transform.position;
            StairsUp = GameObject.FindWithTag("stairsUp");
        }else{
            Destroy(floor); floor = null;            
            Create_Current_Floor();
        }
        
    }
}
