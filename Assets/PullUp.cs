using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUp : MonoBehaviour
{
    public Vector2 directionDisplace;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "WallDetector")
        {
            Debug.Log("Pull up");
            PlayerController data = collision.transform.parent.GetComponent<PlayerController>();
            data.pullUpDisplacement = directionDisplace;
            data.currentState = PlayerController.State.Pullup;
            data.PullUp();
        }
    }
}
