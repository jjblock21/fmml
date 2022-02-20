using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Main.EnvironmentObserver
{
    public class TimeMachine
    {
        private EnviroSkyMgr skyManager;
        private bool isEnabled;

        public void Setup()
        {
            if (!ModSceneManager.IsPlayableMapLoaded())
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
            skyManager.Time.cycleLengthInMinutes = GetTimePerCycleInMinutes(TimeSpeed.Frozen);
            EnviroSkyMgr.instance.Time.ProgressTime = EnviroTime.TimeProgressMode.None;
        }

        public void SetTimeSpeed(TimeSpeed timeSpeed)
        {
            skyManager.Time.cycleLengthInMinutes = GetTimePerCycleInMinutes(timeSpeed);
            EnviroSkyMgr.instance.Time.ProgressTime = EnviroTime.TimeProgressMode.Simulated;
        }

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

        public bool IsEnabled { get { return isEnabled; } }
    }

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
}