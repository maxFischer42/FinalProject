using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDropdown : MonoBehaviour
{
    private Animator animator;
    private bool state = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        animator.SetBool("Open", state);
        state = !state;
    }

}
