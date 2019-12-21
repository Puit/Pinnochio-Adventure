using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    public Animator animator;
    public Scene2Manager sceneManager;
    private float elapse = 0f;
    private float timeToDestroy = 0.5f;
    private bool destroying = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!destroying)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                animator.SetTrigger("Caught");
                if(sceneManager.life < 5)
                    sceneManager.life = sceneManager.life + 2;
                else
                {
                    if (sceneManager.life < 6)
                        sceneManager.life = sceneManager.life + 1;
                }
                destroying = true;
            }
        }
    }
    private void Update()
    {
        if (destroying)
        {
            elapse += Time.deltaTime;
            if (elapse >= timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
