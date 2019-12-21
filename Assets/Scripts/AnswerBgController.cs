using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBgController : MonoBehaviour {

    Text text;
    Image image;
    public Color wrong = new Color(253 / 255, 73 / 255, 83 / 255, 255 / 255);
    public Color right = new Color(94 / 255, 255 / 255, 123 / 255, 255 / 255);

    void Start () {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }
	
    bool checkIfAnswerLied()
    {
        if (text.text.Equals("WELL LIED!"))
            return true;
        else
            return false;
    }

	void Update () {

        if (checkIfAnswerLied())
            image.color = right;
        else
            image.color = wrong;
            
	}
}
