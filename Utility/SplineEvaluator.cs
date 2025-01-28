using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class SplineEvaluator : MonoBehaviour
{
    public SplineContainer splineContainer;

    public float speed = 1.0f;

    public bool patrol = false;

    public bool reverse = false;

    public UnityEvent onComplete;

    public Action onComplete_internal;

    private bool isActive = false;

    private float time = 0.0f;

    private float duration = 0.0f;

    private float scaledDuration = 0.0f;

    private float direction = 1.0f;

    private void Start()
    {
        duration = splineContainer.CalculateLength();

        scaledDuration = duration / duration;

        Activate();

        if (reverse)
        {
            time = scaledDuration;
            FlipDirection();
        }
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (splineContainer.Spline != null)
        {
            time += (speed * Time.deltaTime / duration) * direction;

            time = Mathf.Clamp(time, 0.0f, scaledDuration);

            transform.position = splineContainer.EvaluatePosition(time);

            if (patrol)
            {
                if (time >= scaledDuration ||
                    time <= 0.0f)
                {
                    FlipDirection();
                }
            }
            else if (time >= scaledDuration)
            {
                onComplete?.Invoke();
                onComplete_internal?.Invoke();
                Deactivate();
            }
        }
    }

    private void FlipDirection()
    {
        direction *= -1.0f;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
