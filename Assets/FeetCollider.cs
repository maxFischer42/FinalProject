﻿using System.Collections;
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
        bool value;
        if (Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask) ||
            Physics2D.Raycast(new Vector3(transform.position.x + 0.1f, transform.position.y, 0f), Vector2.down, distance, layerMask) ||
            Physics2D.Raycast(new Vector3(transform.position.x - 0.1f, transform.position.y, 0f), Vector2.down, distance, layerMask))
        {
            value = true;
        }
        else
        {
            value = false;
        }
        controller.SendMessage("GroundCheck", value);
    }

}
