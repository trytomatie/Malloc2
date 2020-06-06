using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplaterEffect : MonoBehaviour
{
    public Vector2 velocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x < 0.1f && rb.velocity.y < 0.1f)
        {
            rb.velocity = new Vector2(0.3f,-1.8f);
            Material m = GetComponent<SpriteRenderer>().material;
            Destroy(gameObject, 1.5f);
            this.enabled = false;
        }

    }
}
