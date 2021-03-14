using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{   
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
            Instantiate(player, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
