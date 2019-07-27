using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timeline : ActionElement
{
    [System.Serializable]
    struct Element {
        public float StartTime;
        public ActionElement action;
    }

    // Inspector
    [SerializeField] float playbackSpeed = 1;
    [SerializeField] List<Element> elements = new List<Element>();

    float time = 0;
    float cachedDuration;
    public override float Duration => cachedDuration;

    // Mono logic
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        time += Time.deltaTime * playbackSpeed;

        if (time > Duration + 0.2f)
            time = 0;
        if(time < 0) {
            time = Duration + 0.2f;
        }

        Evaluate(time);
    }

    // Playable logic
    public override void Init()
    {
        cachedDuration = -1;

        foreach (var e in elements) {
            // Sets cached duration to biggest endTime of all playables
            cachedDuration = Mathf.Max(
                cachedDuration, 
                e.StartTime + e.action.Duration); // End time of element
        }
    }

    public override void Evaluate(float time)
    {
        foreach (var e in elements) {
            // Check is element should be active
            bool playableActive = time > e.StartTime && time < e.StartTime + e.action.Duration;
            e.action.SetActive(playableActive);

            if (!e.action.IsActive && playableActive) {
                // Element was just activated
                e.action.Init();
            }

            // If it should, evaluates it using time offsetted by startTime
            if (playableActive)
                e.action.Evaluate(time - e.StartTime);
        }
    }
}
