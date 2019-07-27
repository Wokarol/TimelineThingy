using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionElement : MonoBehaviour
{
    public bool IsActive { get; private set; } = false;

    /// <summary>
    /// Duration of element is seconds
    /// </summary>
    public abstract float Duration { get; }

    /// <summary>
    /// Called when Element is enabled
    /// </summary>
    public virtual void Init() { }

    /// <summary>
    /// Evaluates element based on time
    /// </summary>
    /// <param name="time">Time, in seconds</param>
    public abstract void Evaluate(float time);

    /// <summary>
    /// Determines if element should be active
    /// </summary>
    public virtual void SetActive(bool state)
    {
        gameObject.SetActive(state);
        IsActive = state;
    }
}
