using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFalling : MonoBehaviour {

    float elapse = 0;
    public float timeToGoBack = 2f;
    bool fall = false;
    Vector3 startPosition;

    public Rigidbody2D rb;
    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            fall = true;
            Invoke("DropPlatform", 0.5f);
        }
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
        rb.gravityScale = 1f;
        rb.mass = 0.5f;
    }
    private void Update()
    {
        if (fall)
        {
            elapse += Time.deltaTime;
            if (elapse >= timeToGoBack)
            {
                fall = false;
                rb.isKinematic = true;
                rb.velocity = new Vector3 (0f,0f,0f);
                transform.position = startPosition;
                elapse = 0;
            }
        }
    }
}
