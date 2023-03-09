using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding_En : Enemy_class
{
    //Attack_Move called from animation key frame
    [Header("Options")]
    public ParticleSystem Particle_Explosion;
    public Color FLashing_Colour = Color.red;
    public float explosion_radius = 3f;

    public virtual void Attack_Move(){
        //explosion effect
        gameObject.GetComponent<SpriteRenderer>().color = FLashing_Colour;
        
        StartCoroutine(Expolsion());

    }

    private void Explode(){
        Particle_Explosion.Play();
        if ((transform.position - Waypoint.position).magnitude <= explosion_radius){
            Waypoint.GetComponent<Character_Controller>().Take_Damage(Damage_Deal);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    public virtual IEnumerator Expolsion(){

        yield return new WaitForSeconds(.5f);
        Explode();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);

    }
}
