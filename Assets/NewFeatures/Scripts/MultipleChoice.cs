using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MultipleChoice : MonoBehaviour, IInteractable
{
    private int arraySize = 4;
    private int questionNumber = 0;

    [System.Serializable]
    public class Question
    {
        public string text;

        [System.Serializable]
        public class Answer
        {
            public string text;
            public bool correct;
        }

        public Answer[] answer;
    }

    public Question[] question;

    private void OnValidate()
    {
        foreach (var answerNumber in question)
            if (answerNumber.answer.Length > arraySize)
            {
                Debug.LogWarning("Du må ikke have flere svar end 4!");
                Array.Resize(ref answerNumber.answer, arraySize);
            }
    }

    public void AnswerQuestion(int answerNumber)
    {
        //if (question[questionNumber].answer[answerNumber].correct)

    }

    public void Interact()
    {
        
    }
}
