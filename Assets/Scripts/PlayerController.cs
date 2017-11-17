using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float maxHorizontalSpeed;
    public float jump;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 1.25f;

    private Rigidbody2D rb;
    private float distToGround;
    //private CharacterController controller;

    public Collider2D[] attackHitboxes;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //controller = GetComponent<CharacterController>();
        distToGround = 0.375f;
    }

    bool IsGrounded(){
        return Physics2D.Raycast((Vector2)transform.position, -Vector2.up, distToGround + 0.1f);
    }

    void Update()
    {
        // input keys
        if (Input.GetKeyDown("space") && IsGrounded()){
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        if ((rb.velocity.x > -1*maxHorizontalSpeed) && Input.GetKey("left")){
            rb.AddForce(new Vector2(-speed, 0));
        }
        if ((rb.velocity.x < maxHorizontalSpeed) && Input.GetKey("right"))
        {
            rb.AddForce(new Vector2(speed, 0));
        }
        // Implementing a better feel on jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            Debug.Log("fuck");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        }
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        rb.AddForce(movement * speed);
        */
    }

    private void LaunchAttack(Collider2D col)
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.extents, col.transform.rotation.z);
        foreach(Collider2D c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

}
// Destroy(other.gameObject);