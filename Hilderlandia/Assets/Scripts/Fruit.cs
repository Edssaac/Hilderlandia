using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer srender;
    private CircleCollider2D coll2d;

    public GameObject collected;
    public int point;


    // Start is called before the first frame update
    void Start()
    {
        srender = GetComponent<SpriteRenderer>();
        coll2d = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            srender.enabled = false;
            coll2d.enabled = false;
            collected.SetActive(true);

            AudioManager.instance.collectedFX.Play();
            GameController.instance.score += point;
            GameController.instance.UpdateScore();

            Destroy(gameObject, 0.25f);
        }
    }

}
