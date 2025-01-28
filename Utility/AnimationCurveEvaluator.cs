using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCurveEvaluator : MonoBehaviour
{
    public enum PostionAxis 
    { 
        X,
        Y,
        Z
    };

    public PostionAxis axis;

    public AnimationCurve curve;

    public float duration;

    private Timer timer;

    private bool isActive = false;

    private Vector3 cachedPosition;

    public UnityEvent onComplete;

    public Action onComplete_Internal;

    private void Start()
    {
        Activate();
    }

    private void Update()
    {
        if (isActive)
        {
            float normalizedTime = timer.CurrentTime / timer.duration;

            float curveValue = curve.Evaluate(normalizedTime);

            Vector3 newPos = cachedPosition;

            switch (axis)
            {
                case PostionAxis.X:
                    newPos.x += curveValue;
                    break;
                case PostionAxis.Y:
                    newPos.y += curveValue;
                    break;
                case PostionAxis.Z:
                    newPos.z += curveValue;
                    break;
            }

            transform.position = newPos;
        }
    }

    public void Activate()
    {
        isActive = true;

        cachedPosition = transform.position;

        timer = gameObject.AddComponent<Timer>();
        timer.endAction = Timer.EndAction.DestroyTimer;
        timer.duration = duration;
        timer.onComplete_Internal += InvokeComplete;
        timer.StartTimer();
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void InvokeComplete()
    {
        Deactivate();

        onComplete?.Invoke();
        onComplete_Internal?.Invoke();
    }
}
