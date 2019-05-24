using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
    public float speedMultiplier;
    public float xMaxDistanceDelta = 0.1f;

    private SpriteRenderer rend;
    private Animator anim;

    private PlayerAnimationController animator;
    //note: 0 = Idle, 1 = Walk, 2 = Roll, 3 = Dash, 4 = Attack1


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        animator = GetComponent<PlayerAnimationController>();
    }



    void Update()
    {
        GetInputs inputs = new GetInputs();
        if (inputs.hInput != 0)
        {
            //check if rolling
            if(inputs.yInput < 0)
            {

            }
            else
            {
                xMove(inputs.hInput);
                animator.ChangeAnimation(1);
            }
        }
        else if(inputs.hInput == 0)
        {
            animator.ChangeAnimation(0);
        }

    }




    public void xMove(float x)
    {
        if(x > 0)
        {
            rend.flipX = false;
        }
        else
        {
            rend.flipX = true;
        }
        Vector3 velocity = new Vector2(x * speedMultiplier,0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + velocity, xMaxDistanceDelta);
    }



}

public class GetInputs
{
    public float hInput;
    public float yInput;
    public float aInput;
    public float bInput;
    public float cInput;

    public GetInputs()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        aInput = Input.GetAxisRaw("Action1");
        bInput = Input.GetAxisRaw("Action2");
        cInput = Input.GetAxisRaw("Action3");
    }
}