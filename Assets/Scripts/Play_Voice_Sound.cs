using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Voice_Sound : MonoBehaviour
{
    public AudioSource _as;
    public AudioClip _ac;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.PlayOneShot(_ac);
    }
    private void Update()
    {
        if(_as.isPlaying == false)
        {
            Destroy(this.gameObject);
        }
    }
}
