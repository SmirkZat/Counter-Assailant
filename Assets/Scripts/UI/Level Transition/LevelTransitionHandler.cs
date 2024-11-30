using System;
using UnityEngine;

public class LevelTransitionHandler : MonoBehaviour
{
    public static LevelTransitionHandler Instance;
    public static Action OnFadeInLevelComplete;
    public static Action OnFadeOutLevelComplete;

    readonly int FADE_IN_ANIMATION_HASH = Animator.StringToHash("FadeInAnimation");
    readonly int FADE_OUT_ANIMATION_HASH = Animator.StringToHash("FadeOutAnimation");

    [SerializeField] bool fadeInOnStart = false;

    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (fadeInOnStart)
        {
            PlayLevelFadeInAnimation();
        }
    }

    void PlayLevelFadeInAnimation()
    {
        animator.Play(FADE_IN_ANIMATION_HASH);
    }

    public void PlayLevelFadeOutAnimation()
    {
        animator.Play(FADE_OUT_ANIMATION_HASH);
    }


    void OnFadeIn()
    {
        OnFadeInLevelComplete?.Invoke();
    }

    void OnFadeOut()
    {
        OnFadeOutLevelComplete?.Invoke();
    }
}
