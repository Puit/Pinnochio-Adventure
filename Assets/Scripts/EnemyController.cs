using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Animator animator;
    bool destructor = false;
    public float timeDestroy = 1.5f;
    public int damage = 1;
    public AIPath aiPath;
    public CircleCollider2D coliderInParent;
    private Vector3 localScale;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        localScale = transform.localScale;
	}
	public void Kill()
    {
        animator.SetTrigger("Kill");
        GetComponentInParent<CircleCollider2D>().enabled = false;
        aiPath.enabled = false;
        coliderInParent.enabled = false;
        StartCoroutine(WaitForDestroy(this));
    }
    IEnumerator WaitForDestroy(EnemyController e)
    {
        yield return new WaitForSeconds(e.timeDestroy);
        e.destructor = true;
    }

	// Update is called once per frame
	void Update () {
        if (destructor)
            Destroy(gameObject);
        if (aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        else if(aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
    }
}
