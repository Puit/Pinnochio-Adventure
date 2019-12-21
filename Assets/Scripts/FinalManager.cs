using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalManager : MonoBehaviour {

    public Text text;
    public string finalText = "";
    public string sceneName = "";
    public Color blackColor = new Color(0f, 0f, 0f, 0f);
    public Color whiteColor = new Color(1f, 1f, 1f, 0f);
    public RawImage rI;
    public bool finish = false;

    float elapse = 0f;
    public float timeTransition = 0.01f;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        rI = GetComponentInChildren<RawImage>();
    }
    public void EnableFinishTrance()
    {
        gameObject.SetActive(true);
        finish = true;
    }
    private void Update()
    {
        if (finish)
        {
            text.text = finalText;
            rI.color = blackColor;

            elapse += Time.deltaTime;
            if (elapse >= timeTransition)
            {
                elapse = 0f;
                //Finish the scene and go to next one
                if (sceneName != "")
                {
                    GoToScene goToScene = new GoToScene();
                    goToScene.ChangeScene(sceneName);
                }
            }
            else
            {
                //increment visibility
                blackColor = new Color(0f, 0f, 0f, elapse / timeTransition);
                whiteColor = new Color(1f, 1f, 1f, elapse / timeTransition);
            }
        }

    }
}
