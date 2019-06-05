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
            if ((data.currentState != PlayerController.State.Slide ||
                data.currentState != PlayerController.State.Roll ||
                data.currentState != PlayerController.State.Attack1) && !data.slideCollider.enabled)
            {
                data.rigidBody.velocity = Vector2.zero;
                data.pullUpDisplacement = directionDisplace;
                data.currentState = PlayerController.State.Pullup;
                data.PullUp();
            }
        }
    }
}
