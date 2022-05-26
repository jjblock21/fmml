using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine;

namespace Main.EnvironmentObserver
{
    /*
     * EnviroSkyWeather
     * 1  Clear Sky
     * 2  Cloudy    1
     * 3  Cloudy    2
     * 4  Cloudy    3
     * 5  Cloudy    4
     * 6  Foggy         [Broken]
     * 7  LightRain
     * 8  HeavyRain
     * 9  LightSnow
     * 10 HeavySnow
     * 11 Storm
     */
    public class TimeManager
    {
        private EnviroSkyMgr skyManager;
        private bool isEnabled;

        /// <summary>
        /// Call this every time a new map is loaded.
        /// </summary>
        public void Setup()
        {
            if (!MapManager.IsPlayableMapLoaded())
            {
                isEnabled = false;
                return;
            }
            if (EnviroSkyMgr.instance == null)
            {
                Debug.LogError("Unexpected Error while setting up the TimeMgr.");
                isEnabled = false;
                return;
            }
            skyManager = EnviroSkyMgr.instance;
            isEnabled = true;
        }

        public void FreezeTime()
        {
            // TODO: set day night tool to specified time
            if (!isEnabled) return;
            skyManager.Time.cycleLengthInMinutes = GetTimePerCycleInMinutes(TimeSpeed.Frozen);
            skyManager.Time.ProgressTime = EnviroTime.TimeProgressMode.None;
        }

        public void SetTimeOfDay(int hours)
        {
            if (!isEnabled) return;
            skyManager.SetTimeOfDay(hours);
            FreezeTime();
        }

        public void ChangeWeather(Weather weather)
        {
            if (!isEnabled) return;
            skyManager.ChangeWeather((int)weather);
        }

        public void SetTimeOfDayPreset(TimeOfDay timeOfDay)
        {
            SetTimeOfDay((int)timeOfDay);
        }

        /// <summary>
        /// Converts a TimeSpeed enum to the time it takes for one cycle to finish.
        /// </summary>
        /// <param name="timeSpeed">The speed setting</param>
        /// <returns>The time per cycle in minutes.</returns>
        public float GetTimePerCycleInMinutes(TimeSpeed timeSpeed)
        {
            switch ((int)timeSpeed)
            {
                case 0: return 0f;
                case 1: return 120f;
                case 2: return 30f;
                case 3: return 15f;
                case 4: return 5f;
                case 5: return 1f;
                case 6: return 0.5f;
                case 7: return 0.2f;
                case -1: return 0.01f;
                default: return 120f;
            }
        }

        /// <summary>
        /// Is the TimeManager instance set up correctly.
        /// </summary>
        public bool IsEnabled { get { return isEnabled; } }
    }

    /// <summary>
    /// The speed time advances at.
    /// </summary>
    public enum TimeSpeed
    {
        Frozen = 0,
        Normal = 1,
        _30MinPerCycle = 2,
        _15MinPerCycle = 3,
        _5MinPerCycle = 4,
        Faster = 5,
        Fastest = 6,
        Warp = -1,
    }

    public enum TimeOfDay
    {
        Midnight = 0,
        Morning = 6,
        Noon = 12,
        Afternoon = 15,
        Evening = 20,
    }

    public enum Weather
    {
        Cloudy = 1,
        LightRain = 6,
        HeavyRain = 7,
        Clear = 0,
        Thunder = 10,
        LightSnow = 8
    }

    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
}