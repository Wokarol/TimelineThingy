using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionElement : MonoBehaviour
{
    public abstract float Duration { get; }

    public virtual void Init(float startTime) { }
    public abstract void Evaluate(float progress);

#if UNITY_EDITOR
    /// <summary>
    /// Don't include base if you don't want to call Evalate in preview
    /// </summary>
    /// <param name="progress"></param>
    public virtual void PreviewEvaluate(float progress) => Evaluate(progress);

    public virtual void StartPreview(float startTime) => Init(startTime);
    public virtual void EndPreview() { } 
#endif
}
