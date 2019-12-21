using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //To convert the array to a list quickly :)
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject quiz;
    //public DialogManager dialog;
    public Question[] questions; //We use an array because we are not gonna resize it while playing
    private static List<Question> unansweredQuestions;// We use a list because we are gonna resize it while playing and is static to enable this list to persist when we change scenes
    private Question currentQuestion;
    private float timeBetweenQuestions = 1.5f;
    public float noseGrowerFactor = 45f;
    public float timeNoseGrowing = 3f;

    [SerializeField]
    public Text firstText;
    [SerializeField]
    public Text secondText;

    [SerializeField]
    private Text factText;
    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;
    [SerializeField]
    private Animator animator;

    //Nose and squirrel
    public Transform nose;


    private void Start()
    {
        GetQuestion();
    }
    public void GetQuestion()
    {
        //We store the questions in the unansweredQuestions list
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentQuestion();
        quiz.SetActive(false);
    }
    public void ActivateQuiz()
    {
        quiz.SetActive(true);
    }
    public void DeactivateQuiz()
    {
        quiz.SetActive(false);
    }
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
        unansweredQuestions.RemoveAt(randomQuestionIndex);

        firstText.text = currentQuestion.firstText;
        secondText.text = currentQuestion.secondText;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "YOU DIDN'T LIE!"; //CORRECT
            falseAnswerText.text = "WELL LIED!";  //WRONG
        }
        else
        {
            trueAnswerText.text = "WELL LIED!";    //WRONG
            falseAnswerText.text = "YOU DIDN'T LIE!"; //CORRECT
        }

    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetQuestion();
    }

    IEnumerator GrowNose()
    {
        nose.localScale = new Vector3(nose.localScale.x + Time.deltaTime * noseGrowerFactor, nose.localScale.y, nose.localScale.z);
        yield return new WaitForSeconds(timeNoseGrowing); 
    }
    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        StartCoroutine(TransitionToNextQuestion());

        if (trueAnswerText.text.Equals("WELL LIED!"))
            StartCoroutine(GrowNose());

    }
    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        StartCoroutine(TransitionToNextQuestion());
        if (falseAnswerText.text.Equals("WELL LIED!"))
            StartCoroutine(GrowNose());
    }
}
