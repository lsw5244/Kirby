using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip audioJump;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySounds(string action){
        switch(action){
            case "JUMP":
                audioSource.clip = audioJump;
                break;
        }
        audioSource.Play();
    }
}
