using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FloorSoundMaterial { Metallic }
public class FloorSoundManager : MonoBehaviour
{
    [SerializeField] private FloorSoundMaterial type;
    [SerializeField] private AudioClip[] metalicFootstepSoundsEffect;
    [SerializeField] private AudioSource _audioSource;
    
    private void OnTriggerStay(Collider other)
    {
        var s = other.gameObject.GetComponent<ISoundable>();
        
        if (s != null)
        {
            if (s.ShouldPlaySound() && !_audioSource.isPlaying)
            {
                _audioSource.Stop();
                _audioSource.clip = metalicFootstepSoundsEffect[Random.Range(0, metalicFootstepSoundsEffect.Length)];
                _audioSource.Play();
            }
        }
    }
}
