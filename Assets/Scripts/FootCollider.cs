using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{

    public CharacterController2D controller;
    public Rigidbody2D rb;
    //public Animator myAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(velocity);
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //myAnimator.SetBool("Break", true);
            if (collision.GetComponent<EnemyController>() != null)
                collision.GetComponent<EnemyController>().Kill();
            if (collision.GetComponent<FoxController>() != null)
                collision.GetComponent<FoxController>().Kill();

            rb.velocity = new Vector3(0f, 0f, 0f);
            controller.Move(0, false, true);
        }
    }
}