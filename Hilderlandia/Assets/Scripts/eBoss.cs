using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBoss : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public int life;
    public float speed; 
    public int points;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;
    public LayerMask layer;

    public GameObject gameFinish;

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
    float actualSpeed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if ( height>0 && !playerDestroyed )
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
                rig.bodyType = RigidbodyType2D.Kinematic;

                life--;
                if ( life > 2 )
                {
                    actualSpeed = speed;
                    AudioManager.instance.enemydieFX.Play();
                    StartCoroutine(reactive(true, "hit", 0f, 0f)); //tomando dano;
                    StartCoroutine(reactive(false, "hit", actualSpeed, 1.5f)); //voltando a ação;
                }
                else if ( life == 2 )
                {
                    speed *= 2f;
                    actualSpeed = speed;
                    AudioManager.instance.enemydieFX.Play();
                    StartCoroutine(reactive(true, "hit", 0f, 0f));
                    StartCoroutine(reactive(false, "hit", actualSpeed, 1.5f));
                }
                else if ( life == 1 )
                {
                    speed += (speed > 0) ? 1 : -1;
                    actualSpeed = speed;
                    AudioManager.instance.enemydieFX.Play();
                    StartCoroutine(reactive(true, "hit", 0f, 0f));
                    StartCoroutine(reactive(false, "rage", actualSpeed, 1.5f)) ;
                }
                else if ( life <= 0 )
                {
                    StartCoroutine(reactive(true, "hit", 0f, 0f));
                    AudioManager.instance.bossdieFX.Play();

                    GameController.instance.score += points;
                    GameController.instance.UpdateScore();

                    StartCoroutine(Anim());
                    StartCoroutine(Finish());
                    Destroy(gameObject, 2.5f);
                }
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

    IEnumerator reactive(bool sit, string name, float velocity, float timer)
    {
        yield return new WaitForSeconds(timer);

        if (name != "rage")
            anim.SetBool(name, (life<=0) ? !sit : sit );
        else
            anim.SetBool(name, !sit);
        
        speed = velocity;
        box2d.enabled = !sit;
        circle2d.enabled = !sit;
   }


    IEnumerator Anim()
    {
        yield return new WaitForSeconds(1.3f);
        GetComponent<SpriteRenderer>().enabled = false;
    }

   IEnumerator Finish()
   {
       yield return new WaitForSeconds(2.2f);
       GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Pause();
       AudioManager.instance.win.Play();
       gameFinish.SetActive(true);
       Time.timeScale = 0;
   }

}
