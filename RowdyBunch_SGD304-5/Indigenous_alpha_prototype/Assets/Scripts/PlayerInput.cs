using UnityEngine;
using System.Collections;


public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float thrust;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
 
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb2d.velocity = movement * speed;

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jump = new Vector2(0.0f, thrust);
            rb2d.velocity = jump * speed;
        }
    }
}