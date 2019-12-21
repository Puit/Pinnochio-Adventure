using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    public PlayerMovement player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.attack && collision.gameObject.tag.Equals("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyController>() != null)
                collision.gameObject.GetComponent<EnemyController>().Kill();
            if (collision.gameObject.GetComponent<FoxController>() != null)
                collision.gameObject.GetComponent<FoxController>().Kill();
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Actionable"))
        {
            collision.gameObject.GetComponent<ActionableCranck>().Action();
        }
    }
}
