using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public static Action OnDialogComplete;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI nextLine;

    [SerializeField] List<string> lines = new List<string>();

    int currentLineIndex = -1;

    readonly int FADE_IN_ANIMATION = Animator.StringToHash("FadeInAnimation");
    readonly int TEXT_FADE_IN_ANIMATION = Animator.StringToHash("FadeInTextAnimation");
    readonly int TEXT_FADE_OUT_ANIMATION = Animator.StringToHash("FadeOutTextAnimation");

    bool canSkipText = false;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkipText)
        {
            if (currentLineIndex < lines.Count - 1)
            {
                PlayFadeOutAnimation();
            }
            else
            {
                OnDialogComplete?.Invoke();
            }
        }
    }

    void PlayDialogBoxFadeInAnimation()
    {
        animator.Play(FADE_IN_ANIMATION);
    }

    void PlayFadeInTextAnimation()
    {
        animator.Play(TEXT_FADE_IN_ANIMATION);
    }

    void PlayFadeOutAnimation()
    {
        canSkipText = false;
        nextLine.gameObject.SetActive(false);
        animator.Play(TEXT_FADE_OUT_ANIMATION);
    }

    void SetNewLine()
    {
        currentLineIndex++;
        dialogText.text = lines[currentLineIndex];
        PlayFadeInTextAnimation();
    }

    void NewLineLoaded()
    {
        canSkipText = true;
        nextLine.gameObject.SetActive(true);
    }
}
