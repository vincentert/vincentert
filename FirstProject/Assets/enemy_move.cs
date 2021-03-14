using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour {

    
    private GameObject player;

    [SerializeField]
    public float agroRange;

    [SerializeField]
    public float attackRange;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float speedFalling;

    [Range(0, .3f)] [SerializeField] 
    private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;
    Rigidbody2D rb2D;

    public hited hited;

    bool moovingRight = false;

    bool moovingLeft = false;

    int direction = 1;

    Vector3 targetVelocity ;

    public Animator animator;

    bool attacking = false;

    bool m_facingRight = true;


    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(attacking){
            stopChasingPlayer();
        }else{
            float distanceFromPlayer = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if(distanceFromPlayer<attackRange){
                stopChasingPlayer();
                attackPlayer();
            }
            else if(distanceFromPlayer<agroRange){
                chasePlayer();        
            }else{
                stopChasingPlayer();
                moovingLeft = false;
                moovingRight = false;
            } 
        }       
    }

    void chasePlayer(){    
        if(player.transform.position.x>gameObject.transform.position.x){
            direction = 1;
            if(!m_facingRight){
                Flip();
            }
            m_facingRight = true;
        }else if(player.transform.position.x<gameObject.transform.position.x){
            direction = -1;
            if(m_facingRight){
                Flip();
            }
            m_facingRight = false;
        }
        targetVelocity = new Vector2(speed*direction, rb2D.velocity.y-speedFalling);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    void stopChasingPlayer(){    
        targetVelocity = new Vector2(0, rb2D.velocity.y-speedFalling);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    void attackPlayer(){

        if(hited.isAvailable()){

            animator.SetBool("Attacking", true);


            StartCoroutine(resetAttack(.2f));
            attacking = true;
        }
    }

    IEnumerator resetAttack(float timer)
    {
        yield return new WaitForSeconds(timer);
        animator.SetBool("Attacking", false);
        attacking = false;
    }

    	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
