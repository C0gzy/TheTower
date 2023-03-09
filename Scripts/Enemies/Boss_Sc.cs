using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Sc : Projectile_Enemies
{

    [Header("Boss Settings")]
    private int Attack_Type = 0;
    public GameObject[] Which_Enemies_To_Spawn;
    public int How_Many_To_Spawn;
    public GameObject Parent_To_Spawn_To;
    public int Shoot_Times;

    public virtual void Create_Enemies(){
        for (int i = 0 ; i < How_Many_To_Spawn; i++){
            var New_Entity = Instantiate(Which_Enemies_To_Spawn[Random.Range(0,Which_Enemies_To_Spawn.Length)] , Shoot_positions[Random.Range(0 , Shoot_positions.Count)]);
            New_Entity.transform.parent = Parent_To_Spawn_To.transform;
        }
    }

    public virtual IEnumerator Weapon_Fire_Cooldown(){

        for (int i = 0 ; i < Shoot_Times ; i++){
            Projectile_Create_Shoot();   
            yield return new WaitForSeconds(.5f);

        }
        //Object_Animation.SetBool("Attacking" , true);

    }

    public override void Attack_Move()
    {
        StartCoroutine(Start_Cooldown());
        Attack_Type = Random.Range(0,2);
        switch(Attack_Type){
            case 0:
                Debug.Log("fire");
                StartCoroutine(Weapon_Fire_Cooldown());

                
                break;
            case 1:
                Create_Enemies();
                break;
        }
    }
}
