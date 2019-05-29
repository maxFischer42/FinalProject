using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    private PlayerController controller;
    public LayerMask layerMask;
    public float distance = 0.01f;

    public void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }
    public void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask))
        {
            controller.SendMessage("GroundCheck", true);
        }
        else
        {
            controller.SendMessage("GroundCheck", false);
        }
    }

}
