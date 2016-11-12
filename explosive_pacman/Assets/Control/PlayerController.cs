using UnityEngine;
using UnityEngine.Networking;

//This class controls player movement and animations
//Attach this to Character only
public class PlayerController : NetworkBehaviour
{
    private Animator anim;
    private Rigidbody2D body;

    public float speed = 5.0f;

    [SyncVar]
    public Color player_color = Color.white;

    [SyncVar]
    bool blockPlayers = true;

    //Initialization function
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        if (isLocalPlayer)
        {
            gameObject.GetComponent<PlayerRandomSpawner>().CmdRespawn();
        }

        gameObject.GetComponent<SpriteRenderer>().material.color = player_color;
    }

    void Update()
    {
        //Not the current player or invalid animation
        if (!isLocalPlayer || anim == null)
        {
            return;
        }

        if (blockPlayers)
            return;

        //When moving animation is set accordingly
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            anim.SetBool("left", true);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            body.velocity = new Vector2(-speed, 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", true);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            body.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", true);
            anim.SetBool("down", false);
            body.velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", true);
            body.velocity = new Vector2(0, -speed);
        }
    }

    public void setBlock(bool b)
    {
       blockPlayers = b;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

}
