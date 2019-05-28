using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 10;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerHitbox")
        {
            PlayerDamageSCript data = collision.GetComponent<PlayerDamageHolder>().data;
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            direction.Normalize();
            rigidbody.velocity = (-direction * data.knockbackScalar);
            health -= Mathf.RoundToInt(Random.Range(data.damageRange.x, data.damageRange.y));
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
