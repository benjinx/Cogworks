using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public enum StartTime
    {
        None,
        OnAwake,
        OnEnable,
        OnStart,
    }

    public enum EndAction
    {
        OnlyInvokeEvent,
        Disable,
        Destroy,
        DestroyTimer,
    }

    public StartTime startTime = StartTime.None;

    public EndAction endAction = EndAction.OnlyInvokeEvent;

    // Repeating isn't going to work with Disable/Destroy
    public bool repeating = false;

    public float duration;

    public string identifer = string.Empty;

    public UnityEvent onComplete;

    public Action onComplete_Internal;

    private float currentTime = 0.0f;

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }
    }

    private bool isActive = false;
    
    public bool IsActive {
        get
        {
            return isActive;
        }
    }

    private void Awake()
    {
        if (startTime == StartTime.OnAwake)
        {
            StartTimer();
        }
    }

    private void OnEnable()
    {
        if (startTime == StartTime.OnEnable)
        {
            StartTimer();
        }
    }

    private void OnDisable()
    {
        if (startTime == StartTime.OnEnable)
        {
            StopTimer();
        }
    }

    private void Start()
    {
        if (startTime == StartTime.OnStart)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        // Verify this timer is active
        if (!isActive)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime > duration)
        {
            isActive = false;
            currentTime = 0.0f;
            onComplete?.Invoke();
            onComplete_Internal?.Invoke();

            switch (endAction)
            {
                case EndAction.OnlyInvokeEvent:
                    if (repeating)
                    {
                        StartTimer();
                    }
                    break;
                case EndAction.Disable:
                    gameObject.SetActive(false);
                    break;
                case EndAction.Destroy:
                    Destroy(gameObject);
                    break;
                case EndAction.DestroyTimer:
                    Destroy(this);
                    break;
            }
        }
    }

    public void StopTimer()
    {
        isActive = false;
        currentTime = 0.0f;
    }

    public void StartTimer()
    {
        isActive = true;
        currentTime = 0.0f;
    }

    public void ResetTimer()
    {
        StopTimer();
        StartTimer();
    }
}
