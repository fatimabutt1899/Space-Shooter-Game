using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosion;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Audio Source on Explosion is NULL");
        }
        else
        {
            _audioSource.clip = _explosion;
        }
        Destroy(this.gameObject,3f);
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
