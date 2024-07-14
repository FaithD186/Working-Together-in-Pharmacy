using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

/*

This script plays the desired sequence of animation. Methods are attached to GameObjects in the scene
that, upon pressing, plays the next sequence of animation. 

*/
public class SequenceStarterScript : MonoBehaviour
{
    public VideoPlayer[] Videos;
    public RawImage[] RawImages;
    private int currentVideoIndex = 0;
    public string[] sequenceNames; // sequence names are assigned in the inspector

    private void Start()
    {
        // Disable all raw images initially
        foreach (RawImage rawImage in RawImages)
        {
            rawImage.enabled = false;
        }
    }
    private void PlayVideo(int videoIndex, string videoFileName)
    {
        Videos[videoIndex].url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Videos[videoIndex].Play();
    }
    private void ShowRawImage(int rawImageIndex)
    {
        RawImages[rawImageIndex].enabled = true;
    }

    private void HideRawImage(int rawImageIndex)
    {
        RawImages[rawImageIndex].enabled = false;
    }
    public void StartSeq(){
        if (currentVideoIndex > 0){
            HideRawImage(currentVideoIndex - 1);
        }
        if (currentVideoIndex <= Videos.Length - 1){
            ShowRawImage(currentVideoIndex);
            PlayVideo(currentVideoIndex, sequenceNames[currentVideoIndex]);
            currentVideoIndex++;
        }
    }
}
