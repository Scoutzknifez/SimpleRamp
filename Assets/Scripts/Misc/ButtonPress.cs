using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonPress : MonoBehaviour
{
    public Animator animator;
    public AudioSource source;

    private bool isPlaying = false;

    public void pressButton()
    {
        if (!isPlaying)
            StartCoroutine(playAnimationAndSound());
    }

    IEnumerator playAnimationAndSound()
    {
        isPlaying = true;

        if (animator.isActiveAndEnabled)
            animator.Play("ButtonPress", -1, 0f);
        else
            animator.enabled = true;

        source.Play();

        yield return new WaitForSeconds(source.clip.length);

        isPlaying = false;
    }
}
