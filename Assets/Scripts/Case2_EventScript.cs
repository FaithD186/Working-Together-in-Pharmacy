using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class Case2_EventScript : MonoBehaviour
{
    // Panel GameObjects (i.e. different "screens")
    public GameObject PatientProfile;
    public GameObject EndPanel;
    public GameObject FactPanel;
    public GameObject Reflection;

    public GameObject MenuButton;
    public GameObject MenuPanel;

    // Video player control GameObjects
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;
    public VideoPlayer[] Videos;

    // Variable Trackers
    public int currentVideoIndex = 0;

    // Popups in scenario 
    public GameObject QuestionPanel_Scenario; 
    public GameObject CorrectPanel_Scenario;
    public GameObject IncorrectPanel_Scenario;
    public GameObject Continue;
    public GameObject Close;
    public string PlayerAnswer;
    public string[] CorrectAnswers;
    public GameObject SubmitButton;

    // Video playback speed
    public GameObject SpeedButton;
    public GameObject SpeedButtonPressed;

    // Screens
    public GameObject Canvas;
    public GameObject ComputerScreen;
    public GameObject ComputerScreen2;
    public GameObject Prescription;

    // True False questions
    public GameObject TrueFalsePanel;
    public GameObject TrueFalse_CorrectPanel;
    public GameObject TrueFalse_IncorrectPanel;
    public TextMeshProUGUI TFQuestionText;
    public TextMeshProUGUI TFExplanationText;

    // Refers to quizzes within scenarios
    private int currentQuizNum = 0;
    public TextMeshProUGUI QuizQuestionText;
    public TextMeshProUGUI QuizExplanationText;
    public TextMeshProUGUI Choice1Text;
    public TextMeshProUGUI Choice2Text;
    public TextMeshProUGUI Choice3Text;
    public string[] QuizCorrectAnswers;


    private string playerChoice;


    private SequenceStarterScript sequenceStarter;
    private VideoController videoController;

    //private int currentFactNum = 0;
    

    void Start()
    {   PatientProfile.SetActive(true);
        FactPanel.SetActive(false);
        Reflection.SetActive(false);
        EndPanel.SetActive(false);
        ComputerScreen.SetActive(false);
        ComputerScreen2.SetActive(false);
        Prescription.SetActive(false);

        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);

        QuestionPanel_Scenario.SetActive(false);
        TrueFalsePanel.SetActive(false);

        sequenceStarter = Canvas.GetComponent<SequenceStarterScript>();
        videoController = Canvas.GetComponent<VideoController>();    
    }

    public void StartVideo()
    {
        Debug.Log("Current video index is" + currentVideoIndex);
        PatientProfile.SetActive(false);

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
        if (currentVideoIndex == 0){
            ComputerScreen.SetActive(true);
            return;
        }
        else if (currentVideoIndex == 8){
            Reflection.SetActive(true);
            return;
        }
        else if (currentVideoIndex == 1 || currentVideoIndex == 7){
            ShowTrueFalsePanel();
            return;     
        }
        else if (currentVideoIndex == 2 || currentVideoIndex == 3){
            sequenceStarter.StartSeq();
            videoController.ContinueClicked();
            ContinueClicked();
            StartVideo();
            return;
        }
        else if (currentVideoIndex == 4 || currentVideoIndex == 5){
            Show_QuestionInScenario();
            return;
        }

        else if (currentVideoIndex == 6){
            FactPanel.SetActive(true);
            return;
        }

        // currentVideoIndex == 6: "What if" panel
    
        //if video out of bounds
        else{
            return;
        }

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
        Debug.Log("currentQuizNum" + currentQuizNum);
        Debug.Log("Correct answer" + CorrectAnswers[currentQuizNum]);
        if (PlayerAnswer != CorrectAnswers[currentQuizNum]){
            IncorrectPanel_Scenario.SetActive(true);
        }else{
            CorrectPanel_Scenario.SetActive(true);
            PlayerAnswer = "None";
        }
    }

    public void ChoiceClicked(){
        SubmitButton.SetActive(true);
    }

    public void CloseClicked(){
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
            Prescription.SetActive(false);

            QuestionPanel_Scenario.SetActive(false);
            CorrectPanel_Scenario.SetActive(false);
            IncorrectPanel_Scenario.SetActive(false);

            TrueFalsePanel.SetActive(false);
            TrueFalse_CorrectPanel.SetActive(false);
            TrueFalse_IncorrectPanel.SetActive(false);


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
        QuizQuestionText.text = GetQuizQuestionText(currentQuizNum);
        QuizExplanationText.text = GetQuizExplanationText(currentQuizNum);
        Choice1Text.text = GetChoice1Text(currentQuizNum);
        Choice2Text.text = GetChoice2Text(currentQuizNum);
        Choice3Text.text = GetChoice3Text(currentQuizNum);
    }

    public void ShowTrueFalsePanel(){
        TrueFalsePanel.SetActive(true);
        TrueFalse_IncorrectPanel.SetActive(false);
        TrueFalse_CorrectPanel.SetActive(false);
        TFQuestionText.text = GetQuizQuestionText(currentQuizNum);
        TFExplanationText.text = GetQuizExplanationText(currentQuizNum);
    }

    public void ShowEndPanel(){
        EndPanel.SetActive(true);
    }

    public void CheckAnswer(){
        if (currentQuizNum == 0){
            if (playerChoice == "True"){
                TrueFalse_CorrectPanel.SetActive(true);
            } else{
                TrueFalse_IncorrectPanel.SetActive(true);
            }
        }else if (currentQuizNum == 4){
            if (playerChoice == "False"){
                TrueFalse_CorrectPanel.SetActive(true);
            } else{
                TrueFalse_IncorrectPanel.SetActive(true);
            }
        }
    }
    public void closeClicked(){
        TrueFalse_IncorrectPanel.SetActive(false);
        playerChoice = "None";
    }
    public void TrueClicked(){
        playerChoice = "True";
        CheckAnswer();
    }
    public void FalseClicked(){
        playerChoice = "False";
        CheckAnswer();
    }

    public void Show_Prescription(){
        Prescription.SetActive(true);
    }
    
    // Text events 

    public string GetQuizQuestionText(int index){
        string[] QuizQuestions = {
            "In the scenario, both the pharmacist and the registered pharmacy technician are qualified to contact the prescriber to obtain the missing information on the prescription.",
            "The pharmacist may adapt a prescription for all the following EXCEPT:",
            "What would have been the most appropriate course of action regarding the presence of the coffee cup on the designated compounding counter?",
            "The main objective of documenting errors and near misses in a medical safety system is to:",
            "Mr. Patel's second question is about sunscreen application in relation to Nikhil's newly prescribed cream. It is within the regulated pharmacy technician's scope of practice to respond to this question."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }
    public string GetQuizExplanationText(int index){
        string[] QuizQuestions = {
            "Both pharmacists and technicians can contact prescribers (e.g., doctors/nurse practitioners/dentists) to clarify any prescription discrepancies and make note of any changes to the prescription or accept a new prescription verbally over the phone.",
            "The RPh may adapt a prescription based upon the individual circumstances of the patient by altering the dose, dosage form, regimen or route of administration to address the patient’s unique needs and circumstances.",
            "RPhT has responsibility to educate team members regarding compounding standards.",
            "AIMS (Assurance and Improvement in Medication Safety) is a mandatory medication safety program that enables practitioners to learn from medication events, better understand why they happened, and how they can be prevented.",
            "Therapeutic information is part of a pharmacist's scope of practice. Technical information, as in the question regarding product stability, is part of the registered pharmacy technician's scope of practice."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }

    public string GetChoice1Text(int index){
        string[] Choice1Texts = {"He selects an over the counter (OTC) product for Bob to treat his cold symptoms.", 
        "Dose", 
        "The RPhT should have said nothing and discarded the cup and its contents.", 
        "Identify and keep a record of pharmacy staff who have made errors."};
        if (index >= 0 && index < Choice1Texts.Length)
            return Choice1Texts[index];
        else
            return "";
    }
    public string GetChoice2Text(int index){
        string[] Choice2Texts = {"He asks the pharmacist to assess Bob and make a recommendation.", 
        "Therapeutic equivalent", 
        "The RPhT should have asked the pharmacy manager to speak to the relief pharmacist about leaving a coffee cup in the compounding area.", 
        "Share information and create action plans to improve medication safety."};
        if (index >= 0 && index < Choice2Texts.Length)
            return Choice2Texts[index];
        else
            return "";
    }
    public string GetChoice3Text(int index){
        string[] Choice3Texts = {"He collects more information and then asks the pharmacist to assess Bob and make a recommendation.", 
        "Route of administration", 
        "The RPhT did a good job informing the relief pharmacist that no food or drink is allowed in the compounding area.", 
        "Ensure there is pharmacy documentation in case of future litigation by patients."};
        if (index >= 0 && index < Choice3Texts.Length)
            return Choice3Texts[index];
        else
            return "";
    }

    public string GetQuestionText(int index)
    {
        string[] questionTexts = { "Reflecting on the scenario you just completed, please identify the effective patient-pharmacy professional interactions in the scenario. Select all that apply.", 
        "Which of the following tasks fall within the scope of both pharmacy technicians and pharmacists? Select all that apply.", 
        "What was effective in Sayed and Damilola's interactions? Select all that apply.", 
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are bound to uphold the Ontario College of Pharmacists Code of Ethics."};
        if (index >= 0 && index < questionTexts.Length)
            return questionTexts[index];
        else
            return "";
    }

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
