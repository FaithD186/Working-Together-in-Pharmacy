using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class EventScript_Case3 : MonoBehaviour
{
        
    // Panel GameObjects (i.e. different "screens")
    public GameObject PatientProfile;
    public GameObject MultipleQuestionPanel; // Question with multiple choices
    public GameObject TrueFalseQuestionPanel; // T/F Question
    public GameObject EndPanel;
    public GameObject KnowledgeCheck;
    public GameObject FactPanel;

    public GameObject MenuButton;
    public GameObject MenuPanel;

    // Video player control GameObjects
    public VideoPlayer[] Videos;
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;

    // Texts
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI ExplanationText;
    public TextMeshProUGUI FactText;

    // Variable Trackers
    private int currentVideoIndex = 0;
    private int currentQuestionIndex = 0;
    

    void Start()
    {   PatientProfile.SetActive(true);
        FactPanel.SetActive(false);
        MultipleQuestionPanel.SetActive(false);
        TrueFalseQuestionPanel.SetActive(false);
        KnowledgeCheck.SetActive(false);
        EndPanel.SetActive(false);

        SkipButton.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);      
    }

    public void StartVideo()
    {
        PatientProfile.SetActive(false);

        MenuButton.SetActive(true);
        SkipButton.SetActive(true);
        PauseButton.SetActive(true);

        Videos[currentVideoIndex].loopPointReached += LoadFactPanel;
    }

    public void LoadFactPanel(VideoPlayer vp){
        SkipButton.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);
        if (currentVideoIndex >= Videos.Length || currentVideoIndex < 0){
            return;
        }
        else{
            FactPanel.SetActive(true);
            FactText.text = GetFactText(currentVideoIndex);
        }

    }

    public void ContinueClicked(){ // Continue in Fact Panel
            FactPanel.SetActive(false);
            if (currentVideoIndex == Videos.Length - 1){ // last video
                KnowledgeCheck.SetActive(true);
            }else{
                SkipButton.SetActive(true);
                PauseButton.SetActive(true);
                currentVideoIndex++;
            }
    }

    public void StartKnowledgeCheck(){
        Videos[currentVideoIndex].Stop();
        KnowledgeCheck.SetActive(false);
        MultipleQuestionPanel.SetActive(true);
        currentQuestionIndex++;
    }

    public void QuestionContinueClicked(){ // Continue in Knowledge Check (question panel)
        if (currentQuestionIndex == 4){ // last question panel
            TrueFalseQuestionPanel.SetActive(false);
            EndPanel.SetActive(true);
        }else{
            TrueFalseQuestionPanel.SetActive(true);
            QuestionText.text = GetQuestionText(currentQuestionIndex);
            ExplanationText.text = GetExplanationText(currentQuestionIndex);
            currentQuestionIndex++;
        }
        
    }
    
    // Text events 

    public string GetFactText(int index){
        string[] FactTexts = {"It is within the Registered Pharmacy Technician’s (RPhT) scope of practice to perform the technical product check on a prescription, which includes checking the packaged contents of the prescription vial against the original drug bottle for accuracy.\n\nOnly Registered Pharmacists (RPh) can perform a clinical check of the medication, which includes the checking of drug interactions, allergies, and the appropriateness of the drug prescribed for the patient.",
        "The Registered Pharmacy Technician (RPhT) and Registered Pharmacist (RPh) do not need to contact the patient’s parents to obtain permission to proceed with his request given his age.\n\nHealth professionals must determine whether the patient is capable of making informed decisions regarding their health, and if determined to be a mature minor, the parents do not need to be contacted first. There is no stipulated age of consent for medical treatments in Ontario.",  
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) can administer COVID-19 and influenza vaccines after they complete an Ontario College of Pharmacists (OCP) approved injection training course and declare their training with OCP.\n\nRegistered Pharmacists (RPh) may also vaccinate patients for Hepatitis A, Hepatitis B, Herpes Zoster, Varicella, Human Papilloma Virus, Typhoid, Rabies, Japanese Encephalitis and Yellow Fever.", 
        "Personal health information can only be disclosed to whomever the patient identifies as being in their circle of care (in this example, the patient’s family doctor) unless the custodian of the health information (the Registered Pharmacist or Registered Pharmacy Technician) believes there is risk of harm to either the patient or someone else."};
        if (index >= 0 && index < FactTexts.Length)
            return FactTexts[index];
        else
            return "";
    }
    public string GetQuestionText(int index)
    {
        string[] questionTexts = { "None", 
        "The Registered Pharmacy Technician (RPhT) and Registered Pharmacist (RPh) do not need to contact the patient’s parents to obtain permission to proceed with his request given his age.", 
        "Registered Pharmacists (RPh) in Ontario may administer vaccines to eligible patients.", 
        "Personal health information can be disclosed to whomever the patient identifies as being in their circle of care."};
        if (index >= 0 && index < questionTexts.Length)
            return questionTexts[index];
        else
            return "";
    }

    public string GetExplanationText(int index)
    {
        string[] explanationTexts = { "None", 
        "There is no stipulated age of consent for medical treatments in Ontario.\n\nHealth professionals must determine whether the patient is capable of making informed decisions regarding their health, and if determined to be a mature minor, the parents do not need to be contacted first.", 
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) can administer COVID-19 and influenza vaccines after they complete an Ontario College of Pharmacists (OCP) approved injection training course and declare their training with OCP.\n\nRegistered Pharmacists (RPh) may also vaccinate patients for Hepatitis A, Hepatitis B, Herpes Zoster, Varicella, Human Papilloma Virus, Typhoid, Rabies, Japanese Encephalitis and Yellow Fever.", 
        "Personal health information can only be disclosed to whomever the patient identifies as being in their circle of care (in this example, the patient’s family doctor) unless the custodian of the health information (the pharmacist or pharmacy technician) believes there is risk of harm to either the patient or someone else."};
        if (index >= 0 && index < explanationTexts.Length)
            return explanationTexts[index];
        else
            return "";
    }

    public void SkipVideo(){
        Videos[currentVideoIndex].Stop();
        LoadFactPanel(null);
    }
}
