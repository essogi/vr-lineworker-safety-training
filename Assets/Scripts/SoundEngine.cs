using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour
{

    private AudioSource audioSource;
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
        
    }

    public void on()
    {
        state = true;
    }
    public void off()
    {
        state = false;
    }
}
