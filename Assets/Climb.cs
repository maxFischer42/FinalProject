using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public float speedMultiplier = 1.0f;
    public float maxDistanceDelta = 0.1f;
    public float staminaUsage = 0.01f;

    private PlayerController con;
    private void Start()
    {
        con = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
    private Animator anim;
    
    void Update()
    {
            GetInputs input = new GetInputs();
            if(input.yInput != 0)
            {
                anim.enabled = true;
                Move(input.yInput);
            con.UseStamina(staminaUsage);
            }
            else
            {
                anim.enabled = false;
            }
            float a = 1;
            if (!GetComponent<SpriteRenderer>().flipX)
                a = -1;
            if (input.bInput > 0)
                GetComponent<PlayerController>().Jump(a);
    }

    void Move(float x)
    {
        Vector3 velocity = new Vector2(0, x * speedMultiplier);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + velocity, maxDistanceDelta);
    }
}
