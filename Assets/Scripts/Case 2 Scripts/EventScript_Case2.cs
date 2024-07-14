using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

/*

This script controls how GameObjects react to various events in the Second Scenario (i.e. beginning of the scenario, 
and the end of videos, knowledge checks), including: how specific text elements (fact panels, question panels) react to 
events in the gameplay.

*/

public class EventScript_Case2 : MonoBehaviour
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
        if (currentQuestionIndex == 5){ // last question panel
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
        string[] FactTexts = {"It is important for Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) to introduce themselves and share their credentials at the start of any communication with a patient.\n\nSharing credentials can help patients verify that they are interacting with licensed professionals who have the necessary knowledge and training to provide accurate and reliable information.",
        "A Registered Pharmacy Technician (RPhT) can recommend non-pharmacologic therapy using the R.I.C.E acronym. R.I.C.E treatment for acute injury refers to:\n\n1. Rest.\n2. Ice the injured area.\n3. Compress the injured area to reduce swelling.\n4. Elevate the injured area above the heart to reduce swelling.",  
        "All members of the pharmacy staff can assist in increasing awareness of Indigenous Services Canada's Non-Insured Health Benefits (NIHB) coverage, among Indigenous patients and pharmacy professionals.\nRegistered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) have a vital role to play in removing barriers and improving access to safe, equitable, quality health care, especially for people from underserviced communities who are seeking help with pain management.", 
        "As a Registered First Nations person, Jordyn can access health care benefits at the pharmacy, such as access to over-the-counter and prescription medicine for minor ailments that are covered by NIHB when recommended or prescribed by a RPh, coverage for gauze/tape if prescribed by a nurse practitioner, and coverage for crutches if prescribed by a physician. \n\nHowever, NIHB does not cover access to physiotherapy."};
        if (index >= 0 && index < FactTexts.Length)
            return FactTexts[index];
        else
            return "";
    }
    public string GetQuestionText(int index)
    {
        string[] questionTexts = { "None", 
        "Registered Pharmacy Technicians (RPhT) can recommend non-pharmacological measures such as rest, ice, compression, and elevation to patients with acute injuries.", 
        "All members of the pharmacy staff can assist in increasing awareness of Indigenous Services Canada's Non-Insured Health Benefits (NIHB) coverage, both among Indigenous patients and pharmacy professionals.", 
        "As a Registered First Nations person, Jordyn can access health care benefits at the pharmacy, such as access to over-the-counter and prescription medicine for minor ailments, coverage for gauze and tape if prescribed by a nurse practitioner, and access to physiotherapy.",
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) play an important role in improving access to safe and equitable health care, especially for people from underserviced communities seeking help with pain management."};
        if (index >= 0 && index < questionTexts.Length)
            return questionTexts[index];
        else
            return "";
    }

    public string GetExplanationText(int index)
    {
        string[] explanationTexts = { "None", 
        "It is within a Registered Pharmacy Technician’s (RPhT) scope of practice to recommend some non-pharmacologic therapy using the acronym R.I.C.E. \n\nR.I.C.E treatment for acute injury refers to: \n1. Rest.\n2. Ice the injured area.\n3. Compress the injured area to reduce swelling.\n4. Elevate the injured area above the heart to reduce swelling.", 
        "All pharmacy staff members can contribute to raising awareness about the coverage provided by Indigenous Services Canada's Non-Insured Health Benefits (NIHB) program, among both Indigenous patients and other pharmacy professionals. \n\nThe NIHB program provides coverage for a range of health services and benefits to eligible individuals from Indigenous communities across Canada.", 
        "Eligible Indigenous peoples can access health care benefits at the pharmacy, including: \n1) Access to over-the-counter and prescription medicine that are covered by NIHB when recommended or prescribed by a RPh.\n2) Coverage for gauze/tape if prescribed by a nurse practitioner \n3) Coverage for crutches, and possibly other mobility equipment if prescribed by a physician.\n\nHowever, NIHB does not cover access to physiotherapy.",
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are uniquely positioned and have a role to play in removing barriers and improving access to timely, safe, equitable, quality health care, especially for people from underserviced communities who are seeking help with pain management." };
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
