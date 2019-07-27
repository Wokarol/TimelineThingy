using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopAndRotate : ActionElement
{
    [SerializeField] float duration = 2;
    [SerializeField] Vector2 scale = Vector2.one;
    [SerializeField] AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.4f, 1), new Keyframe(1, 0));
    [Space]
    [SerializeField] float rotationSpeed = 5;

    float randomOffset;

    public override float Duration => duration;

    public override void Init(float startTime)
    {
        randomOffset = Random.Range(0, 360f);
    }

    public override void Evaluate(float progress)
    {
        transform.localScale = scale * animationCurve.Evaluate(progress);
        transform.localScale += Vector3.forward;
        transform.localRotation = Quaternion.Euler(0, 0, randomOffset + (progress * 360f * rotationSpeed));
    }

    // Preview cache
#if UNITY_EDITOR
    Vector3 scalePreviewCache;
    Quaternion rotationPreviewCache;
    public override void StartPreview(float startTime)
    {
        base.StartPreview(startTime);
        scalePreviewCache = transform.localScale;
        rotationPreviewCache = transform.localRotation;
    }

    public override void EndPreview()
    {
        transform.localScale = scalePreviewCache;
        transform.localRotation = rotationPreviewCache;
    } 
#endif
}
