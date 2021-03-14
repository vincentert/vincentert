using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hited : MonoBehaviour
{   
    bool isShaking = false;
    [SerializeField] float shakeAmout = .2f;

    [SerializeField] GameObject gameObjectTaget;
    Vector2 startPos;
    [SerializeField] int hp = 150;
    // Start is called before the first frame update
    [SerializeField] string vulnerableCollisionGameObjecTag ;

    public HealthBar healthBar;
    void Start()
    {   
        healthBar.SetMaxHealth(hp);
        startPos = gameObjectTaget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            hp--;
            healthBar.SetHealth(hp);
            gameObjectTaget.transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmout;
        }
        if (hp <= 0)
        {
            //Destroy(gameObject);
            gameObjectTaget.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.tag == vulnerableCollisionGameObjecTag)
        {   
            isShaking = true;
            startPos = gameObjectTaget.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        
        if (collision.gameObject.tag == vulnerableCollisionGameObjecTag)
        {
            isShaking = false;
            gameObjectTaget.transform.position = startPos;
        }
    }

    public bool isAvailable(){
        return !isShaking;
    }

}
