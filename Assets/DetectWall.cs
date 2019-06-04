using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            transform.parent.SendMessage("WallCheck", true);
            transform.parent.GetComponent<PlayerAnimationController>().ChangeAnimation(13);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            transform.parent.SendMessage("WallCheck", false);
    }
}
