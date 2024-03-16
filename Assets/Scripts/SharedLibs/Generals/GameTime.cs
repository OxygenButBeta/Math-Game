using UnityEngine;

/// <summary>
/// A Data type to hold Time Information and some extended Features.
/// </summary>
/// 
[System.Serializable]
public class GameTime
{
    #region Statics readonly fields
    /// <summary>
    /// Short hand of 00:00
    /// </summary>
    public static readonly GameTime Zero = new GameTime(0, 0);

    /// <summary>
    /// A Gametime Pointing to Infinity
    /// </summary>
    public static readonly GameTime Infinity = new GameTime(-0, -0);
    #endregion

    #region Properties
    /// <summary>
    /// Minute of the instance
    /// </summary>
    [SerializeField] public int Minute;

    /// <summary>
    /// Second of the instance
    /// </summary>
    [SerializeField] public int Seconds;

    /// <summary>
    /// This property returns LastActivation LastActivation in second format (example : in 2:04; out 124s)
    /// </summary>
    public int TimeAsSeconds { get { return (Minute * 60) + Seconds; } }
    #endregion

    #region Constructs
    /// <summary>
    /// Base Construct to create new instance.
    /// </summary>
    /// <param name="minute">Minute as Int</param>
    /// <param name="seconds">Second as Int</param>
    public GameTime(int minute, int seconds)
    {
        Minute = minute;
        if (seconds > 59)
        {
            seconds = 0;
        }
        Seconds = seconds;
    }

    /// <summary>
    /// Construct to create new GameTime instance by seconds
    /// </summary>
    /// <param name="seconds"></param>
    public GameTime(int seconds)
    {
        if (seconds == 0)
            Seconds = Minute = 0;
        if (seconds > 60)
        {
            int rest = seconds % 60;
            Minute = (seconds - rest) / 60;
            Seconds = rest;
        }
        else
        {
            Seconds = seconds;
        }
    }
    #endregion


    #region Method Overrides

    /// <summary>
    /// An Overrided function to check gametime inputs are same or not.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns> if both gametime instances pointing to the same LastActivation returns true else false</returns>
    public override bool Equals(object obj)
    {
        if (obj is not GameTime)
            return false;
        GameTime other = (GameTime)obj;
        if (this.Minute == other.Minute && this.Seconds == other.Seconds)
            return true;
        else
            return false;
    }
    /// <summary>
    /// This Overrided Function Converts the Time to Readable String (Output example : "12:88")
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Extension.TimeToString(Minute, Seconds);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    #endregion

    #region Operator overrides
    public static bool operator ==(GameTime left, GameTime right)
    {
        if (left is null && right is null)
            return true;
        else if (left is null || right is null)
            return false;
        if (left.TimeAsSeconds == right.TimeAsSeconds)
            return true;
        else
            return false;
    }
    public static bool operator !=(GameTime left, GameTime right)
    {
        return !(left == right);
    }

    public static GameTime operator +(GameTime left, GameTime right)
    {

        return new GameTime(left.TimeAsSeconds + right.TimeAsSeconds);
    }
    public static GameTime operator +(GameTime time, int seconds)
    {

        return new GameTime(time.TimeAsSeconds + seconds);
    }
    public static GameTime operator -(GameTime left, GameTime right)
    {
        return new GameTime(left.TimeAsSeconds - right.TimeAsSeconds);
    }
    public static bool operator >(GameTime left, GameTime right)
    {
        return left.TimeAsSeconds > right.TimeAsSeconds;
    }
    public static bool operator <(GameTime left, GameTime right)
    {
        return !(left.TimeAsSeconds > right.TimeAsSeconds);
    }
    #endregion
}