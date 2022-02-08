using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBoss : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D rb;

    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isFlipped = true;
        }
        else if(rb.position.x < player.position.x && isFlipped)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isFlipped = false;
        }

       
    }
}
