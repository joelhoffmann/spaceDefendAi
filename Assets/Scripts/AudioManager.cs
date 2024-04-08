using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isMenu;

    [Header("----Audio Sources------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----Audio Files------")]
    public AudioClip backgroundMenu;
    public AudioClip backgroundGame;
    public AudioClip baseHit;
    public AudioClip shieldDistruction;
    public AudioClip shopOpen;

    private void Start()
    {
        if (isMenu)
        {
            musicSource.clip = backgroundMenu;
            musicSource.loop = true;
            musicSource.Play();

        }
        else
        {
            musicSource.clip = backgroundGame;
            musicSource.loop = true;
            musicSource.Play();

        }
    }

    public void PlaySFX(AudioClip clip) { 
        SFXSource.PlayOneShot(clip);
    }
}