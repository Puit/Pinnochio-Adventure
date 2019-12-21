using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Scene2Manager : MonoBehaviour {

    public int life = 6;
    public int stars = 0;
    public DialogManager dialogManager;
    public bool flag1 = false;
    public BezierFollow squirrelLeaving;
    public PlayerMovement player;
    public FinalManager finalManager;
    public Text starText;
    public Transform squirrel;
    private Vector3 squirrelStartScale;
    public bool jumpIntro = false;

    //public CinemachineVirtualCamera cinemachineCamera;
    // Use this for initialization
    void Start () {
        dialogManager.text.text = "";
        dialogManager.setVisibilityOn();
        dialogManager.StartDialog();
        squirrelStartScale = squirrel.localScale;
        jumpIntro = FindObjectOfType<JumpIntro>().introDone;
    }
    public void Hit(int damage)
    {
        life -= damage;
        if (life < 1)
            DeathController();
    }
    public void DeathController()
    {
        //Death animation and reStart the scene
        player.Death();
        Debug.Log("DEATH");

        //Play Animation of GAME OVER and reestart scene
        finalManager.text.text = "GAME OVER";
        finalManager.timeTransition = 1f;
        finalManager.sceneName = "Scene2";

        finalManager.EnableFinishTrance();
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
        if (!jumpIntro)
        {
            //Pinnocchio and squirrel are talking
            if (dialogManager.dialogOn)
            {
                SetNextToDialog(dialogManager);
                player.enabled = false;
                squirrel.transform.localScale = new Vector3(-squirrelStartScale.x, squirrelStartScale.y, squirrelStartScale.z);
            }

            //Squirrel go out of the scene
            if (dialogManager.dialogDone && !squirrelLeaving.moving && !squirrelLeaving.finish)
            {
                if (squirrel != null)
                    squirrel.transform.localScale = new Vector3(squirrelStartScale.x, squirrelStartScale.y, squirrelStartScale.z);
                squirrelLeaving.coroutineAllowed = true;
                //Debug.Log("SQUIRREL LEAVING");
            }
            if (dialogManager.dialogDone && squirrelLeaving.finish)
            {
                if(squirrel != null)
                    Destroy(squirrel.gameObject);
                player.enabled = true;
                dialogManager.text.text = "";
                dialogManager.setVisibilityOff();
                jumpIntro = true;
            }
        }
        else
        {
            if (squirrel != null)
                Destroy(squirrel.gameObject);
            player.enabled = true;
            dialogManager.text.text = "";
            dialogManager.setVisibilityOff();
        }

        if (stars > 10)
        {
            starText.text = stars.ToString();
            Debug.Log("IF");
        }
        else
        {
            starText.text = "0" + stars.ToString();
            Debug.Log("Else");
        }
        if (stars >= 250)
        {
            finalManager.EnableFinishTrance();
            finalManager.finish = true;
        }
    }
}
