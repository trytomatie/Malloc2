using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip startClip;
    public AudioClip loopClip;
    public AudioSource audio1;
    public AudioSource audio2;
    void Start()
    {
        audio1.clip = startClip;
        audio1.Play();
        audio2.clip = loopClip;
        audio2.PlayDelayed(startClip.length);
    }


}
