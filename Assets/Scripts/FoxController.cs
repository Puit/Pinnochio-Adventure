using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour {
    public Transform target;
    private float elapse = 0f;
    public Rigidbody2D myRigidbody;
    private Vector2 start;
    private Vector2 end;
    public float speed;
    private bool isLeft= false;
    private Vector3 scale;
    Animator animator;
    bool destructor = false;
    public float timeDestroy = 1.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        start = transform.position;
        end = target.position;
        scale = transform.localScale;
    }
    void Rotate(float speed)
    {
        if (transform.position.x - target.position.x > 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            isLeft = true;
        }
        else if (transform.position.x - target.position.x < 0)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            isLeft = false;

        }
    }
    public void Kill()
    {
        animator.SetTrigger("Kill");
        GetComponentInParent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitForDestroy(this));
    }
    IEnumerator WaitForDestroy(FoxController e)
    {
        yield return new WaitForSeconds(e.timeDestroy);
        e.destructor = true;
    }
    // Update is called once per frame
    void Update () {
        if (target != null)
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            start = new Vector2(start.x, transform.position.y);
            end = new Vector2(end.x, transform.position.y);
            target.position = new Vector2(target.position.x, transform.position.y);
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, fixedSpeed);
            Rotate(fixedSpeed);
            if (target.position.x == transform.position.x)
            {
                target.position = (target.position.Equals(end)) ? start : end;
            }
        }
        if (destructor)
            Destroy(gameObject);
    }
}
