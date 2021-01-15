using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOverlapper : MonoBehaviour
{
    private bool _isLooping;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip intro, loop;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (_isLooping) return;
        
        if (source.time < intro.length) return;
        
        _isLooping = true;
        source.clip = loop;
        source.loop = true;
        source.Play();
    }
}
