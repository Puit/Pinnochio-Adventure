using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpIntro : MonoBehaviour {

    public bool introDone = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update () {
		if (!introDone)
        {
            introDone = FindObjectOfType<Scene2Manager>().jumpIntro;
        }
	}
}
