using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{

    public string level;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            GameController.instance.UpdateFinalScore();
            SceneManager.LoadScene( level );
        }
    }

}
