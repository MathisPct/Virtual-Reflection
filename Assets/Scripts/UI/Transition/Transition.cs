using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    private float durationFadeIn; //duration of fade in
    private float durationFadeOut; //duration of fade out

    [SerializeField] private Animator fader;

    private AnimationClip clip;

    public float DurationFadeIn { get => durationFadeIn; }

    public float DurationFadeOut { get => durationFadeOut; }

    void Awake()
    {
        fader = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateAnimClipTimes(); 
    }

    /// <summary>
    /// Animation de transition de transparent à noir
    /// </summary>
    public void FadeIn()
    {
        fader.Play("FadeIn", 0, 0.0f);
    }

    /// <summary>
    /// Animation de transition de noir à transparent
    /// </summary>
    public void FadeOut()
    {
        fader.Play("FadeOut", 0, 0.0f);
    }

    /// <summary>
    /// Cette méthode permet de définir automatiquement les durées des animations en les affectant aux attributs de classe
    /// </summary>
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = fader.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "FadeIn":
                    durationFadeIn = clip.length;
                    break;
                case "FadeOut":
                    durationFadeOut = clip.length;
                    break;
            }
        }
    }
}
