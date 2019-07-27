using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timeline : ActionElement
{
    [System.Serializable]
    struct Playable {
        public float StartTime;
        public ActionElement element;
    }

    [SerializeField] List<Playable> playables = new List<Playable>();

    float cachedDuration;
    public override float Duration => cachedDuration;

    public override void Evaluate(float progress)
    {
        foreach (var playable in playables) {
            playable.element.Evaluate(progress);
        }
        //throw new System.NotImplementedException();
    }

    public override void PreviewEvaluate(float progress)
    {
        foreach (var playable in playables) {
            var time = progress * Duration;
            var localTime = time - playable.StartTime;
            localTime /= playable.element.Duration;
            localTime = Mathf.Clamp01(localTime);
            playable.element.PreviewEvaluate(localTime);
        }
    }

    // Preview
    public override void StartPreview(float startTime)
    {
        cachedDuration = -1;

        base.StartPreview(startTime);
        foreach (var playable in playables) {
            playable.element.StartPreview(playable.StartTime);
            if (playable.StartTime + playable.element.Duration > cachedDuration)
                cachedDuration = playable.StartTime + playable.element.Duration;
        }
    }

    public override void EndPreview()
    {
        base.EndPreview();
        foreach (var playable in playables) {
            playable.element.EndPreview();
        }
    }
}
