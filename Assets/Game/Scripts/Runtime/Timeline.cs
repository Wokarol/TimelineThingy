using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timeline : ActionElement
{
    [System.Serializable]
    struct Element {
        public float StartTime;
        public ActionElement Action;
    }

    // Inspector
    [SerializeField] float playbackSpeed = 1;
    [SerializeField] bool autoUpdate = true;
    [SerializeField] List<Element> elements = new List<Element>();

    float time = 0;
    float cachedDuration;
    public override float Duration => cachedDuration;

    // Mono logic
    private void Start()
    {
        Init(true);
    }

    private void Update()
    {
        if (autoUpdate) {
            time += Time.deltaTime * playbackSpeed;

            if (time > Duration + 0.2f)
                time = 0;
            if (time < 0) {
                time = Duration + 0.2f;
            }

            Evaluate(time); 
        }
    }

    // Playable logic

    public override void Init(bool firstTime)
    {
        if (firstTime) {
            cachedDuration = -1;

            foreach (var e in elements) {
                e.Action.Init(true);

                // Sets cached duration to biggest endTime of all playables
                cachedDuration = Mathf.Max(
                    cachedDuration,
                    e.StartTime + e.Action.Duration); // End time of element
            }
            foreach (var e in elements) {
                e.Action.SetActive(false);
            } 
        }
    }

    public override void SetActive(bool state)
    {
        base.SetActive(state);
        foreach (var e in elements) {
            e.Action.SetActive(state);
        }
    }

    public override void Evaluate(float time)
    {
        foreach (var e in elements) {
            // Check is element should be active
            bool playableActive = time > e.StartTime && time < e.StartTime + e.Action.Duration;
            e.Action.SetActive(playableActive);

            if (!e.Action.IsActive && playableActive) {
                // Element was just activated
                e.Action.Init(false);
            }

            // If it should, evaluates it using time offsetted by startTime
            if (playableActive)
                e.Action.Evaluate(time - e.StartTime);
        }
    }
}
