using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Scene2Manager sceneManager;
    public SpriteRenderer sprite;

    public Animator animator;
    public Collider2D[] myColliders = new Collider2D[3];
    public float runSpeed = 40f;

    public float horizontalMove = 0f;

    public bool jump = false;
    public bool hit = false;
    public bool attack = false;
    public bool visibility = true;

    public float timeHit = 0.5f;
    private bool kickedFromLeft = true;

    public float timeAttack = 0.5f;
    public int damage = 1;
    public bool enableToHit = true;


    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attack", attack);
        animator.SetBool("Hit", hit);
        if(!hit)
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (controller.m_Grounded)
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        else
            animator.SetFloat("Speed", 0f);
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AttackWaiter(this));
        }
        if (visibility)
            sprite.enabled = true;
        else
            sprite.enabled = false;

    }

    //Is called in characterController2D OnLanding Invoke from Unity
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    public void TellSceneManagerIAmHit()
    {
        sceneManager.Hit(damage);
    }
    void FixedUpdate()
    {
        if (hit)
        {
            if (!attack)
            {
                StartCoroutine(HitVisibility(this));
            }
                //controller.Hit(kickedFromLeft, hit); // POSAR Gestor de visibilitat
        }
        else
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
        jump = false;
    }
    IEnumerator HitVisibility(PlayerMovement p) //No puede modificar valores de por si por eso se le entra PlayerMovement
    {
        float divisor = 2f;
        p.visibility = true;
        yield return new WaitForSeconds(p.timeHit / divisor);
        p.visibility = false;
        yield return new WaitForSeconds(p.timeHit / divisor);
        p.visibility = true;
    }

        IEnumerator HitWaiter(PlayerMovement p) //No puede modificar valores de por si por eso se le entra PlayerMovement
    {
        enableToHit = false;
        p.hit = true;
        yield return new WaitForSeconds(p.timeHit);
        p.hit = false;
        p.controller.hited = false;
        enableToHit = true;
    }
    IEnumerator AttackWaiter(PlayerMovement p)
    {
        p.attack = true;
        yield return new WaitForSeconds(p.timeAttack);
        p.attack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && !attack && enableToHit)
        {
            if (collision.gameObject.GetComponent<EnemyController>() != null)
                damage = collision.gameObject.GetComponent<EnemyController>().damage;
            else
                damage = 1;
            Debug.Log("HIT");
            TellSceneManagerIAmHit();
            StartCoroutine(HitWaiter(this));
            //Debug.Log("HIT");
            if (collision.transform.position.x > transform.position.x)
                kickedFromLeft = true;
            else
                kickedFromLeft = false;
        }
    }
    public void Death()
    {
        for(int i = 0; i <= myColliders.Length - 1; i++)
        {
            myColliders[i].enabled = false;
        }
    }
}