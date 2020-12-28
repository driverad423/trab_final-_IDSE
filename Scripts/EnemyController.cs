using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.1f;
            if (transform.position.y + yOffset < col.transform.position.y)
            {
                col.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                col.SendMessage("EnemyKnockBack", transform.position.x);
            }
            
        }
    }
}
