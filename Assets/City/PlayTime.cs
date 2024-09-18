using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayTime : MonoBehaviour
{
    private static PlayTime instance;
    public static PlayTime Instance { get 
        {
            if (instance == null)
                return GameObject.FindFirstObjectByType<PlayTime>();

            return instance; 
        }
        private set 
        { 
            instance = value;
        } 
    }

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

    //private void Update()
    //{
    //    Debug.Log("current time : "+PlayTime_ms);
    //}

    public void Initialize(int sessionStart_ms)
    {
        this.sessionStart_ms = sessionStart_ms;
        Counting = true;
    }
}
