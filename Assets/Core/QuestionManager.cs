// QuestionManager.cs - HỎI ĐÁP ẨU
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public Button answerA, answerB;
    public string correctAnswer;
    private Action onCorrect;
    private Action onWrong;
    string currentQuestion;

    public void ShowQuestion(string question, string optionA, string optionB, string correct, Action onCorrect = null, Action onWrong = null)
    {
        //Time.timeScale = 0; // Dừng game
        PlayerController.Instance.Stop();
        questionPanel.SetActive(true);

        questionText.text = question;
        if (optionA.Trim().Equals(string.Empty))
        {
            answerA.gameObject.SetActive(false);
        }
        if (optionB.Trim().Equals(string.Empty))
        {
            answerB.gameObject.SetActive(false);
        }

        answerA.GetComponentInChildren<TextMeshProUGUI>().text = optionA;
        answerB.GetComponentInChildren<TextMeshProUGUI>().text = optionB;

        this.onCorrect = onCorrect;
        this.onWrong = onWrong;
        correctAnswer = correct;
    }

    public void OnAnswerSelected(string selected)
    {
        if (selected == correctAnswer)
        {
            Debug.Log(onCorrect);
            questionPanel.SetActive(false);
            PlayerController.Instance.UnStop();

            //Time.timeScale = 1;
            onCorrect?.Invoke();
        }
        else
        {
            onWrong?.Invoke();  
            GameManager.instance.LoseLife();
            questionPanel.SetActive(false);
            PlayerController.Instance.UnStop();
            //Time.timeScale = 1;
        }
    }
}