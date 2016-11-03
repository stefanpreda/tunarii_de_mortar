using UnityEngine;
using UnityEngine.Networking;

public class Pac_Controller : MonoBehaviour
{

    public float maxSpeed = 11;
    bool right = true;
    bool up = true;
    Animator a;
    // Use this for initialization
    void Start()
    {
        a = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // deplasare axa X
        float move = Input.GetAxis("Horizontal");
        a.SetFloat("speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        
        // deplasare axaY
        float moveV = Input.GetAxis("Vertical");
        a.SetFloat("vspeed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveV * maxSpeed);

        // orientarea fetei player-ului... merge cu alt fsm mai simplu
        if (move > 0 && !right)
            Flip();
        else if (move < 0 && right)
            Flip();

        //TODO : continuare

    }

    // face flip la orientarea caracterului stanga <-> dreapta
    void Flip()
    {
        right = !right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
