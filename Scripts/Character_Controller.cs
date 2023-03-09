using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : Entity_Class
{
    [Header("Player Setting")]
    public float Speed = 3f;
    private Rigidbody2D Ridge2d;
    private Animator Player_Animator;
    private SpriteRenderer Player_Sprite;
    private Vector2 Movement_Vec;
    //public GameObject Shot_Dir;
    public GameObject Camera_Object;
    public GameObject[] Heart_Objects;
    public float fire_cooldown;
    [Header("Other Settings")]
    public GameObject GameOverScreen;
    bool can_shoot = true;

    void Awake()
    {
        Player_Animator = gameObject.GetComponentInChildren<Animator>();
        Player_Sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        Ridge2d = GetComponent<Rigidbody2D>();
    }

    public override void Game_Over_Screen()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public override void Hearts(){
        float Heart_Find_Num = ((int)Health / 2);
        Heart_Find_Num = Mathf.Ceil(Heart_Find_Num);
        Debug.Log(Heart_Find_Num);
        GameObject Current_Heart = Heart_Objects[(int)Heart_Find_Num];// will change later to support increase of hearts
        GameObject HeartHalf = Current_Heart.transform.Find("1").gameObject;
        GameObject HeartFull = Current_Heart.transform.Find("2").gameObject;
        if ((int)Health % 2 == 0){
            Debug.Log("Even");
            HeartFull.SetActive(false);
            HeartHalf.SetActive(false);
        }else{
            HeartFull.SetActive(false);
            HeartHalf.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movement_Vec.x = Input.GetAxisRaw("Horizontal");
        Movement_Vec.y = Input.GetAxisRaw("Vertical");
        if ((Input.GetMouseButtonDown(0)) && can_shoot == true){
            Shot();
            can_shoot = false; StartCoroutine(cooldown());
        }
        if ((Movement_Vec.x != 0) || (Movement_Vec.y != 0)){
            Player_Animator.SetBool("Movement",true);
        }else{
            Player_Animator.SetBool("Movement",false);
        }
    } 

    void FixedUpdate() {
        Ridge2d.MovePosition(Ridge2d.position + Movement_Vec * Speed * Time.fixedDeltaTime);
        switch((int)Movement_Vec.x){
            case 1:
                Player_Sprite.flipX = false;
                break;
            case -1:
                Player_Sprite.flipX = true;
                break;
            default:
                break;
        }
        //Shot();
    }

    void Shot(){
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Shot_Dir.transform.LookAt(new Vector3(point.x, 0, point.z));

        var Shot_Dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Shot_Dir.z = 0;       
        Shot_Dir.Normalize();

        Create_Projectile(Projectle_Speed, Shot_Dir , transform.position , Bullet_Tag_Orgin);
    }

    IEnumerator cooldown(){
        yield return new WaitForSeconds(fire_cooldown);
        can_shoot = true;
    }


}
