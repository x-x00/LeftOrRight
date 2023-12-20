using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnMusicOnOff : MonoBehaviour {
    public Texture musicOn;
    public Texture musicOff;
    bool isMusicOn = true;

    public void MuteMusic() {
        if (isMusicOn) {
            FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = true;
            GetComponent<RawImage>().texture = musicOff;
            isMusicOn = false;
        }
        else {
            FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = false;
            GetComponent<RawImage>().texture = musicOn;
            isMusicOn = true;
        }
    }
}
