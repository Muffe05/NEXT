using System;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoice : MonoBehaviour, IInteractable
{
    [Header("PossibleOutcomes")]
    [SerializeField] private bool tryAgain = false;
    [SerializeField] private bool startAgain = false;
    [SerializeField] private bool goBack = false;
    [Space(10)]

    [Header("OtherVariables")]
    public GameObject cam;
    [SerializeField] private Animator anim;
    [Space(10)]

    private int arraySize = 4;
    private int questionNumber = 0;

    private bool won = false;

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

        public Answer[] answer = new Answer[4];
    }

    public Question[] question;

    private void Start()
    {
        anim = MultipleChoiceManager.multipleChoiceManager.gameObject.GetComponent<Animator>();
    }
    private void OnValidate()
    {
        foreach (var answerNumber in question)
        {
            if (answerNumber.answer.Length > arraySize)
            {
                Debug.LogWarning("Du må ikke have flere svar end 4!");
                Array.Resize(ref answerNumber.answer, arraySize);
            }
            if (answerNumber.answer.Length < arraySize)
            {
                Debug.LogWarning("Hvis du gerne vil fjerne et svar, så fjern teksten!");
                Array.Resize(ref answerNumber.answer, arraySize);
            }
        }
        for (int i = 0; i < question.Length; i++)
            for (int j = 0; j < question[i].answer.Length; j++)
                if (question[i].answer[j].correct && question[i].answer[j].text == "")
                {
                    Debug.LogWarning("Du kan ikke have et tomt svar som korrekt!");
                    question[i].answer[j].correct = false;
                }
    }

    public void AnswerQuestion(int answerNumber)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (question[questionNumber].answer[answerNumber].correct)
        {
            questionNumber++;

            if (questionNumber >= question.Length)
            {
                AnswerManager.answerManager.anim.SetTrigger("You Win");
                won = true;

                MultipleChoiceManager.multipleChoiceManager.Deactivate();
                return;
            }

            anim.SetTrigger("Correct");
            AnswerManager.answerManager.anim.SetTrigger("Correct");

            Invoke("InputValues", 1f);
            return;
        }

        if (tryAgain)
            anim.SetTrigger("Wrong");
        else if (questionNumber == 0)
            anim.SetTrigger("Wrong");
        else
            anim.SetTrigger("Correct");

        AnswerManager.answerManager.anim.SetTrigger("Incorrect");

        if (startAgain)
            questionNumber = 0;
        else if (goBack && questionNumber > 0)
            questionNumber--;

        Invoke("InputValues", 1f);
    }

    public void Interact()
    {
        if (won)
        {
            AnswerManager.answerManager.anim.SetTrigger("Finished");
            return;
        }

        MultipleChoiceManager.multipleChoiceManager.Activate(this);
        InputValues();
    }

    private void InputValues()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
            
        if (MultipleChoiceManager.multipleChoiceManager.text[0].text != null)
            MultipleChoiceManager.multipleChoiceManager.text[0].text = question[questionNumber].text;

        for (int i = 1; i < MultipleChoiceManager.multipleChoiceManager.text.Length; i++)
        {
            if (question[questionNumber].answer[i - 1].text == "")
                MultipleChoiceManager.multipleChoiceManager.text[i].GetComponentInParent<RawImage>().enabled = false;
            else
                MultipleChoiceManager.multipleChoiceManager.text[i].GetComponentInParent<RawImage>().enabled = true;

            MultipleChoiceManager.multipleChoiceManager.text[i].text = question[questionNumber].answer[i - 1].text;
        }
    }
}
