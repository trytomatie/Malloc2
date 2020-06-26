using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseExplosionAnimation : MonoBehaviour {
    private float bounceValue = 0.3f;
    private int timer;
    private Rigidbody2D rb;
    private float horizontalVelocity;
    private int randomTimeExtender;

    private bool stopMovement = false;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        horizontalVelocity = Random.Range(-0.4f, 0.4f);
        bounceValue = Random.Range(0f, 1.4f);
        rb.AddTorque(Random.Range(0, 63f));
        randomTimeExtender = Random.Range(0, 70);
        rb.gravityScale = 0.25f;
        rb.velocity = new Vector2(horizontalVelocity, bounceValue);
        gameObject.tag = "Background";
    }
	
	// Update is called once per frame
	void Update () {

        if(timer == 600)
        {
            PublicGameResources.FadeSprite(GetComponent<SpriteRenderer>(), 5f, true);
        }
        if (timer == 90 + randomTimeExtender)
        {
            rb.gravityScale = 0;
            rb.drag = 10;
            rb.angularDrag = 10;
            stopMovement = true;
        }
        if (timer % 30 == 0 && !stopMovement)
        {
            rb.AddTorque(Random.Range(0, 123f));
            bounceValue *= 0.8f;
            rb.velocity = new Vector2(horizontalVelocity, bounceValue);
        }
        timer++;

    }
}
