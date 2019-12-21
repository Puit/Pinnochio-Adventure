using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFollowerScript : MonoBehaviour {

    public AIPath aiPath;
    public Vector3 startPosition;
    bool backToPosition = false;
    float elapse = 0;
    private void Start()
    {
        aiPath.canSearch = false;
        startPosition = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            aiPath.canSearch = true;
            backToPosition = false;
            elapse = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            aiPath.canSearch = false;
            backToPosition = true;
        }
    }
    private void Update()
    {
        if (backToPosition)
        {
            if(elapse<1)
                elapse += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, startPosition, elapse);
        }
        
    }
}
