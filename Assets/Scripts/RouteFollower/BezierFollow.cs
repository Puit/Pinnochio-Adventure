using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour {

    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 followerPosition;
    public float speedCurveModifier;
    public float speedLineModifier;
    public bool coroutineAllowed;

    private Animator animator;

    private List<Vector3> targets = new List<Vector3>();
    private int currentTarget = 0;
    private Vector3 startPosition;

    public bool moving = false;
    public bool finish = false;
    private float gravity;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        animator = transform.GetComponent<Animator>();
        routeToGo = 0;
        tParam = 0f;
        startPosition = transform.position;
        coroutineAllowed = false;
        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            gravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
            
    }
    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
            animator.SetTrigger("Run");
        }

    }
    void FlipSprite(Vector3 nextPosition)
    {
        if (transform.position.x < nextPosition.x)
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
        else
        {
            if (transform.position.x > nextPosition.x)
            {
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }
        }
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        moving = true;

        if (routes[routeNumber].GetComponent<Route>().isCurve)
        {
            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedCurveModifier;

                followerPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                    3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                    Mathf.Pow(tParam, 3) * p3;

                FlipSprite(followerPosition);
                transform.position = followerPosition;
                yield return new WaitForEndOfFrame();
            }
            tParam = 0f;
            routeToGo += 1;
            if (routeToGo > routes.Length - 1)
            {
                coroutineAllowed = false;
                finish = true;
            }
            else
            {
                coroutineAllowed = true;
            }
            if (finish)
            {
                if (gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
                }
            }
        }
        else
        {
            if(targets.Count == 0)
                for (int i = 0; i <= routes[routeNumber].childCount -1; i++)
                {
                    targets.Add(routes[routeNumber].GetChild(i).position);
                }
            if (transform.position != targets[currentTarget])
            {
                Vector3 pos = targets[currentTarget];
                Rigidbody2D rb;
                tParam = 0;
                startPosition = transform.position;
                if(GetComponent<Rigidbody2D>() != null)
                {
                    rb = GetComponent<Rigidbody2D>();
                }
                else
                {
                    rb = gameObject.AddComponent<Rigidbody2D>();
                    rb.mass = 1;
                    rb.gravityScale = 0;
                    rb.constraints =  RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                    rb.isKinematic = false;
                }
                //Debug.Log("pos = " + pos);
                while (tParam < 1)
                {
                    FlipSprite(pos);
                    tParam += Time.deltaTime / speedLineModifier;
                    rb.MovePosition(Vector3.Lerp(startPosition, pos, tParam));
                    yield return new WaitForEndOfFrame();
                }
                coroutineAllowed = true;
            }
            else
            {
                currentTarget++;
                //Debug.Log("Current Target = " + currentTarget + " Targets.Count = " + targets.Count);
                if (currentTarget < targets.Count)
                {
                    //Debug.Log("Im in");
                    StartCoroutine(GoByTheRoute(routeToGo));
                    coroutineAllowed = true;
                }
                else
                {
                    finish = true;
                    moving = false;
                    //Debug.Log("routeToGo = " + routeToGo);
                    //Debug.Log("routes.Length - 1 = " + (routes.Length - 1));
                    if (routeToGo >= routes.Length - 1)
                    {
                        coroutineAllowed = false;
                        finish = true;
                    }
                    else
                    {
                        routeToGo++;
                        coroutineAllowed = true;
                    }
                }

            }

        }
    }
}
