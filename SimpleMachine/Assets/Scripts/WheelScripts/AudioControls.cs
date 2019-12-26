using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControls : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip moveCar;
    private AudioClip brake;
    private AudioClip crash;
    private AudioClip cutAxle;
    private void Start()
    {
        audioSource = GameObject.Find("AudioManager")?.GetComponent<AudioSource>();
        moveCar = Resources.Load("Sounds/WheelSounds/movingCar", typeof(AudioClip)) as AudioClip;
        brake = Resources.Load("Sounds/WheelSounds/Braking", typeof(AudioClip)) as AudioClip;
        crash = Resources.Load("Sounds/WheelSounds/Crash", typeof(AudioClip)) as AudioClip;
        cutAxle = Resources.Load("Sounds/WheelSounds/cutAxle", typeof(AudioClip)) as AudioClip;
    }

    public void playAudios(string audioName)
    {
        switch (audioName)
        {
            case "forward":
                audioSource.clip = moveCar;
                break;
            case "backward":
                audioSource.clip = brake;
                break;
            case "crash":
                audioSource.clip = crash;
                break;
            case "cutAxle":
                audioSource.clip = cutAxle;
                break;
        }
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }


}
