using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizFinisher : MonoBehaviour {

    public SequenceScene1 SequenceManager;

    private void Update()
    {
        if(transform.localScale.x > 12.16134f)
        {
            SequenceManager.quizGameDone = true;
        }
    }
}
