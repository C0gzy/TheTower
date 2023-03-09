using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class Entity_Class : MonoBehaviour
{
    [Header("Entity Character Settings")]
    public float Health;

    [Header("Entity Settings")]
    public GameObject Bullet;
    public float Projectle_Speed; 
    public string Bullet_Tag_Orgin;   
    public virtual void Create_Projectile(float Speed , Vector3 Direction , Vector3 location , string Tag){
        GameObject Active_Bullet = Instantiate(Bullet , location , Quaternion.identity);
        Active_Bullet.GetComponent<Bullet_Handler>().Direction = Direction;
        Active_Bullet.GetComponent<Bullet_Handler>().speed = Speed;
        Active_Bullet.GetComponent<Bullet_Handler>().Bullet_Tag_Orgin = Tag;

        //GameObject Active_Bullet = PrefabUtility.InstantiatePrefab(Bullet) as GameObject;
        //var Active_Bullet = Selection.activeGameObject; //PrefabUtility.InstantiatePrefab(Bullet, transform);
        //Active_Bullet.transform.position = location;
        
    }


    public virtual void Take_Damage(float Dmg){
        Health -= Dmg;
        if (Health <= 0){
            if (gameObject.tag == "Player"){
                Game_Over_Screen();
            }else{
                Destroy(gameObject);
            }
        }else{
            
            if (gameObject.tag == "Player"){
                Hearts();
                gameObject.GetComponentInChildren<Animator>().Play("TakeDamage");
            }else{
                gameObject.GetComponent<Animator>().Play("TakeDamage");
            }
        }
    }

    public abstract void Hearts();
    public abstract void Game_Over_Screen();
}
