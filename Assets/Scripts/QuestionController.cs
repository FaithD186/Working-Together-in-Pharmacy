using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

/*
This script controls the question/quiz behaviour. 

*/
public class QuestionController : MonoBehaviour
{
    // public GameObject Submit;
    // public GameObject Continue; // correct answer panel
    // public GameObject Close; // incorrect answer panel

    // // public GameObject[] QuestionPanels;
    // // public GameObject[] CorrectPanels;
    // // public GameObject[] IncorrectPanels;
    // public GameObject EndPanel;

    // private string playerChoice;
    // public string[] correctAnswers; // correct answers (e.g. {"2", "False", "True", "True"}) are assigned in the inspector
    // private int currentQuestionIndex = 0;


    // void Start()
    // {
    //     SetQuestionPanel(currentQuestionIndex);
    //     EndPanel.SetActive(false);
    // }

    // methods for first question panel (multiple choices)
    // public void OnChoice1Click()
    // {
    //     playerChoice = "1";
    //     CheckAnswer();

    // }
    // public void OnChoice2Click()
    // {
    //     playerChoice = "2";
    //     CheckAnswer();

    // }
    // public void OnChoice3Click()
    // {
    //     playerChoice = "3";
    //     CheckAnswer();

    // }
    // public void OnChoice4Click(){
    //     playerChoice = "4";
    //     CheckAnswer();
    // }
    // public void CheckAnswer(){
    //     if (playerChoice != correctAnswers[currentQuestionIndex]){
    //         IncorrectPanels[currentQuestionIndex].SetActive(true);
    //     }else{
    //         CorrectPanels[currentQuestionIndex].SetActive(true);
    //     }
    // }
    // public void closeClicked(){
    //     IncorrectPanels[currentQuestionIndex].SetActive(false);
    //     playerChoice = "None";
    // }
    // public void ContinueClicked(){
    //     if (currentQuestionIndex == QuestionPanels.Length - 1){
    //         EndPanel.SetActive(true);
    //         QuestionPanels[currentQuestionIndex].SetActive(false);
    //     }
    //     else{
    //         SetQuestionPanel(currentQuestionIndex);
    //         QuestionPanels[currentQuestionIndex].SetActive(false);
    //         QuestionPanels[currentQuestionIndex++].SetActive(true);
    //     }
    //     playerChoice = "None";

    // }

     // methods for T/F questions
    // public void TrueClicked(){
    //     playerChoice = "True";
    //     CheckAnswer();
    // }
    // public void FalseClicked(){
    //     playerChoice = "False";
    //     CheckAnswer();
    // }

    // private void SetQuestionPanel(int index)
    // {
    //     foreach (var panel in QuestionPanels)
    //     {
    //         panel.SetActive(false);
    //     }

    //     foreach (var panel in CorrectPanels)
    //     {
    //         panel.SetActive(false);
    //     }

    //     foreach (var panel in IncorrectPanels)
    //     {
    //         panel.SetActive(false);
    //     }
    //     playerChoice = "None";
    // }
}
