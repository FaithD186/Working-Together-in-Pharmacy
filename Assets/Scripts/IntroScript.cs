using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{

    public GameObject IntroScreen;
    public GameObject GetStarted;
    public GameObject ContinueButton;

    void Start()
    {
        IntroScreen.SetActive(false);
        GetStarted.SetActive(true);
        ContinueButton.SetActive(false);
    }
    public void StartGame(){
        GetStarted.SetActive(false);
        IntroScreen.SetActive(true);
        ContinueButton.SetActive(true);
    }
    public void Continue(){
        SceneManager.LoadScene("Home");
    }

}
