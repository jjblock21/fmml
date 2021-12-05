using FModApi;
using Injected;
using System.Reflection;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Main
{
    public static class FlyMode
    {
        public static bool flyModeActive = false;
        private static FirstPersonController flyModeControler = null;
        public static ToggleClass flyModeToggle = new ToggleClass();

        private static void EnableFlyMode()
        {
            FirstPersonController firstPerson = Object.FindObjectOfType<FirstPersonController>();
            SetPlayerGravity(0, firstPerson);
            SetSpeed(20, firstPerson);
            flyModeControler = firstPerson;
            flyModeActive = true;
        }

        private static void DisableFlyMode()
        {
            FirstPersonController firstPerson = Object.FindObjectOfType<FirstPersonController>();
            SetPlayerGravity(2, firstPerson);
            SetSpeed(10, firstPerson);
            flyModeControler = firstPerson;
            flyModeActive = false;
        }

        private static void SetPlayerGravity(int gravity, FirstPersonController firstPerson)
        {
            if (firstPerson == null) return;
            GameReflector gr = new GameReflector(firstPerson);
            FieldInfo field = gr.GetField("m_GravityMultiplier", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(firstPerson, gravity);
        }

        private static void Jump(int force)
        {
            if (flyModeControler == null) return;
            GameReflector gr = new GameReflector(flyModeControler);
            Vector3 dir = (Vector3)gr.GetFieldValue("m_MoveDir");
            dir.y = force;
            gr.SetFieldValue("m_MoveDir", dir);
        }

        private static void SetSpeed(int speed, FirstPersonController firstPerson)
        {
            if (firstPerson == null) return;
            GameReflector gr = new GameReflector(firstPerson);
            gr.SetFieldValue("m_RunSpeed", speed);
        }

        public static void UpdateFlyMode()
        {
            if (flyModeActive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump(8);
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    flyModeControler.ResetMovement(flyModeControler.gameObject.transform.position);
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Jump(-8);
                }
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    flyModeControler.ResetMovement(flyModeControler.gameObject.transform.position);
                }
            }
        }

        public static void ToggleFlyMode(bool input)
        {
            if (input && !flyModeActive) EnableFlyMode();
            else if (!input && flyModeActive) DisableFlyMode();
        }
    }
}
