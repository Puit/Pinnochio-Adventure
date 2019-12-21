using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableCranck : MonoBehaviour {

    public List<GameObject> myGameObjects;
    public Animator myAnimator;
    bool b = false;

	public void Action()
    {
        b = !b;
        if (b)
        {
            myAnimator.SetTrigger("Activate");
            for(int i = 0; i<= myGameObjects.Count-1; i++)
            {
                myGameObjects[i].SetActive(false);
            }
        }
        else
        {
            myAnimator.SetTrigger("Deactivate");
            for (int i = 0; i <= myGameObjects.Count-1; i++)
            {
                myGameObjects[i].SetActive(true);
            }
        }
    }
	// Update is called once per frame
}
