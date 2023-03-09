using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy_class : Entity_Class
{

    [Header("Movement Settings")]
    public Transform Waypoint;
    public float Speed = 1f;
    public float Distance_To_Keep = 3f;
    bool Moving_To_Point = false;
    public Transform New_Bounce_Point;
    [Header("Attacks Settings")]
    public float Damage_Deal = 1f;
    public float Attack_Distance = .1f;
    bool Can_Attack = true;
    public float Attack_CoolDown = 2f;
    [Header("Object Specific Settings")]
    public Animator Object_Animation;
    private float Area = 1.0f;
    Vector3 WaypointNew; 
    // Start is called before the first frame update
    public virtual void Start()
    {
        Object_Animation = GetComponent<Animator>();
        WaypointNew = transform.position;
        if (Waypoint == null){
            Waypoint = GameObject.FindWithTag("Player").transform;
        }
        //float Save_Attack = Attack_CoolDown;
    }
    public override void Game_Over_Screen()
    {
        return;
    }

    public override void Hearts()
    {
        return;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void LateUpdate() {
        Vector2 Sprite_Flip = (Waypoint.position - transform.position).normalized;
        if (Vector2.Dot(Sprite_Flip, transform.right) < 0){
            GetComponent<SpriteRenderer>().flipX = true;
        }else{
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    /* Didn't work
    bool Can_See(){
        // Calculate the direction of the raycast
        Vector2 raycastDirection = (WaypointNew - transform.position).normalized;       
        Vector2 tileSize = new Vector2(1, 1);
        // Calculate the offset for the raycast point based on the direction of the raycast
        Vector2 offset = (tileSize / 2) + (raycastDirection * (tileSize / 2));
        
        Vector3 New_Offset = new Vector3(offset.x, offset.y, 0);
        RaycastHit2D RayCast_wall_Dectection = Physics2D.Raycast(transform.position + New_Offset, (WaypointNew - transform.position));
        Debug.DrawRay(transform.position ,  (WaypointNew - transform.position) , Color.blue);
        Debug.Log(RayCast_wall_Dectection.collider);
        if (RayCast_wall_Dectection.collider != null && RayCast_wall_Dectection.collider.gameObject.name == "Tilemap"){
            Tilemap gridmap = RayCast_wall_Dectection.collider.gameObject.GetComponent<Tilemap>();

            Vector3 DirectPoint = gridmap.transform.InverseTransformPoint(RayCast_wall_Dectection.point);
            //Vector3Int tilePos = new Vector3Int((int)RayCast_wall_Dectection.point.x , (int)RayCast_wall_Dectection.point.y , 0);
            
            Vector3Int tilePos = gridmap.WorldToCell(DirectPoint);
            TileBase tile_Hit = gridmap.GetTile(tilePos);
            Debug.Log("directpoint - "+ DirectPoint);
            Debug.Log("Tile Hit: "+tile_Hit);
            Debug.Log(gridmap.GetColliderType(tilePos));
            if (tile_Hit != null && tile_Hit.name != ""){
                return false;
            }

        }
        return true;
        
    }
    */
    protected virtual void Move(){
        RaycastHit2D RayCast_Player_Dectection = Physics2D.Raycast(transform.position , (Waypoint.position - transform.position));
        Debug.DrawRay(transform.position ,  (Waypoint.position - transform.position) , Color.red);
        
        /*Check if enemy has a clear line of sight with player and the enemy is not currently doing an attack animation
            Set the enemy animation state to moving
            and move the enemy towards the player at a constant speed
        */
        if ((RayCast_Player_Dectection.collider.tag == "Player") && (Object_Animation.GetBool("Attacking") == false)){

            Object_Animation.SetBool("Is_Moving" , true);
            transform.position = Vector2.MoveTowards(transform.position, Waypoint.position , Speed * Time.deltaTime);

            //Debug.Log((transform.position - Waypoint.position).magnitude <= Attack_Distance);
            if (((transform.position - Waypoint.position).magnitude <= Attack_Distance) && (Can_Attack == true)){
                Object_Animation.SetBool("Is_Moving" , false);
                Object_Animation.SetBool("Attacking" , true);
                Can_Attack = false;
            }
            Moving_To_Point = false;
        }else if (Object_Animation.GetBool("Attacking") == false){
            if (Moving_To_Point == true){
                transform.position = Vector2.MoveTowards(transform.position, WaypointNew , Speed * Time.deltaTime);
                if (((transform.position - WaypointNew).magnitude < Area )){
                    Moving_To_Point = false;
                }
                Area += .5f * Time.deltaTime;
            }else{
                Area = .5f;
                New_Bounce_Point.localPosition = Random.insideUnitCircle * 2f;
                WaypointNew = New_Bounce_Point.position;
                Moving_To_Point = true;
            }
        }
        /*
        GameObject[] All_Enemies = GameObject.FindGameObjectsWithTag("Hostiles");
        if ((transform.position - Waypoint.position).magnitude > Distance_To_Keep){

            foreach (GameObject Hostile in All_Enemies){
                if ((transform.position - Hostile.transform.position).magnitude > 2f){
                    New_Bounce_Point.localPosition = Random.insideUnitCircle * 2f;
                    transform.position = Vector2.MoveTowards(transform.position, New_Bounce_Point.position , (Speed * 2 )* Time.fixedDeltaTime);
                }
            }



            transform.position = Vector2.MoveTowards(transform.position, Waypoint.position , Speed * Time.fixedDeltaTime);
        }
        */
    }

    public virtual IEnumerator Start_Cooldown(){
        yield return new WaitForSeconds(.5f);
        Object_Animation.SetBool("Attacking" , false);
        yield return new WaitForSeconds(Attack_CoolDown);
        Can_Attack = true;
    }
}
