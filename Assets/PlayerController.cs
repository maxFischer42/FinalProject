﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
    public bool isCooldown = false;
    public bool isUnder = false;



    private Vector2 uptiltcolloffset;
    private Vector2 uptiltthrustcolloffset;

    private GameObject currentHitbox;

    public enum State {Idle, Walk, Run, Roll, Slide, Attack1, IdleJump, LongJump, Falling, Landing, Climbing, Pullup};
    public State currentState = State.Idle;

    [Header("Components")]
    private SpriteRenderer rend;
    private Animator anim;
    private PlayerAnimationController animator;
    //note: 0 = Idle, 1 = Run, 2 = Roll, 3 = Dash, 4 = Attack1, 5 = Walk, 6 = Slide
    public Rigidbody2D rigidBody;



    [Header("Colliders")]
    public CapsuleCollider2D mainCollider;
    public CircleCollider2D rollCollider;
    public BoxCollider2D slideCollider;

    [Header("AttackDetails")]
    public GameObject upwardTilt;
    public Vector2 flip1Offset = Vector2.zero;
    public GameObject upwardTiltThrust;
    public Vector2 flip1Offset2 = Vector2.zero;

    [Header("Stamina")]
    public float currentStamina = 100;
    public float timeToGetStamina = 0.2f;
    public float timeToRegainStamina = 2.5f;
    public float rollStamina = 25;
    public float runStamina = 2;
    public float slideStamina = 20;
    public float jumpStamina = 30;
    public float staminaGain = 0.1f;
    public Color staminaGoodColor;
    public Color staminaBadColor;

    [Header("Multiplier Variables")]
    public float speedMultiplier;
    public float xMaxDistanceDelta = 0.1f;
    public float runMultiplier = 1.5f;
    public float rollMultiplier = 1.8f;
    public float slideMultiplier = 2.3f;

    [Header("UI Objects")]
    public Image StaminaSlider;
    public GameObject StaminaObject;
    public float secondTillWheelVanish = 1f;

    [Header("Other Variables")]
    public bool isGrounded = false;
    public Vector2 idleJumpForce = new Vector2(0, 1f);
    public Vector2 longJumpForce = new Vector2(0, 0.7f);
    public GameObject jumpEffect;
    public GameObject longJumpEffect;
    public float gravityScale = 1f;

    [HideInInspector]
    public Vector2 pullUpDisplacement;

    bool normalJump = false;

    private bool noStamina = false;

    private Climb climbScript;

    private float staminaTimer;

    private float memoryX;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        animator = GetComponent<PlayerAnimationController>();
        climbScript = GetComponent<Climb>();
    }

    void Update()
    {
        CheckStamina();
        if (slideCollider.enabled && currentState != State.Slide)
        {
            slideCollider.enabled = false;
            mainCollider.enabled = true;
        }
        if((currentState == State.Falling && rigidBody.velocity.magnitude < 1) || (currentState == State.LongJump && isGrounded && rigidBody.velocity.magnitude < 1))
            currentState = State.Idle;
        if (currentState == State.Climbing)
        {
            animator.ChangeAnimation(13);
            return;
        }
        if (currentState == State.Pullup)
            return;
        if (!isGrounded && rigidBody.velocity.y < 0)
        {
            if(currentState == State.IdleJump)
            {
                animator.ChangeAnimation(11);
            }
            else
            {
                animator.ChangeAnimation(8);
            }
            currentState = State.Falling;
        }
        if(isCooldown && currentState == State.Idle)
        {
            isCooldown = !isCooldown;
        }
        if(isGrounded && currentState == State.Falling)
        {
            if(!normalJump)
            {
                animator.ChangeAnimation(9);
            }
            else
            {
                animator.ChangeAnimation(11);
            }
        }
        if (animator.animations[0].animation == GetComponent<Animator>().runtimeAnimatorController && currentState != State.Idle)
        {
            currentState = State.Idle;
            mainCollider.enabled = true;
            slideCollider.enabled = false;
            rollCollider.enabled = false;
        }
        if (currentState == State.IdleJump || currentState == State.LongJump || currentState == State.Falling)
            return;
        GetInputs inputs = new GetInputs();
        
        if (isCooldown)
            return;
        if (isGrounded)
        {
            if (inputs.aInput > 0 && !noStamina)
            {
                Debug.Log("foo");
                currentState = State.Attack1;

                animator.ChangeAnimation(4);
                return;
            }
            if (inputs.bInput > 0 && !noStamina)
            {
                Debug.Log("jump?");
                Jump(inputs.hInput);
                return;
            }
            if (inputs.hInput != 0)
            {
                memoryX = inputs.hInput;
                //check if rolling or sliding
                if (inputs.yInput != 0)
                {
                 if (inputs.yInput < 0 && !noStamina)
                 {
                    Slide(inputs.hInput);
                 }
                 else if(inputs.yInput > 0 && !noStamina)
                 {
                    Roll(inputs.hInput);
                 }
            }
            else
            {
                xMove(inputs.hInput,inputs.cInput);
            }
        }
        else
        {
            
            currentState = State.Idle;
            animator.ChangeAnimation(0);
        }
        }
    }

    public IEnumerator GetStamina()
    {
        yield return new WaitForSeconds(timeToGetStamina);
        currentStamina += staminaGain;
        if(currentStamina >= 100 && noStamina)
        {
            StaminaSlider.color = staminaGoodColor;
            noStamina = false;
        }
    }

    public void UseStamina(float amount)
    {
        staminaTimer = 0;
        currentStamina -= amount;
    }

    void CheckStamina()
    {
        if (currentStamina != 100 && noStamina)
        {
            StaminaSlider.color = staminaBadColor;
            if (currentState == State.Climbing)
            {
                currentState = State.Falling;
                climbScript.enabled = false;
                anim.enabled = true;
                rigidBody.gravityScale = gravityScale;
            }
        }
        if(currentStamina < 100)
        {
            StaminaObject.SetActive(true);
        }
        if (currentStamina > 100)
            currentStamina = 100;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            noStamina = true;

        }
        if(staminaTimer > timeToRegainStamina && currentStamina != 100 && currentState != State.Climbing && currentState != State.Falling)
        {
            StartCoroutine(GetStamina());
        }
        if(currentStamina != 100)
        {
            StaminaObject.SetActive(true);
        }
        staminaTimer += Time.deltaTime;
        StaminaSlider.fillAmount = (currentStamina / 100f);
    }

    public void GroundCheck(bool var)
    {
        isGrounded = var;
    }

    public void xMove(float x, float c)
    {
        float d = xMaxDistanceDelta;
        if(x > 0)
        {
            rend.flipX = false;
        }
        else
        {
            rend.flipX = true;
        }
        Vector3 velocity = new Vector2(x * speedMultiplier,0);
        if(c > 0 && !noStamina)
        {
            UseStamina(runStamina);
            animator.ChangeAnimation(5);
            d *= runMultiplier;
            currentState = State.Run;
        }
        else
        {
            animator.ChangeAnimation(1);
            currentState = State.Walk;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + velocity, d);
    }

    public void Roll(float x)
    {
        UseStamina(rollStamina);
        Vector2 velocity = new Vector2(x  * rollMultiplier,0f);
        animator.ChangeAnimation(2);
        rigidBody.velocity = velocity;
    }

    public void Slide(float x)
    {
        UseStamina(slideStamina);
        Vector2 velocity = new Vector2(x * slideMultiplier, 0f);
        animator.ChangeAnimation(6);
        rigidBody.velocity = velocity;
    }

    public void SlideEvent(int x)
    {
        switch (x)
        {
            case 0:
                slideCollider.enabled = true;
                mainCollider.enabled = false;
                isCooldown = true;
                currentState = State.Slide;
                break;
            case 1:
                if (isUnder)
                {
                    animator.ChangeAnimation(6);
                    Slide(memoryX);
                }
                else
                {
                    mainCollider.enabled = true;
                    slideCollider.enabled = false;
                    currentState = State.Idle;
                    isCooldown = false;
                    animator.ChangeAnimation(0);
                    rigidBody.velocity = rigidBody.velocity / slideMultiplier * 2;
                }
                break;
        }
    }

    public void RollEvent(int x)
    {
        switch(x)
        {
            case 0:
                rollCollider.enabled = true;
                mainCollider.enabled = false;
                isCooldown = true;
                currentState = State.Roll;
                break;
            case 1:
                mainCollider.enabled = true;
                rollCollider.enabled = false;
                currentState = State.Idle;
                isCooldown = false;
                animator.ChangeAnimation(0);
                rigidBody.velocity = rigidBody.velocity / rollMultiplier * 2;
                break;
        }
    }

    public void UpwardTiltEvent(int x)
    {
        switch(x)
        {
            case 0:
                isCooldown = true;
                currentHitbox = (GameObject)Instantiate(upwardTilt,transform);
                if (rend.flipX)
                {
                    currentHitbox.GetComponent<BoxCollider2D>().offset = flip1Offset;
                }
                break;
            case 1:
                Destroy(currentHitbox.gameObject);
                GetInputs inputs = new GetInputs();
                if (inputs.aInput == 0)
                {
                    isCooldown = false;
                    currentState = State.Idle;
                    animator.ChangeAnimation(0);
                }
                break;
            case 2:
                currentHitbox = (GameObject)Instantiate(upwardTiltThrust, transform);
                if (rend.flipX)
                {
                    currentHitbox.GetComponent<BoxCollider2D>().offset = flip1Offset2;
                }
                break;
            case 3:
                Destroy(currentHitbox.gameObject);
                isCooldown = false;
                currentState = State.Idle;
                animator.ChangeAnimation(0);
                break;
        }
    }

    public void Jump(float x)
    {
        climbScript.enabled = false;
        anim.enabled = true;
        rigidBody.gravityScale = gravityScale;
        UseStamina(jumpStamina);
        if (x != 0)
        {
            normalJump = false;
            GameObject effect = (GameObject)Instantiate(longJumpEffect, transform);
            if (x < 0)
                effect.GetComponent<SpriteRenderer>().flipX = true;
            effect.transform.parent = null;
            xMove(x, 0);
            rigidBody.AddForce(new Vector2(x * longJumpForce.x, longJumpForce.y));
            animator.ChangeAnimation(7);
            currentState = State.LongJump;
        }
        else
        {
            normalJump = true;
            GameObject effect = (GameObject)Instantiate(jumpEffect, transform);
            effect.transform.parent = null;
            rigidBody.AddForce(new Vector2(idleJumpForce.x,idleJumpForce.y));
            animator.ChangeAnimation(10);
            currentState = State.IdleJump;
        }
    }

    public void Land()
    {
        animator.ChangeAnimation(0);
        currentState = State.Idle;
    }

    public void HeadCheck(bool x)
    {
        isUnder = x;
    }

    public void WallCheck(bool x)
    {
        if (currentState == State.Falling || currentState == State.Pullup)
            return;
        if (x)
        {
            
            currentState = State.Climbing;
            climbScript.enabled = true;
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0f;

        }
        else if (!x)
        {
            currentState = State.Falling;
            climbScript.enabled = false;
            anim.enabled = true;
            rigidBody.gravityScale = gravityScale;
        }
    }

    public void PullUp()
    {
        currentState = State.Pullup;
        Debug.Log("Pulling Up!");
        animator.ChangeAnimation(14);
        climbScript.enabled = false;
        anim.enabled = true;
    }

    public void PullUpEvent(int x)
    {
        switch(x)
        {
            case 1:
                transform.position += new Vector3((pullUpDisplacement.x / 1.6f), pullUpDisplacement.y / 2);
                break;
            case 5:
                transform.position = transform.position + new Vector3(pullUpDisplacement.x,pullUpDisplacement.y);
                currentState = State.Idle;
                animator.ChangeAnimation(0);
                rigidBody.gravityScale = gravityScale;
                break;

        }
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