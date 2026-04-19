using KalinKonta;
using System.Collections;
using TMPro;
using UnityEngine;

public class StoryTextScroller : MonoBehaviour
{
    [SerializeField] private RectTransform textRect;
    [SerializeField] private GameObject next1Button;
    [SerializeField] private GameObject next2Button;

    [Header("Scroll Settings")]
    [SerializeField] private float startDelay = 2f; 
    [SerializeField] private float scrollSpeed = 200f;
    [SerializeField] private float target1Top = -3800f;
    [SerializeField] private float target2Top = -4500f;

    [Header("Idel Settings")]
    [SerializeField] private float idleThreshold = 5f;
    private float idleTimer = 0f;

    private bool isScrolling = false;
    private bool isFinished = false;
    private int count = 0;

    void Start()
    {
        next1Button.SetActive(false);
        next2Button.SetActive(false);
        StartCoroutine(StartScrollSequence(target1Top, startDelay));
    }

    void Update()
    {
        if (next2Button.activeSelf)
        {
            bool hasInput = InputManager.Instance.WasClicked;

            if (hasInput) idleTimer = 0f;
            else if (!isScrolling && isFinished) idleTimer += Time.deltaTime;

            if (idleTimer >= idleThreshold && isFinished)
            {
                Debug.Log($"Looking my eyes!");
            }
        }

        if (InputManager.Instance.WasClicked && !isFinished)
        {
            SkipScroll();
        }
    }

    IEnumerator StartScrollSequence(float target, float delay)
    {
        isScrolling = true;
        yield return new WaitForSeconds(delay);

        float currentTop = -textRect.offsetMax.y;

        while (currentTop > target)
        {
            currentTop -= scrollSpeed * Time.deltaTime;

            SetTop(currentTop);

            yield return null;
        }

        FinishScroll();
    }

    private void SetTop(float topValue)
    {
        textRect.offsetMax = new Vector2(textRect.offsetMax.x, -topValue);
    }

    public void ContinueScroll()
    {
        next1Button.SetActive(false);
        next2Button.SetActive(false);
        isFinished = false;
        StartCoroutine(StartScrollSequence(target2Top, 0));
    }

    private void SkipScroll()
    {
        StopAllCoroutines();
        switch (count)
        {
            case 0:
                SetTop(target1Top);
                break;
            case 1:
                SetTop(target2Top);
                break;
        }
        FinishScroll();
    }

    private void FinishScroll()
    {
        isFinished = true;
        isScrolling = false;
        
        switch (count)
        {
            case 0:
                next1Button.SetActive(true);
                break;
            case 1:
                next2Button.SetActive(true);
                break;
        }

        count++;
    }
}