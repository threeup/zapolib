
using UnityEngine;
[System.Serializable]
public class ManualTimerEvent
{
    public System.Action FinishedCallback = null;

    public System.Action<float> ProgressCallback = null;

    public float CountdownTime { get; set; }
    [field: SerializeField] public bool IsLooping { get; set; }

    [field: SerializeField] public bool IsCountingDown { get; private set; }

    [SerializeField] private float _countdownTimer;

    public ManualTimerEvent(float time, bool looping)
    {
        CountdownTime = time;
        IsLooping = looping;
        IsCountingDown = false;
    }
    public void Launch()
    {
        Reset();
        IsCountingDown = true;
    }
    public void Resume()
    {
        IsCountingDown = true;
    }
    public void Reset()
    {
        _countdownTimer = CountdownTime;
    }
    public void Stop()
    {
        _countdownTimer = -1.0f;
        IsCountingDown = false;
    }

    public float RemainingPercent()
    {
        return 1 - _countdownTimer / CountdownTime;
    }



    public bool TimerTick(float dt)
    {
        if (!IsCountingDown)
        {
            return false;
        }
        if (_countdownTimer < 0.0f)
        {
            ProgressCallback?.Invoke(0);
            return false;
        }
        _countdownTimer -= dt;
        if (_countdownTimer < 0.0f)
        {
            if (IsLooping)
            {
                Launch();
            }
            else
            {
                Stop();
            }
            FinishedCallback?.Invoke();
            ProgressCallback?.Invoke(1);
            return true;
        }
        ProgressCallback?.Invoke(RemainingPercent());
        return false;
    }

    public override string ToString()
    {
        return _countdownTimer.ToString("n2")+"/"+CountdownTime.ToString("n2");
    }
}
