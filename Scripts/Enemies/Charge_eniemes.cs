using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge_eniemes : Enemy_class
{
    //Attack_Move called from animation key frame
    public virtual void Attack_Move(){
        
        if ((transform.position - Waypoint.position).magnitude <= Attack_Distance){
            Vector2 Knockback_Direction = (Waypoint.position - transform.position);
            Waypoint.transform.position = new Vector2(Waypoint.transform.position.x + Knockback_Direction.x , Waypoint.transform.position.y + Knockback_Direction.y);
            Waypoint.GetComponent<Character_Controller>().Take_Damage(Damage_Deal);

        }

        StartCoroutine(Start_Cooldown());
    }
}
