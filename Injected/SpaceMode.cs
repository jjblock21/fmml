using FModApi;
using Injected;
using System.Reflection;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Main
{
    public static class SpaceMode
    {
        public static bool spaceModeActive = false;
        public static ToggleClass spaceModeToggle = new ToggleClass();

        private static void Spacemode(bool enabled)
        {
            FirstPersonController firstPerson = Object.FindObjectOfType<FirstPersonController>();
            if (firstPerson == null) return;
            GameReflector gr = new GameReflector(firstPerson);
            FieldInfo field = gr.GetField("m_JumpSpeed", BindingFlags.NonPublic | BindingFlags.Instance);
            if (enabled)
            {
                Physics.gravity = Vector3.zero;
                field.SetValue(firstPerson, 0);
            }
            else
            {
                Physics.gravity = new Vector3(0, -9.8f, 0);
                field.SetValue(firstPerson, 10);
            }
        }

        public static void ToggleSpaceMode(bool input)
        {
            if (input && !spaceModeActive)
            {
                spaceModeActive = true;
                Spacemode(true);
            }
            else if (!input && spaceModeActive)
            {
                spaceModeActive = false;
                Spacemode(false);
            }
        }
    }
}
