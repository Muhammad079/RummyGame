using System;

public static class DateTimeAdjuster
{
   public static DateTime StringIntoDateTime(string timeString)
    {
        DateTime time;
        time = DateTime.Parse(timeString);
        return time;
    }
    public static float HoursIntoSeconds(float hours)
    {
        return (hours * 3600);
    }
    public static float MinutesIntoSeconds(float minutes)
    {
        return (minutes * 60);
    }
    public static float SecondsIntoMinutes(float seconds)
    {
        return (seconds / 60);
    }
    public static float SecondsIntoHours(float seconds)
    {
        return (seconds / 3600);
    }
    public static float TimeElapsedInSec(DateTime lastTime)
    {
        var currentTime = DateTime.UtcNow;
        var timeElapsed = currentTime - lastTime;
        return (timeElapsed.Days * 86400 + timeElapsed.Hours * 3600 + timeElapsed.Minutes * 60 + timeElapsed.Seconds);
    }
    
    public static float TimeElapsedInSec(string lastTime)
    {
        var currentTime = DateTime.UtcNow;
        var time = DateTime.Parse(lastTime);
        var timeElapsed = currentTime - time;
        return (timeElapsed.Days * 86400 + timeElapsed.Hours * 3600 + timeElapsed.Minutes * 60 + timeElapsed.Seconds);
    }
}
