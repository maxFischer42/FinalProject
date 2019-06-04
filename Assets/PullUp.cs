using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUp : MonoBehaviour
{
    public Vector2 directionDisplace;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController data = collision.GetComponent<PlayerController>();
        if(data)
        {
            data.pullUpDisplacement = directionDisplace;
            data.currentState = PlayerController.State.Pullup;
            //set pulling up animation once have it//
        }
    }
}
