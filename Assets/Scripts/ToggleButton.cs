using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ToggleButton : MonoBehaviour
{

    public List<string> PlayerAnswers = new List<string>(); // Store player's answers
    public List<List<string>> CorrectAnswersList = new List<List<string>>(); // Store correct answers for each question
    public List<Button> choiceButtons; // Reference to the Button components of the choices
    public Button submitButton; // Reference to the submit button
    public GameObject correctPanel; // Reference to the correct panel
    public GameObject wrongPanel; // Reference to the wrong panel

    private bool[] isSelected; // Array to track button selection states
    private bool isSubmitted = false; // Flag to track if the submit button is pressed

    public int questionNum = 0;

    void Start()
    {
        CorrectAnswersList.Add(new List<string> { "3" }); // Correct answers for the question
        CorrectAnswersList.Add(new List<string> { "2" });
        CorrectAnswersList.Add(new List<string> { "3" });
        CorrectAnswersList.Add(new List<string> { "3", "2" });

        isSelected = new bool[choiceButtons.Count]; // Initialize the array size

        // Assign the onClick event dynamically to avoid using Unity's inspector for each button
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            int buttonIndex = i; // Capture the current button index to avoid closure issues
            choiceButtons[i].onClick.AddListener(() => ToggleSelection(buttonIndex));
        }

        //submitButton.onClick.AddListener(SubmitAnswers); // Assign the onClick event for the submit button
    }

    // Toggle button selection state
    void ToggleSelection(int buttonIndex)
    {
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

    public void SubmitAnswers()
{
    Debug.Log("Player Answers: " + string.Join(", ", PlayerAnswers));
    Debug.Log("Correct Answers: " + string.Join(", ",  CorrectAnswersList[questionNum]));
    Debug.Log("Question num: " + string.Join(", ",  questionNum));
    // Check if all correct choices are selected for the specified question
    bool allCorrectSelected = true;
    foreach (string correctAnswer in CorrectAnswersList[questionNum])
    {
        if (!PlayerAnswers.Contains(correctAnswer))
        {
            allCorrectSelected = false;
            break;
        }
    }

    // Show appropriate panel based on correctness of the specified question
    if (allCorrectSelected && PlayerAnswers.Count == CorrectAnswersList[questionNum].Count)
    {
        correctPanel.SetActive(true); // Show the correct panel
    }
    else
    {
        wrongPanel.SetActive(true); // Show the wrong panel
    }

}
    public void IncreaseQuestionNum(){
        questionNum++;
    }
    public void ReplayScene(){
        wrongPanel.SetActive(false);
        correctPanel.SetActive(false);
        ClearSelections();
        // move to togglebutton and add in clear selection
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
        isSubmitted = false;

        // Hide panels
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
    }
}
