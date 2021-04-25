using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    [SerializeField] private AudioSource backRoundMusic;
    [SerializeField] private AudioSource bossMusic;
    private AudioSource currentAudio;
    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    private void Start()
    {
        currentAudio = backRoundMusic;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.activeSelf)
        {
            ChangeAudioSource(bossMusic);
            backRoundMusic.enabled = false;

        }
        else {
            ChangeAudioSource(backRoundMusic);
            

        }
    }
    void ChangeAudioSource(AudioSource audio) {
        if (currentAudio == audio) {
            return;

        }
        currentAudio.Stop();
        currentAudio = audio;
        currentAudio.Play();

    }
    public AudioSource getCurrentAudio() {

        return currentAudio;

    }
}


 