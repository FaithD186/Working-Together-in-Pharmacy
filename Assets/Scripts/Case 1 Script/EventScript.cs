using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

/*

This script controls the presentation of text-based elements and UI screens within Scenario 1. 

Determines the flow of content post-video (e.g. display of quiz, "Did You Know" section, 
computer screens, etc.)

*/

public class EventScript : MonoBehaviour
{
    
    // Panel GameObjects
    public GameObject PatientProfile;
    public GameObject EndPanel;
    public GameObject FactPanel;
    public GameObject MenuButton;
    public GameObject MenuPanel;

    // Video player control GameObjects
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;
    public VideoPlayer[] Videos;

    // Texts
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI ExplanationText;
    public TextMeshProUGUI FactText;

    // Variable Trackers
    private int currentVideoIndex = 0;
    private int currentFactNum = 0;

    // Popups in scenario 
    public GameObject QuestionPanel_Scenario; 
    public GameObject CorrectPanel_Scenario;
    public GameObject IncorrectPanel_Scenario;
    public GameObject CorrectPanel; 
    public GameObject IncorrectPanel;
    public GameObject Continue;
    public GameObject Close;
    public string PlayerAnswer;
    public GameObject SubmitButton;

    // Video playback speed
    public GameObject SpeedButton;
    public GameObject SpeedButtonPressed;

    // Screens
    public GameObject Canvas;
    public GameObject ComputerScreen;
    public GameObject ComputerScreen2;
    public GameObject ReflectionPanel;

    // Refers to quizzes within scenarios
    private int currentQuizNum = 0;
    public TextMeshProUGUI QuizQuestionText;
    public TextMeshProUGUI QuizExplanationText;
    public TextMeshProUGUI Choice1Text;
    public TextMeshProUGUI Choice2Text;
    public TextMeshProUGUI Choice3Text;
    public string[] CorrectAnswers;

    // Canvas elements
    private SequenceStarterScript sequenceStarter;
    private VideoController videoController;
    

    void Start()
    {   
        // Setting up screen displays
        PatientProfile.SetActive(true);
        FactPanel.SetActive(false);
        EndPanel.SetActive(false);
        ComputerScreen.SetActive(false);
        ComputerScreen2.SetActive(false);
        ReflectionPanel.SetActive(false);

        // Setting up UI game elements
        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);

        // Setting up question panels
        QuestionPanel_Scenario.SetActive(false);
        CorrectPanel.SetActive(false);
        IncorrectPanel.SetActive(false);

        // Retrieving canvas elements
        sequenceStarter = Canvas.GetComponent<SequenceStarterScript>();
        videoController = Canvas.GetComponent<VideoController>();        
    }

    public void StartVideo()
    {
        PatientProfile.SetActive(false);

        // Turn on Video control UI components
        MenuButton.SetActive(true);
        SkipButton.SetActive(true);
        SpeedButton.SetActive(true);
        PauseButton.SetActive(true);

        Videos[currentVideoIndex].loopPointReached += LoadFactPanel;
    }

    public void LoadFactPanel(VideoPlayer vp){  
        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);

        if (currentVideoIndex == 0 || currentVideoIndex == 5 || currentVideoIndex == 6){
            Show_QuestionInScenario();
            return;     
        }
        else if (currentVideoIndex == 1 || currentVideoIndex == 7 || currentVideoIndex == 4){
            // In the second video, we are bypassing fact/question panels and just going to 
            // the next act. So "simulate" clicking "Continue" button on a question/fact panel
            // Attach all methods that would normally be attached to a fact/question panel 
            // to proceed to the next act.
            sequenceStarter.StartSeq();
            videoController.ContinueClicked();
            ContinueClicked();
            StartVideo();
            return;
        }
        else if (currentVideoIndex == 2){
            ComputerScreen.SetActive(true);
            return;
        }
        // If video out of bounds
        if (currentVideoIndex >= Videos.Length || currentVideoIndex < 0){
            return;
        }
        else if (currentVideoIndex == Videos.Length - 1){ 
            // After the last video, show ComputerScreen2
            ComputerScreen2.SetActive(true);
        }
        else{
            FactPanel.SetActive(true);
            currentFactNum++;
        }

    }

    public void ChoiceClicked(){
        SubmitButton.SetActive(true);
    }

    public void CloseClicked(){
        IncorrectPanel.SetActive(false);
        IncorrectPanel_Scenario.SetActive(false);
        SubmitButton.SetActive(false);
        PlayerAnswer = "None";
    }
    public void QuestionContinue(){
        QuestionPanel_Scenario.SetActive(false);
        ComputerScreen.SetActive(false);
        SkipButton.SetActive(true);
        SpeedButton.SetActive(true);
        PauseButton.SetActive(true);
        currentVideoIndex++;
    }

    public void ContinueClicked(){ // Continue in Fact Panel
            FactPanel.SetActive(false);
            ComputerScreen.SetActive(false);

            QuestionPanel_Scenario.SetActive(false);
            CorrectPanel_Scenario.SetActive(false);
            IncorrectPanel_Scenario.SetActive(false);

            SkipButton.SetActive(true);
            SpeedButton.SetActive(true);
            PauseButton.SetActive(true);
            currentVideoIndex++;
    }

    public void IncreaseQuizNum(){
        currentQuizNum++;
    }

    public void Show_QuestionInScenario(){
        QuestionPanel_Scenario.SetActive(true);
        IncorrectPanel_Scenario.SetActive(false);
        CorrectPanel_Scenario.SetActive(false);
        SubmitButton.SetActive(false);

        // Set up text elements
        QuizQuestionText.text = GetQuizQuestionText(currentQuizNum);
        QuizExplanationText.text = GetQuizExplanationText(currentQuizNum);
        Choice1Text.text = GetChoice1Text(currentQuizNum);
        Choice2Text.text = GetChoice2Text(currentQuizNum);
        Choice3Text.text = GetChoice3Text(currentQuizNum);
    }

    public void Show_Reflection(){
        ReflectionPanel.SetActive(true);
    }

    public void Choice1Clicked(){
        PlayerAnswer = "1";
        CheckQuizAnswer();
    }
    public void Choice2Clicked(){
        PlayerAnswer = "2";
        CheckQuizAnswer();
    }
    public void Choice3Clicked(){
        PlayerAnswer = "3";
        CheckQuizAnswer();
    }

    public void CheckQuizAnswer(){
        if (PlayerAnswer != CorrectAnswers[currentQuizNum]){
            IncorrectPanel_Scenario.SetActive(true);
        }else{
            CorrectPanel_Scenario.SetActive(true);
            PlayerAnswer = "None";
        }
    }
    
    // Text events 

    public string GetQuizQuestionText(int index){
        string[] QuizQuestions = {
            "What do you think is Sayed's most appropriate next action to help Bob?",
            "What do you think is Damilola's most appropriate next action to help Bob?",
            "What do you think is Damilola's most appropriate next action to help Bob?",
            "Which of the following authorized acts are NOT within the scope of practice of a registered pharmacy technician?"
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }
    public string GetQuizExplanationText(int index){
        string[] QuizQuestions = {
            "It is within the registered pharmacy technician's (RPhT) scope of practice to collect medical and medication history. This history will inform the pharmacist's assessment and clinical recommendations. While the RPhT can highlight available OTC product options, they cannot recommend one clinically for a specific patient. Once the pharmacist (RPh) has completed their assessment of the patient, they may recommend an OTC product or refer the patient for further evaluation (e.g., by a physician, nurse practitioner).",
            "The pharmacy technician, Sayed, has already started gathering information on Bob's symptoms and they have reviewed Bob's prescription medication profile together. The pharmacist, Damilola, can now proceed with gathering additional information (as needed) to assess Bob and make a clinical recommendation.",
            "According to the scope of practice, a RPhT cannot make clinical recommendations.",
            "According to the scope of practice, a RPhT can perform tasks related to information gathering and triage, perform a technical (but NOT clinical) check of a prescription, and educate a patient on the proper use of a medical device (such as a blood pressure monitor). It is within a pharmacist's scope of practice to perform both a clinical verification and a technical check for product release to the patient."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }

    public string GetChoice1Text(int index){
        string[] Choice1Texts = {
        "He selects an over the counter (OTC) product for Bob to treat his cold symptoms.", 
        "She asks Bob what symptoms he is experiencing.", 
        "She refers Bob back to his primary healthcare provider.", 
        "Perform a clinical check of a refill prescription."};
        if (index >= 0 && index < Choice1Texts.Length)
            return Choice1Texts[index];
        else
            return "";
    }
    public string GetChoice2Text(int index){
        string[] Choice2Texts = {
        "He asks the pharmacist to assess Bob and make a recommendation.", 
        "She gathers more information, based on what Sayed has already collected, to assess Bob and make a clinical recommendation.", 
        "She asks Sayed to make a clinical recommendation based on the information collected.", 
        "Perform a technical check of a refill prescription."};
        if (index >= 0 && index < Choice2Texts.Length)
            return Choice2Texts[index];
        else
            return "";
    }
    public string GetChoice3Text(int index){
        string[] Choice3Texts = {
        "He collects more information and then asks the pharmacist to assess Bob and make a recommendation.", 
        "She helps Bob pick an OTC product, counsels him on the product, and 'cashes him out.'", 
        "She makes a clinical recommendation based on the information collected.", 
        "Educate a patient on the use of a medical device."};
        if (index >= 0 && index < Choice3Texts.Length)
            return Choice3Texts[index];
        else
            return "";
    }

    public string GetQuestionText(int index)
    {
        string[] questionTexts = {
        "Reflecting on the scenario you just completed, please identify the effective patient-pharmacy professional interactions in the scenario. Select all that apply.", 
        "Which of the following tasks fall within the scope of both pharmacy technicians and pharmacists? Select all that apply.", 
        "What was effective in Sayed and Damilola's interactions? Select all that apply.", 
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are bound to uphold the Ontario College of Pharmacists Code of Ethics."};
        if (index >= 0 && index < questionTexts.Length)
            return questionTexts[index];
        else
            return "";
    }

    // Video control UI elements 

    public void SkipVideo(){
        Videos[currentVideoIndex].Stop();
        LoadFactPanel(null);
    }

    public void SpeedClicked(){
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(true);
    }

    public void SpeedUnclicked(){
        SpeedButton.SetActive(true);
        SpeedButtonPressed.SetActive(false);
    }
}
