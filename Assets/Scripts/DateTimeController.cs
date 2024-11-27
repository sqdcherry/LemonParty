using System;
using System.Globalization;
using UnityEngine;

public static class DateTimeController
{
    public static void SetDateTime(string key, DateTime value)
    {
        string convertedToString = value.ToString(format: "u", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(key, convertedToString);
    }

    public static DateTime GetDateTime(string key, DateTime defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, format: "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
            return defaultValue;
    }
}
