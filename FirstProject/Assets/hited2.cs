using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hited2 : MonoBehaviour
{   
    bool isShaking = false;
    [SerializeField] float shakeAmout = .2f;
    Vector2 startPos;
    [SerializeField] int hp = 150;
    // Start is called before the first frame update
    [SerializeField] string vulnerableCollisionGameObjecTag ;

    public HealthBar healthBar;
    void Start()
    {   
        healthBar.SetMaxHealth(hp);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            hp--;
            healthBar.SetHealth(hp);
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmout;
        }
        if (hp <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   print(collision.gameObject.tag);
        if(collision.gameObject.tag == vulnerableCollisionGameObjecTag)
        {   
            isShaking = true;
            startPos = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        print(collision.gameObject.tag);
        /*if (collision.gameObject.tag == vulnerableCollisionGameObjecTag)
        {
            isShaking = false;
            transform.position = startPos;
        }*/
    }

}
