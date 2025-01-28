using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineHitboxExample : MonoBehaviour
{
    public SplineEvaluator splineEvaluator;

    void Start()
    {
        splineEvaluator.Activate();
    }

}
