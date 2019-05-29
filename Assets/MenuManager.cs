using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator menuAnimator;
    private bool menuState = false;

    public void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            menuAnimator.SetBool("Open",menuState);
            menuState = !menuState;
        }
    }
}
