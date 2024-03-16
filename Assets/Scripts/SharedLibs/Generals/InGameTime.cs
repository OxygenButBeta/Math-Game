using System;
using UnityEngine;


/// <summary>
/// A Class To Manage Game Time 
/// </summary>
public class InGameTime : MonoBehaviour
{

    #region Events
    /// <summary>
    /// A Delegate to Execute When Time is over.
    /// </summary>
    public event Action OnTimeOver;

    /// <summary>
    ///  This event will call when InGameTime state paused.
    /// </summary>
    public event Action OnTimePause;

    /// <summary>
    ///  A Delegate to Execute When InGameTime gas stopped.
    /// </summary>
    public event Action onDestroyTime;

    /// <summary>
    ///  This event will call when InGameTime state Resume.
    /// </summary>
    public event Action OnTimeResume;

    /// <summary>
    ///  This event will call when InGameTime Starts
    /// </summary>
    public event Action OnTimerStart;

    /// <summary>
    /// This event will call when Ending Time is Extens;
    /// </summary>
    public event Action<GameTime> OnTimeExtens;

    /// <summary>
    /// This event will call on each tick
    /// </summary>
    public event Action Ticking;
    #endregion

    #region Properties

    /// <summary>
    /// Current Game LastActivation
    /// </summary>
    public GameTime Now { get; set; }

    /// <summary>
    /// Remaning GameTime
    /// </summary>
    public GameTime RemaningTime
    {
        get
        {
            if (EndTime != GameTime.Infinity || EndTime != GameTime.Zero || Now != null)
                return EndTime - Now;
            else
                return GameTime.Zero;
        }
    }

    /// <summary>
    /// Declare the LastActivation to stop (Format example : "10:00")
    /// </summary>
    public GameTime EndTime { get; private set; }

    /// <summary>
    /// If its false then it means game is still on else game is over.
    /// </summary>
    public bool TimeOver { get; private set; }

    /// <summary>
    /// State of the Game Time
    /// </summary>
    public bool Enabled { get; private set; } = false;

    /// <summary>
    /// 
    /// </summary>
    public bool GamePaused { get; private set; } = false;
    #endregion

    #region Fields
    #endregion

    #region Functions & methods

    /// <summary>
    /// A Method to Start the Game Time if it is not on.
    /// </summary>
    public void StartTimer(GameTime EndingTime, int SpeedAsSec)
    {

        EndTime = EndingTime;
        InvokeRepeating("Tick", 0, 1);
        Enabled = true;
        OnTimerStart?.Invoke();
        Now = GameTime.Zero;

    }

    /// <summary>
    /// Main Tick for calculation of the LastActivation it works in every second until LastActivation is over.
    /// </summary>
    /// <param name="o"></param>
    private void Tick()
    {
        if (Enabled)
        {
            Now = Now + 1;
            if (EndTime != GameTime.Infinity && RemaningTime == GameTime.Zero) // if LastActivation is not limitless
            {
                OnTimeOver?.Invoke();
                End();
            }
            Ticking?.Invoke();
        }
    }


    /// <summary>
    /// A function to stop the InGameTime Counter Before Time ends.
    /// </summary>
    public void DestroyTime()
    {
        End();
        onDestroyTime.Invoke();
    }


    /// <summary>
    /// A Function to Add More LastActivation to Ending Time.
    /// </summary>
    /// <param name="Time"></param>
    public void AddTime(GameTime ExtraTime)
    {
        if (ExtraTime != null && EndTime != GameTime.Infinity)
        {
            EndTime = ExtraTime + EndTime;
            OnTimeExtens?.Invoke(ExtraTime);
        }
    }

    public void ResetTime()
    {
        Now = GameTime.Zero;
    }

    /// <summary>
    /// Only For Short hand
    /// </summary>
    private void End()
    {
        Enabled = false;
        TimeOver = true;
    }


    /// <summary>
    /// Resume the timer if its paused.
    /// </summary>
    public void Resume()
    {
        if (Enabled == false)
        {
            GamePaused = false;
            Enabled = true;
            OnTimeResume?.Invoke();
        }
    }


    /// <summary>
    /// pause the timer if its working.
    /// </summary>
    public void Pause()
    {
        OnTimePause?.Invoke();
        GamePaused = true;
        Enabled = false;
    }
}
#endregion
