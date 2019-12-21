using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Image panel;
    public Text text;
    public string[] sentences;
    public int index = 0;
    public float typingSpeed;
    public bool dialogOn = false;
    //public bool quizOn = false;
    public bool dialogDone = false;
	// Use this for initialization
	void Start () {
        //text.text = "";
	}
    public void StartDialog()
    {
        text.text = "";
        dialogOn = true;
        setVisibilityOn();
        StartCoroutine(Type());
    }
    //Pensaba hacer que rellene el dialogo pero al estar hecho el Type con un foreach perdere mucho tiempo
    //public void FillAllDialog()
    //{
    //    text.text = sentences[index];
    //    index = sentences.Length - 1;
    //}
    private void Update()
    {
        //setVisibility();
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray()){
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void setVisibilityOn()
    {
        panel.color = new Color(0f, 0f, 0f, 165f / 255f);
        text.color = new Color(1f, 1f, 1f, 1f);
    }
    public void setVisibilityOff()
    {
        panel.color = new Color(0f, 0f, 0f, 0f);
        text.color = new Color(0f, 0f, 0f, 0f);
    }
    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            text.text = "";
            dialogOn = false;
            dialogDone = true;
            setVisibilityOff();
        }
    }
}
