using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeHelper : MonoBehaviour
{
    public Vector2 addForceMultiplier = new Vector2(0, 1f);
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetInputs inputs = new GetInputs();
            if(inputs.hInput != 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity + addForceMultiplier;
                Debug.Log("Push");
            }
        }
    }

}
