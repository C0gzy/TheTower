using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Enemies : Enemy_class
{
    [Header("Options")]
    public List<Transform> Shoot_positions;

    public override void Start()
    {
        base.Start();
        if (Shoot_positions.Count == 0){
            Shoot_positions.Add(Waypoint);
        }
    }
    //Attack_Move called from animation key frame

    public virtual void Projectile_Create_Shoot(){
        if ((transform.position - Waypoint.position).magnitude <= Attack_Distance){
            for (int i = 0; i < Shoot_positions.Count ; i++){

                var Shot_Dir = (Shoot_positions[i].position - transform.position);
                Shot_Dir.z = 0;
                Shot_Dir.Normalize();
                Create_Projectile(Projectle_Speed , Shot_Dir , transform.position , Bullet_Tag_Orgin);
            }
        }
    }

    public virtual void Attack_Move(){
        Projectile_Create_Shoot(); 
        StartCoroutine(Start_Cooldown());
          
    }
}