using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMan : MonoBehaviour
{
    public Slider audioSlider;
    public ChangeAudio currentAudio;
    
 
        /// <summary>
        /// change the volume of the current audio source that is playing based in the current slider value
        /// </summary>
    void Update()
    {
        currentAudio.getCurrentAudio().volume = audioSlider.value;
    }
}
