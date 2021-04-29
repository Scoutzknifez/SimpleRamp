using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RandomPitchChanger : MonoBehaviour
{
    public AudioSource source;

    [Range(0, 1.25f)]
    public float min = .7f;
    [Range(.7f, 1.5f)]
    public float max = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        source.pitch = Random.Range(min, max);
    }
}
