using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

/*

This script controls events at the end of the scenario, Including button behaviour AND 
text events for summative questions at the end of the scenario.

*/

public class SummativeQuestions_Case1 : MonoBehaviour
{
    public GameObject SummativeQuestionPanel;
    public TextMeshProUGUI SummativeQuestionText;
    public TextMeshProUGUI Choice1;
    public TextMeshProUGUI Choice2;
    public TextMeshProUGUI Choice3;
    public TextMeshProUGUI SummativeExplanationText;

    public GameObject EndPanel;
    public GameObject ReflectionPanel;

    public List<string> PlayerAnswers = new List<string>(); // Store player's answers
    public List<List<string>> CorrectAnswersList = new List<List<string>>(); // Store correct answers for each question
    public List<Button> choiceButtons; // Reference to the Button components of the choices
    public Button submitButton; // Reference to the submit button
    public GameObject Submit; // the submit button gameobject
    public GameObject correctPanel; // Reference to the correct panel
    public GameObject wrongPanel; // Reference to the wrong panel


    private bool[] isSelected; // Array to track button selection states

    private int questionNum = 0;


    // Button Behaviour
    void Start()
    {
        SummativeQuestionPanel.SetActive(false);
        EndPanel.SetActive(false);
        ReflectionPanel.SetActive(false);
        CorrectAnswersList.Add(new List<string> { "1", "2", "3" }); // Correct answers for the question
        CorrectAnswersList.Add(new List<string> { "1", "3" });
        CorrectAnswersList.Add(new List<string> { "1", "2", "3" });

        isSelected = new bool[choiceButtons.Count]; // Initialize the array size

        // Assign the onClick event dynamically to avoid using Unity's inspector for each button
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            int buttonIndex = i; // Capture the current button index to avoid closure issues
            choiceButtons[i].onClick.AddListener(() => ToggleSelection(buttonIndex));
        }

    }

    public void ShowEndPanel(){
        EndPanel.SetActive(true);
    }
    void ToggleSelection(int buttonIndex){
        isSelected[buttonIndex] = !isSelected[buttonIndex]; // Toggle the selection state
       
        // Update button color based on selection state
        Color newColor = isSelected[buttonIndex] ? new Color(0.5f, 0.8f, 0.8f) : Color.white;
        choiceButtons[buttonIndex].GetComponent<Image>().color = newColor;


        // Update player's answers
        string choiceName = choiceButtons[buttonIndex].name;
        if (isSelected[buttonIndex])
        {
            PlayerAnswers.Add(choiceName); // Add the choice to player's answers
        }
        else
        {
            PlayerAnswers.Remove(choiceName); // Remove the choice from player's answers
        }
    }


    public void SubmitAnswers(){
        Debug.Log("Player Answers: " + string.Join(", ", PlayerAnswers));
        Debug.Log("Correct Answers: " + string.Join(", ",  CorrectAnswersList[questionNum]));
        Debug.Log("Question num: " + string.Join(", ",  questionNum));
        // Check if all correct choices are selected for the specified question
        bool allCorrectSelected = true;
        foreach (string correctAnswer in CorrectAnswersList[questionNum]){
            if (!PlayerAnswers.Contains(correctAnswer)){
                allCorrectSelected = false;
                break;
            }
        }


        // Show appropriate panel based on correctness of the specified question
        if (allCorrectSelected && PlayerAnswers.Count == CorrectAnswersList[questionNum].Count){
            correctPanel.SetActive(true); // Show the correct panel
        }
        else{
            wrongPanel.SetActive(true); // Show the wrong panel
        }
    }

    public void IncreaseQuestionNum(){
        questionNum++;
    }

    public void ChoiceClicked(){
        Submit.SetActive(true);
    }


    // Function to clear all selections and player's answers
    public void ClearSelections()
    {
        // Reset all selection states and button colors
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            isSelected[i] = false;
            choiceButtons[i].GetComponent<Image>().color = Color.white;
        }
       
        // Clear player's answers and reset submission state
        PlayerAnswers.Clear();

        // Hide panels
        Submit.SetActive(false);
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
    }

    // Text events

    public void NextSummativeQuestions(){
        ClearSelections();
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
        SummativeQuestionPanel.SetActive(true);
        SummativeQuestionText.text = GetQuizQuestionText(questionNum);
        Choice1.text = GetChoice1Text(questionNum);
        Choice2.text = GetChoice2Text(questionNum);
        Choice3.text = GetChoice3Text(questionNum);
        SummativeExplanationText.text = GetQuizExplanationText(questionNum);


        if (questionNum == 3){
            ReflectionPanel.SetActive(true);
        }
       
    }

    public string GetQuizQuestionText(int index){
        string[] QuizQuestions = {
            "Reflecting on the scenario you just completed, please identify the effective patient-pharmacy professional interactions in the scenario. Select all that apply.",
            "Which of the following tasks fall within the scope of both pharmacy technicians and pharmacists? Select all that apply.",
            "What was effective in Sayed and Damilola's interactions? Select all that apply."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }
    public string GetChoice1Text(int index){
        string[] Choice1Texts = {"Use of both closed-ended and open-ended line of questioning.",
        "Perform a technical check of new or refill medications.",
        "Complete and concise handoffs between patient interactions."};
        if (index >= 0 && index < Choice1Texts.Length)
            return Choice1Texts[index];
        else
            return "";
    }
    public string GetChoice2Text(int index){
        string[] Choice2Texts = {"Consulted patient profile to make appropriate recommendations.",
        "Provide information on medication dose and administration.",
        "Demonstrated collaboration between team members and appreciation for each other's contributions."};
        if (index >= 0 && index < Choice2Texts.Length)
            return Choice2Texts[index];
        else
            return "";
    }
    public string GetChoice3Text(int index){
        string[] Choice3Texts = {"Both non-pharmacological and follow-up care was offered.",
        "Providing instruction on how to use the blood pressure monitoring device.",
        "Optimized the scope of practice of each pharmacy professional."};
        if (index >= 0 && index < Choice3Texts.Length)
            return Choice3Texts[index];
        else
            return "";
    }
    public string GetQuizExplanationText(int index){
        string[] QuizQuestions = {
            "As Bob is a regular patient of the pharmacy, he was greeted by name, which prompted a positive triage process. During the initial interaction, Sayed gathered information that facilitated Damilola's further assessment of Bob's concerns. In the scenario, the two pharmacy professionals collaborated in order to provide optimal patient care (pharmacological and non-pharmacological recommendations). Follow-up care was also offered such as checking in with the patient to see how he is doing and to teach him the proper use of the blood pressure monitor. ",
            "Both pharmacists and pharmacy technicians can perform information gathering, complete technical verification of prescriptions, and educate on medical device use. Only pharmacists can make a clinical recommendation or perform clinical counselling.",
            "Sayed provided Damilola with a concise summary of Bob's presentation. Sayed and Damilola valued and trusted each other and said thank you to demonstrate their appreciation. Between interactions, the pharmacy technician and pharmacist referenced the other to Bob to highlight the partnership and continuity. The pharmacist and pharmacy technician completed different tasks that enabled each of them to practice to their full scope."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }




}
