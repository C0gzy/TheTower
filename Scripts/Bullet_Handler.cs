using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Handler : MonoBehaviour
{
    public Vector3 Direction;
    public float speed;
    public float Max_life = 4f;
    public string Bullet_Tag_Orgin;
    float TimeStamp;
    void Awake(){
        TimeStamp = Time.time + Max_life;
    }
    void Update(){
        if (TimeStamp <= Time.time){
            Destroy(gameObject);
        }  
    }
    void FixedUpdate(){
        transform.Translate(Direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.tag != Bullet_Tag_Orgin){
            Destroy(gameObject);
            if (col.tag != "Untagged"){
                col.gameObject.GetComponent<Entity_Class>().Take_Damage(1);
            }
        }        
    }
}
