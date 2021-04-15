using System;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;

namespace Injected
{
    public static class Mouse
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y) => SetCursorPos(x, y);
        public static void SetCursorPosition(MousePoint point) => SetCursorPos(point.X, point.Y);

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();
            mouse_event((int)value, position.X, position.Y, 0, 0);
        }

        private static Timer timer = new Timer(2);
        private static bool ssacDown = false;

        public static bool ssacLeft = true;

        public static void InitSuperSonicAutoClicker()
        {
            timer.Elapsed += Timer_Elapsed;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (ssacLeft)
            {
                if (ssacDown)
                {
                    MouseEvent(MouseEventFlags.LeftUp);
                    ssacDown = false;
                }
                else
                {
                    MouseEvent(MouseEventFlags.LeftDown);
                    ssacDown = true;
                }
            }
            else
            {
                if (ssacDown)
                {
                    MouseEvent(MouseEventFlags.RightUp);
                    ssacDown = false;
                }
                else
                {
                    MouseEvent(MouseEventFlags.RightDown);
                    ssacDown = true;
                }
            }
        }

        public static bool ToggleSuperSonicAutoClicker()
        {
            if (timer.Enabled)
            {
                timer.Stop();
                if (ssacDown) MouseEvent(MouseEventFlags.LeftUp);
                if (ssacDown) MouseEvent(MouseEventFlags.RightUp);
            }
            else timer.Start();
            return timer.Enabled;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;
        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
