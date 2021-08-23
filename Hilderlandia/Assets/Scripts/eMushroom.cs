using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMushroom : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed; 
    public int points;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;
    public LayerMask layer;

    private bool colliding;
    private BoxCollider2D box2d;
    private CircleCollider2D circle2d; 

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box2d = GetComponent<BoxCollider2D>();
        circle2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
            speed = -speed;
        }
    }

    bool playerDestroyed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if ( height>0 && !playerDestroyed )
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
                anim.SetBool("die", true);
                speed = 0;
                box2d.enabled = false;
                circle2d.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                
                AudioManager.instance.enemydieFX.Play();
                GameController.instance.score += points;
                GameController.instance.UpdateScore();
                Destroy(gameObject, 0.3f);
            }
            else
            {
                playerDestroyed = true;
                AudioManager.instance.dieFX.Play();
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }

}
