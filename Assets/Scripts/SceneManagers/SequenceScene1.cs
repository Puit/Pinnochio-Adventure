using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceScene1 : MonoBehaviour {

    
    public DialogManager dialogManager1;
    public BezierFollow geppettoLeaving;
    public DialogManager dialogManager2;
    public Animator cageAnimator;
    public BezierFollow squirrelToNose;
    public GameManager quizManager;
    public bool quizGameDone = false;
    private bool quizFlag = false;
    public GameObject cuerdas;
    public DialogManager dialogManager3;
    public FinalManager finalManager;

    private float elapse = 0f;
    private float time = 1f;
    private bool flag2 = false;
    public bool flag3 = false;


    //Squirrel and pinochio leaves

    // Use this for initialization
    void Start () {
        quizManager.DeactivateQuiz();

        dialogManager1.text.text = "";
        dialogManager1.setVisibilityOn();
        dialogManager1.StartDialog();
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
            if (dialogManager1.text.text == d.sentences[d.index])
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

        //Geppetto is talking
        if (dialogManager1.dialogOn)
            SetNextToDialog(dialogManager1);

        //Geppeto is going out of the scene
        if (dialogManager1.dialogDone && !geppettoLeaving.moving && !geppettoLeaving.finish)
            geppettoLeaving.coroutineAllowed = true;

        //We start de dialog Pinocchio and the squirrel are talking
        if (geppettoLeaving != null)
            if (geppettoLeaving.finish && !dialogManager2.dialogOn)
            {
                Destroy(geppettoLeaving.GetComponent<GameObject>());
                dialogManager2.StartDialog();
            }
        if (dialogManager2.dialogOn && !dialogManager2.dialogDone)
            SetNextToDialog(dialogManager2);

        //Quiz starts
        if (dialogManager2.dialogDone && !squirrelToNose.moving && !squirrelToNose.finish && !quizGameDone)
        {
            dialogManager2.text.text = "";
            dialogManager2.setVisibilityOff();
            StopAllCoroutines();
            quizManager.ActivateQuiz();
        }
        //The game finish
        if (quizGameDone && !quizFlag)
        {
            quizFlag = true;
            quizManager.DeactivateQuiz();
            //Squirrel jumps to the nose
            cageAnimator.SetTrigger("Open");
            squirrelToNose.coroutineAllowed = true;
        }
        //If squirrel is back, make animation with the cutet ropes and start the dialogManager3
        if (squirrelToNose.finish)
        {
            Destroy(cuerdas);

            elapse += Time.deltaTime;
            if (elapse >= time)
            {
                elapse = 0f;
                flag2 = true;
            }
        }
        if (flag2 && !flag3)
        {
            dialogManager3.StartDialog();
            flag3 = true;
        }
        if (flag3)
        {
            SetNextToDialog(dialogManager3);
        }
        if(flag3 && dialogManager3.dialogDone)
        {
            finalManager.EnableFinishTrance();
            finalManager.finish = true;
        }
    }
}
