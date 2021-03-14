using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Collider2D collider;
    public Collider2D collider2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.layer);
        if (collision.gameObject.layer != 10)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), collider);
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), collider2);


        }
    }
}
