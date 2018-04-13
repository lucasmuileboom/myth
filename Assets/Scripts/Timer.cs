using UnityEngine;

public class Timer
{
    private float _duration, _startTime;

    public Timer(float n)
    {
        _duration = n;
        _startTime = Time.time;
    }

    public bool Done()
    {
        return (Time.time >= _startTime + _duration);
    }

    public float GetSpentTime()
    {
        return Time.time - _startTime;
    }
}