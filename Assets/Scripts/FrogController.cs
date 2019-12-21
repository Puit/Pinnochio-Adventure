using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

    protected float paraboleVariable;

    public Animator animator;
    public float divisor = 1.3f;
    public float heigth = 0.1f;
    public float timeWaitting = 2f;
    public bool waiting = false;
    public float ajustador = 1.28f;

    private float elapse = 0f;
    private bool oposite = false;

    Vector3 startPosition;
    Vector3 position1;
    Vector3 position2;
    Vector3 localScale;

    public Transform target;
    private void Start()
    {
        startPosition = transform.position;
        position1 = transform.position;
        position2 = target.position;
        localScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        paraboleVariable += Time.deltaTime;
        paraboleVariable = paraboleVariable % divisor;

        if (startPosition == position1)
        {
            if(transform.position.y <= startPosition.y + 0.1f)
            {
                animator.SetBool("Jump", false);
                position1 = target.position;
                position2 = startPosition;
                waiting = true;
                oposite = true;
            }
        }
        else
        {
            if (transform.position.y <= startPosition.y + 0.1f)
            {
                animator.SetBool("Jump", false);
                position2 = target.position;
                position1 = startPosition;
                waiting = true;
                oposite = false;
            }
        }
        elapse += Time.deltaTime;
        if (elapse >= timeWaitting)
        {
            waiting = false;
            animator.SetBool("Jump", true);
            elapse = 0f;
            if (oposite)
            {
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.position = position2;
            }
            else
            {
                transform.localScale = localScale;
                transform.position = position1;
            }
                

        }
        if(!waiting)
            transform.position = MathParabola.Parabola(position1, position2, heigth, paraboleVariable / ajustador);
    }
}
