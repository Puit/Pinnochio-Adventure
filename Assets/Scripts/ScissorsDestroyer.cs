using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsDestroyer : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Squirrel"))
        {
            Destroy(gameObject);
        }
    }
}
