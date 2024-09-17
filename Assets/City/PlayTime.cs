using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayTime : MonoBehaviour
{
    public static PlayTime Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    private int sessionStart_ms;

    public int PlayTime_ms
    {
        get
        {
            return this.sessionStart_ms + (int)stopwatch.ElapsedMilliseconds;
        }
    }

    private bool counting;
    public bool Counting
    {
        get => counting;
        set
        {
            if(value && !counting)
            {
                ResumeCounting();
            }
            else if (!value && counting)
            {
                StopCounting();
            }
            counting = value;
        }
    }

    private void StopCounting()
    {
        stopwatch.Stop();
    }

    private void ResumeCounting()
    {
        stopwatch.Start();
    }

    public void Initialize(int sessionStart_ms)
    {
        this.sessionStart_ms = sessionStart_ms;
        Counting = true;
    }
}
