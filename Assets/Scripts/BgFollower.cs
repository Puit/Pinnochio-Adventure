using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFollower : MonoBehaviour {


    public Transform centerBackground;
    public float offset;
    //public float t =1f;
	
	// Update is called once per frame
	void Update ()
    {
        
        //Vector3 positionToLerp = new Vector3(centerBackground.position.x, transform.position.y, centerBackground.position.z);

        //if (centerBackground.position == positionToLerp)
        //{
        //    t = 0;
        //}
        //else
        //{
        //    t +=  Time.deltaTime;
        //    //Debug.Log(t);
        //    centerBackground.position = Vector3.Lerp(centerBackground.position, positionToLerp, t);
        //}


        if (transform.position.x >= centerBackground.position.x + offset)
            centerBackground.position = new Vector2(transform.position.x + offset, centerBackground.position.y);
        else if (transform.position.x <= centerBackground.position.x - offset)
            centerBackground.position = new Vector2(transform.position.x - offset, centerBackground.position.y);
    }
}
