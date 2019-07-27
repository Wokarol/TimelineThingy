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

    public override void Init()
    {
        // Adds random offset and resets element's state
        randomOffset = Random.Range(0, 360f);
        transform.localScale = Vector3.zero;
    }

    public override void Evaluate(float progress)
    {
        // Scales element according to curve and rotates it
        transform.localScale = scale * animationCurve.Evaluate(progress / duration);
        transform.localScale += Vector3.forward;
        transform.localRotation = Quaternion.Euler(0, 0, randomOffset + (progress * 360f * rotationSpeed));
    }
}
