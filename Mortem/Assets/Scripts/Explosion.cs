using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;
    public AudioClip explosionClip;

    void Start()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        source.PlayOneShot(explosionClip, 0.35f);
        Destroy(gameObject, exp.main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
