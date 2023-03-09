using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public AnimationCurve Spawn_rate;
    public float Time_between_Spawns;
    public float current_time = 0f;
    public GameObject[] En;
    public Transform[] Spawn_Points;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){

        yield return new WaitForSeconds(Time_between_Spawns);
        current_time += Time_between_Spawns;
        
        //Debug.Log(Spawn_rate.Evaluate(current_time / 20));

        for (int i = 0 ; i < Spawn_rate.Evaluate(current_time / 20) * 10; i++){
            
            Debug.Log("spawn" + i);
            Instantiate(En[Random.Range(0,En.Length)] , Spawn_Points[Random.Range(0 , Spawn_Points.Length)]);
        }

        StartCoroutine(Spawn());
    }


}
