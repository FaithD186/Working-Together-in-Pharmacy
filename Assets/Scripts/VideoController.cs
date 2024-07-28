using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*

This script determines video player controls for the current video being played, in 
particular pausing/playing video. 


*/

public class VideoController : MonoBehaviour
{

    public VideoPlayer[] Videos;
    private int currentVideoIndex = 0;
    private long lastFrame;
   
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;
    public GameObject SpeedButton;
    public GameObject SpeedButtonPressed;

    public GameObject FactPanel;
    public GameObject MenuPanel;
    public GameObject QuestionPanel_Scenario; 
    public GameObject PatientProfile;

    public GameObject ComputerScreen;
    public GameObject ComputerScreen2;
    public GameObject Prescription;
    public GameObject ConsentForm2;
    public GameObject Reflection;

    public GameObject TrueFalsePanel;

    public GameObject FactPanel2;
    public GameObject Card;

    public void MenuClicked(){
        MenuPanel.SetActive(true);
        if (currentVideoIndex <= Videos.Length - 1){
            Time.timeScale = 0;
            Videos[currentVideoIndex].Pause();
            SkipButton.SetActive(false);
            SpeedButton.SetActive(false);
            SpeedButtonPressed.SetActive(false);
            PauseButton.SetActive(false);
            PlayButton.SetActive(false);
        }
    }

    public void CloseMenu(){
        if (FactPanel.activeSelf || PatientProfile.activeSelf 
        || QuestionPanel_Scenario.activeSelf || ComputerScreen.activeSelf 
        || ComputerScreen2.activeSelf || Prescription.activeSelf || TrueFalsePanel.activeSelf || Reflection.activeSelf
        || ConsentForm2.activeSelf || FactPanel2.activeSelf || Card.activeSelf){ 
            
            MenuPanel.SetActive(false);

        }
        else{
            // If menu is accessed during video playback
            MenuPanel.SetActive(false);
            Time.timeScale = 1;
            Videos[currentVideoIndex].Play();
            SkipButton.SetActive(true);

            if (Videos[currentVideoIndex].playbackSpeed == 1.0f){
                SpeedButton.SetActive(true);
            }
            else{
                SpeedButtonPressed.SetActive(true);
            }
            
            PauseButton.SetActive(true);
            PlayButton.SetActive(false);
        }
    }

    public void PauseClicked(){
        Time.timeScale = 0;
        Videos[currentVideoIndex].Pause();
        SkipButton.SetActive(false);
        SpeedButton.SetActive(false);
        SpeedButtonPressed.SetActive(false);
        PlayButton.SetActive(true);
    }
    public void UnPauseClicked(){
        Time.timeScale = 1;
        Videos[currentVideoIndex].Play();
        SkipButton.SetActive(true);
        if (Videos[currentVideoIndex].playbackSpeed == 1.0f){
            SpeedButton.SetActive(true);
        }else{
            SpeedButtonPressed.SetActive(true);
        }
        PlayButton.SetActive(false);
    }
    public void ContinueClicked(){
        currentVideoIndex++;
    }
    public void ReplayScene(){
        // Set up game UI elements
        SkipButton.SetActive(true);
        SpeedButton.SetActive(true);
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);

        // Play video from the beginning
        Videos[currentVideoIndex].time = 0;
        Videos[currentVideoIndex].Play();

        // Manage screen displays
        FactPanel.SetActive(false);
        Card.SetActive(false);
        FactPanel2.SetActive(false);
        QuestionPanel_Scenario.SetActive(false);
        ComputerScreen.SetActive(false);
        ComputerScreen2.SetActive(false);
        Prescription.SetActive(false);
        ConsentForm2.SetActive(false);
        TrueFalsePanel.SetActive(false);

        if (Videos[currentVideoIndex].playbackSpeed == 1.0f){
                SpeedButton.SetActive(true);
        }
        else{
                SpeedButtonPressed.SetActive(true);
        }

    } 
    public void TogglePlaybackSpeed(){
        if (Videos[currentVideoIndex].playbackSpeed == 1.0f){
            Videos[currentVideoIndex].playbackSpeed = 1.5f;
        }
        else{
            Videos[currentVideoIndex].playbackSpeed = 1.0f;
        }
    } 
    
}

