using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDropdown : MonoBehaviour
{
    private Animator animator;
    public Vector3 closePosition;
    public Vector3 openPosition;
    public RuntimeAnimatorController m_animation;
    private bool state = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StopOpen()
    {
        //animator.runtimeAnimatorController = null;
        GetComponent<RectTransform>().anchoredPosition = openPosition;
        animator.SetBool("Open", false);
    }

    public void StopClose()
    {
        //animator.runtimeAnimatorController = null;
        GetComponent<RectTransform>().anchoredPosition = closePosition;
        animator.SetBool("False", false);
    }

}
