using Main.UI;
using System;
using System.Linq;
using UnityEngine;

namespace Injected.UI
{
    public static class UI
    {
        private static float
        x, y,
        width, height,
        margin,
        controlHeight,
        controlDist,
        nextControlY,
        bottomOffset;

        public static void Begin(
            string text,
            float _x,
            float _y,
            float _width,
            float _height,
            float _margin,
            float _controlHeight,
            float _controlDist,
            float contolStartY,
            float bottomWidgetOffset)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            margin = _margin;
            controlHeight = _controlHeight;
            controlDist = _controlDist;
            nextControlY = contolStartY + y;
            bottomOffset = bottomWidgetOffset;
            GUI.Box(new Rect(x, y, width, height), text, UIStyles.UpdatedBoxStyle());
        }

        private static Rect NextControlRect()
        {
            Rect r = new Rect(x + margin, nextControlY, width - margin * 2, controlHeight);
            nextControlY += controlHeight + controlDist;
            return r;
        }

        private static Rect NextControlRect(float height)
        {
            Rect r = new Rect(x + margin, nextControlY, width - margin * 2, height);
            nextControlY += height + controlDist;
            return r;
        }

        public static void Space() => nextControlY += controlHeight + controlDist;
        public static void Space(int space) => nextControlY += space;

        public static string MakeEnable(string text, bool state) => string.Format("{0} {1}", text, state ? "ON" : "OFF");
        public static string MakeEnable(string onText, string offText, bool state) => state ? onText : offText;

        public static bool Button(string text, bool state) => Button(MakeEnable(text, state));
        public static bool Button(string text) => GUI.Button(NextControlRect(), text, UIStyles.UpdatedStyle());
        public static bool Button(string text, string text2, bool state) => Button(MakeEnable(text, text2, state));

        public static void Label(string text, float value, int decimals = 2) => Label(string.Format("{0}: {1}", text, Math.Round(value, decimals).ToString()));
        public static void Label(string text) => GUI.Label(NextControlRect(), text, UIStyles.UpdatedTextStyle());
        public static void Label(string text, int height) => GUI.Label(NextControlRect(height), text, UIStyles.UpdatedTextStyle());

        public static float Slider(float val, float min, float max) => GUI.HorizontalSlider(NextControlRect(), val, min, max);
        public static float Slider(float min, float max) => GUI.HorizontalSlider(NextControlRect(), 0, min, max);

        public static string Input(string text, int length) => GUI.TextField(NextControlRect(), text, length, UIStyles.UpdatedStyle());
        public static string Input(string text) => GUI.TextField(NextControlRect(), text, UIStyles.UpdatedStyle());
        public static string Input() => Input("");

        public static int ClampedIntegerInput(int value, int min, int max)
        {
            string newText = Input(value.ToString());
            string numbersOnly = new string(newText.Where(Char.IsDigit).ToArray());
            return Mathf.Clamp(int.Parse(numbersOnly), min, max);
        }

        private static Rect BottomSpaceRect()
        {
            Rect r = new Rect(x + margin, (y + height) - (bottomOffset + controlHeight), width - margin * 2, controlHeight);
            return r;
        }

        public static bool NavigationButton(string text) => GUI.Button(BottomSpaceRect(), text, UIStyles.UpdatedStyle());

        public static Rect GetGraphicsRect() => new Rect(x, y, width, height);

        public static Rect ControlRect(float x, float y, bool infiniteWidth)
        {
            if (infiniteWidth) return new Rect(x, y, 512, controlHeight);
            else return new Rect(x, y, width - margin * 2, controlHeight);
        }

        public static void Label(string text, string toolTip, int offset, int fontSize, Color color)
        {
            Rect r = ControlRect(offset, GetGraphicsRect().height + offset, true);
            GUIContent c = new GUIContent(text, toolTip);
            GUIStyle s = new GUIStyle();
            s.fontSize = fontSize;
            s.normal.textColor = color;
            GUI.Label(r, c, s);
        }

        public static Vector2 GetWidgetSize() => new Vector2(width - margin * 2, controlHeight);

        public static void Label(string text, float height)
        {
            GUI.Label(NextControlRect(height), text, UIStyles.UpdatedTextStyle());
        }

        public static void Label(string text, float height, GUIStyle style, string toolTip = "")
        {
            var content = new GUIContent(text, toolTip);
            GUI.Label(NextControlRect(height), content, style);
        }

        public static void Label(string text, string toolTip, int xOffset, int yOffset, int fontSize, Color color)
        {
            Rect r = ControlRect(xOffset, GetGraphicsRect().height + yOffset, true);
            GUIContent c = new GUIContent(text, toolTip);
            GUIStyle s = new GUIStyle()
            {
                fontSize = fontSize,
                normal = { textColor = color }
            };
            GUI.Label(r, c, s);
        }
    }
}
