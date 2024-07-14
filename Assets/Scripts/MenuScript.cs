using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject MenuButton;
    public GameObject AboutPopup;

    void Start(){
        MenuButton.SetActive(true);
        MenuPanel.SetActive(false);
        AboutPopup.SetActive(false);
    }
    
    // Menu Panel from Home Screen. Accessing Menu in cases is determined by Video player controls (VideoController)
    public void MenuClicked(){
        MenuPanel.SetActive(true);
    }
    public void CloseMenu(){
        MenuPanel.SetActive(false);
    }
    public void BacktoSplashScreen(){
        SceneManager.LoadScene("IntroScene");
    }
    // General Menu 
    public void AboutClicked(){
        AboutPopup.SetActive(true);
    }
    public void AboutClosed(){
        AboutPopup.SetActive(false);
    }
    // Cases
    public void BacktoHome(){
        SceneManager.LoadScene("Home");
    }
    public void ReplayScenario(){
        SceneManager.LoadScene("Case1_Start");
    }
    public void ReplayScenario2(){
        SceneManager.LoadScene("Case2_ECampus");
    }
    public void ReplayScenario3(){
        SceneManager.LoadScene("Case3_ECampus");
    }
    
}
