using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsLifeController : MonoBehaviour {
    public Scene2Manager sceneManager;
    public Animator heart1;
    public Animator heart2;
    public Animator heart3;

    void SetHearts()
    {
        switch (sceneManager.life)
        {
            case 6:
                heart1.SetTrigger("Full");
                heart2.SetTrigger("Full");
                heart3.SetTrigger("Full");
                break;
            case 5:
                heart1.SetTrigger("Half");
                heart2.SetTrigger("Full");
                heart3.SetTrigger("Full");
                break;
            case 4:
                heart1.SetTrigger("Empty");
                heart2.SetTrigger("Full");
                heart3.SetTrigger("Full");
                break;
            case 3:
                heart1.SetTrigger("Empty");
                heart2.SetTrigger("Half");
                heart3.SetTrigger("Full");
                break;
            case 2:
                heart1.SetTrigger("Empty");
                heart2.SetTrigger("Empty");
                heart3.SetTrigger("Full");
                break;
            case 1:
                heart1.SetTrigger("Empty");
                heart2.SetTrigger("Empty");
                heart3.SetTrigger("Half");
                break;
            case 0:
                heart1.SetTrigger("Empty");
                heart2.SetTrigger("Empty");
                heart3.SetTrigger("Empty");
                break;
            default:
                heart1.SetTrigger("Full");
                heart2.SetTrigger("Full");
                heart3.SetTrigger("Full");
                break;
        }
    }
    // Update is called once per frame
    void Update () {
        SetHearts();
	}
}
