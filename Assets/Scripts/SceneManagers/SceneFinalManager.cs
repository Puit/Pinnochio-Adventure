using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFinalManager : MonoBehaviour
{

    public DialogManager dialogManager;
    public DialogManager dialogManagerNo;
    public PlayerMovement player;
    public GameManager quizManager;
    public FinalManager finalManager;

    public Text textNo;
    public bool wrongAnswer = false;
    public bool quizGameDone = false;
    private bool flag1 = false;
    private bool flag2 = false;
    void Start()
    {
        dialogManager.text.text = "";
        dialogManager.setVisibilityOn();
        dialogManager.StartDialog();
    }
    private bool CheckIfDialogIsOn(DialogManager d)
    {
        if (d.dialogOn && !d.dialogDone)
            return true;
        else
            return false;
    }
    private void SetNextToDialog(DialogManager d)
    {
        if (CheckIfDialogIsOn(d))
        {
            if (dialogManager.text.text == d.sentences[d.index])
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    d.NextSentence();
                }
            }
        }
        else
        {
            d.setVisibilityOff();
        }
    }

    void Update()
    {
        //Pinnocchio and the Lady are talking
        if (dialogManager.dialogOn)
        {
            SetNextToDialog(dialogManager);
            player.enabled = false;
        }

        //Final animation and back to Main menu
        if (dialogManager.dialogDone && !flag1)
        {
            dialogManager.text.text = "";
            //dialogManager.setVisibilityOff();
            StopAllCoroutines();
            quizManager.ActivateQuiz();
        }
        if (quizGameDone || flag2)
        {
            if (wrongAnswer && flag1)
            {
                //dialogManagerNo.text.text = "";
                //dialogManagerNo.panel.enabled = true;
                //dialogManagerNo.setVisibilityOn();
                //dialogManagerNo.StartDialog();
                textNo.text = "LADY: NO :D";
                quizManager.DeactivateQuiz();

                flag1 = true;
                flag2 = true;
            }
            finalManager.EnableFinishTrance();
            finalManager.finish = true;
            flag2 = true;
        }
    }
}
