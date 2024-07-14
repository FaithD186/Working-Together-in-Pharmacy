using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class Case3_Eventscript : MonoBehaviour
{
    // Panel GameObjects (i.e. different "screens")
    public GameObject PatientProfile;
    public GameObject EndPanel;
    public GameObject FactPanel;
    public GameObject FactPanel2;
    public GameObject Card;
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

    // Video playback speed
    public GameObject SpeedButton;
    public GameObject SpeedButtonPressed;

    public GameObject ContinueinScene;

    // Screens
    public GameObject Canvas;
    public GameObject ComputerScreen;
    public GameObject ComputerScreen2;
    // public GameObject ConsentForm;
    // public GameObject ConsentForm2;

    // Refers to quizzes within scenarios
    private int currentQuizNum = 0;
    private string playerChoice;
    public TextMeshProUGUI QuizQuestionText;
    public TextMeshProUGUI QuizExplanationText;
    public TextMeshProUGUI Choice1Text;
    public TextMeshProUGUI Choice2Text;
    public TextMeshProUGUI Choice3Text;
    public string[] QuizCorrectAnswers;

    private SequenceStarterScript sequenceStarter;
    private VideoController videoController;
    
    public GameObject Card1;
    public GameObject Card2;

    void Start()
    {   PatientProfile.SetActive(true);
        FactPanel.SetActive(false);
        FactPanel2.SetActive(false);
        Reflection.SetActive(false);
        EndPanel.SetActive(false);
        Card.SetActive(false);
        ComputerScreen.SetActive(false);
        ComputerScreen2.SetActive(false);
        // ConsentForm.SetActive(false);
        // ConsentForm2.SetActive(false);

        Card1.SetActive(true);
        Card2.SetActive(false);

        ContinueinScene.SetActive(false);


        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);

        QuestionPanel_Scenario.SetActive(false);

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

        if (currentVideoIndex >= Videos.Length){
            return;
        }

        Videos[currentVideoIndex].loopPointReached += LoadFactPanel;
    }

    public void LoadFactPanel(VideoPlayer vp){  
        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);
        if (currentVideoIndex == 0 || currentVideoIndex == 1 || currentVideoIndex == 7){
            sequenceStarter.StartSeq();
            videoController.ContinueClicked();
            ContinueClicked();
            StartVideo();
            return;
        }
        else if (currentVideoIndex == 2 || currentVideoIndex == 6 || currentVideoIndex == 8){
            Show_QuestionInScenario();
            return;
        }
        else if (currentVideoIndex == 3){
            ContinueinScene.SetActive(true);
            return;
        }
        else if (currentVideoIndex == 4){
            FactPanel.SetActive(true);
            return;
        }
        else if (currentVideoIndex == 5){
            FactPanel2.SetActive(true);
            return;
        }
    
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

    public void CloseClicked(){
        IncorrectPanel_Scenario.SetActive(false);
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
    
    // public void NextPageForm(){
    //     ConsentForm.SetActive(false);
    //     ConsentForm2.SetActive(true);
    // }

    public void ShowCard2(){
        Card1.SetActive(false);
        Card2.SetActive(true);
    }
    public void ShowCard1(){
        Card1.SetActive(true);
        Card2.SetActive(false);
    }

    public void ContinueClicked(){ // Continue in Fact Panel
            FactPanel.SetActive(false);
            ComputerScreen.SetActive(false);
            ComputerScreen2.SetActive(false);
            FactPanel2.SetActive(false);
            // ConsentForm2.SetActive(false);
            Card.SetActive(false);

            QuestionPanel_Scenario.SetActive(false);
            CorrectPanel_Scenario.SetActive(false);
            IncorrectPanel_Scenario.SetActive(false);

            SkipButton.SetActive(true);
            SpeedButton.SetActive(true);
            PauseButton.SetActive(true);
            if (currentVideoIndex == 8){
                Reflection.SetActive(true);
            }
            currentVideoIndex++;
    }

    public void IncreaseQuizNum(){
        currentQuizNum++;
    }

    public void Show_QuestionInScenario(){
        QuestionPanel_Scenario.SetActive(true);
        IncorrectPanel_Scenario.SetActive(false);
        CorrectPanel_Scenario.SetActive(false);
        QuizQuestionText.text = GetQuizQuestionText(currentQuizNum);
        QuizExplanationText.text = GetQuizExplanationText(currentQuizNum);
        Choice1Text.text = GetChoice1Text(currentQuizNum);
        Choice2Text.text = GetChoice2Text(currentQuizNum);
        Choice3Text.text = GetChoice3Text(currentQuizNum);

    }

    public void ShowEndPanel(){
        EndPanel.SetActive(true);
    }

    // public void Show_ConsentForm(){
    //     ConsentForm.SetActive(true);
    // }

    public void Show_ComputerScreen2(){
        ContinueinScene.SetActive(false);
        ComputerScreen2.SetActive(true);
    }
    public void Show_Card(){
        Card.SetActive(true);
    }
    
    // Text events 

    public string GetQuizQuestionText(int index){
        string[] QuizQuestions = {
            "Under the Regulated Health Professions Act (RHPA), effective January 2021, registered pharmacy technicians are eligible to administer COVID-19 vaccines. With this exemption under the RHPA, to administer the vaccine, regulated pharmacy technicians must:",
            "Prior to withdrawing the COVID-19 vaccine doses, the registered pharmacist or pharmacy technician must do all of the following, EXCEPT:",
            "The pharmacist introduces Maryam to the CARD™ system to provide her with a more positive vaccination experience. All of the following are examples of CARD™ strategies that Maryam could use, EXCEPT:",
            "What do you think is a key component for the administration of the vaccine?",
            "With respect to post injection patient education, a pharmacy technician may educate about the following EXCEPT:"
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }
    public string GetQuizExplanationText(int index){
        string[] QuizQuestions = {
            "The RPhT is expected to have sufficient knowledge, skill and judgment to safely and effectively administer the vaccine; ensure the patient has been assessed by the pharmacist; follow recommended infection prevention and control procedures; ensure that there are appropriate resources available to safely manage any adverse reactions; and document the administration of COVID-19 vaccine as required by the Ministry of Health and/or Public Health.",
            "The vaccine vial should be handled with care. Never shake the vial as this can damage/inactivate the vaccine.",
            "Moving while the healthcare professional is administering the injection could lead to injuries and serious complications (e.g., muscle damage, nerve damage, and even paralysis).",
            "Landmarking the site prior to administration is important to prevent pain and complications, including severe nerve or tissue damage. Although still used in practice, the World Health Organization (WHO) advise against using alcohol skin disinfection for administration of vaccinations. Massaging the site after an injection may increase the risk of adverse events from the vaccine or can cause the vaccine to back up through the subcutaneous tissue, so it should be avoided with intramuscular injections.",
            "Providing clinical recommendations is not part of a registered pharmacy technician's scope of practice. The technician can ask the pharmacist to speak with the patient to answer the question or provide written patient information about managing common side effects associated with post vaccination."
        };
        if (index >= 0 && index < QuizQuestions.Length)
            return QuizQuestions[index];
        else
            return "";
    }

    public string GetChoice1Text(int index){
        string[] Choice1Texts = {"Complete an approved injection training course.", 
        "Inspect the vial for cracks, leaks and particulate matter.", 
        "Listen to music and dance in her seat to distract herself while the pharmacist is injecting the vaccine.", 
        "Swabbing the injection site with alcohol",
        "The patient could take acetaminophen for pain."};
        if (index >= 0 && index < Choice1Texts.Length)
            return Choice1Texts[index];
        else
            return "";
    }
    public string GetChoice2Text(int index){
        string[] Choice2Texts = {"Discuss vaccine benefits and risks with the patient.", 
        "Shake the vial prior to withdrawing the dose.", 
        "Grab the milk chocolate covered almonds from her bag and have some before and after the injection for some comfort.", 
        "Landmarking of injection site",
        "The patient may incur pain at the site of injection."};
        if (index >= 0 && index < Choice2Texts.Length)
            return Choice2Texts[index];
        else
            return "";
    }
    public string GetChoice3Text(int index){
        string[] Choice3Texts = {"Obtain informed consent from the patient.", 
        "Disinfect the entry point/rubber stopper and allow it to dry.", 
        "Take slow deep breaths in and out to help her relax while the pharmacist is injecting the vaccine.", 
        "Massaging the injection site following administration",
        "The patient may incur some swelling at the site of injection."};
        if (index >= 0 && index < Choice3Texts.Length)
            return Choice3Texts[index];
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
