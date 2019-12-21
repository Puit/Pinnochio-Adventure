using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalator : MonoBehaviour {

    public float width, heigth;
    // Use this for initialization
    void Start()
    {
        width = (float)Screen.width/1024;
        heigth = (float)Screen.height/768;
        RectTransform rTransform = GetComponent<RectTransform>();
        rTransform.localScale = new Vector3(width, heigth, 1);
    }
}
