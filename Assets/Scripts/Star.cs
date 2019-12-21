using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public Animator animator;
    public Scene2Manager sceneManager;
    private float elapse = 0f;
    private float timeToDestroy = 0.5f;
    private bool destroying = false;
    public bool countHelper = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!destroying)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                animator.SetTrigger("Caught");
                sceneManager.stars++;
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
        if (countHelper)
        {
            sceneManager.stars++;
            countHelper = false;
        }
    }
}
