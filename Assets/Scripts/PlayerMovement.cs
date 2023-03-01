using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;

    public float speed = 400;
    bool isFacingRight = true;

    public float jumpForce = 5;

    public Rigidbody2D playerRB;

    public Animator animator;

    private void Awake()
    {
       controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += context =>
        {
            direction = context.ReadValue<float>();
        };

        controls.Land.Jump.performed += context => Jump();
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0 )
            Flip();
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }
}
