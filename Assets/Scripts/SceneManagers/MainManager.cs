using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public FinalManager finalManager;
	
    public void goToScene1()
    {
        finalManager.EnableFinishTrance();
        finalManager.finish = true;
    }
}
