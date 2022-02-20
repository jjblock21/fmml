using UnityEngine;

namespace Main.UI
{
    public static class UIStyles
    {
        public static GUIStyle CreateUpdatedTextStyle()
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = 15;
            style.normal.textColor = Color.white;
            return style;
        }

        public static GUIStyle CreateUpdatedBoxStyle()
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.UpperCenter;
            style.fontSize = 15;
            style.normal.textColor = Color.white;
            return style;
        }

        public static GUIStyle CreateUpdatedStyle()
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 15;
            style.normal.textColor = Color.white;
            style.normal.background = normalTexture;
            style.hover.textColor = new Color(0.75f, 0.75f, 0.75f);
            style.hover.background = hoverTexture;
            return style;
        }

        public static GUIStyle UpdatedStyle() => updatedStyle;
        public static GUIStyle UpdatedBoxStyle() => updatedBoxStyle;
        public static GUIStyle UpdatedTextStyle() => updatedTextStyle;

        public static void CreateStyles()
        {
            updatedStyle = CreateUpdatedStyle();
            updatedBoxStyle = CreateUpdatedBoxStyle();
            updatedTextStyle = CreateUpdatedTextStyle();
        }

        public static void CreateTextures(int width, int height)
        {
            normalTexture = CreateTextureOnRuntime(false, width, height);
            hoverTexture = CreateTextureOnRuntime(true, width, height);
        }

        private static GUIStyle updatedStyle;
        private static GUIStyle updatedBoxStyle;
        private static GUIStyle updatedTextStyle;

        private static Texture2D normalTexture;
        private static Texture2D hoverTexture;

        public static Texture2D CreateTextureOnRuntime(bool faded, int width, int height)
        {
            Color color = Color.white;
            if (faded) color = new Color(0.75f, 0.75f, 0.75f);

            int yLevel = 0;

            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            texture.filterMode = FilterMode.Point;

            // Reset
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    texture.SetPixel(i, j, new Color(0f, 0f, 0f, 0f));

            for (int y = 0; y < height; y++)
            {
                if (yLevel == 0 || yLevel == 1 || yLevel == 2 ||
                    yLevel == height - 1 || yLevel == height - 2 ||
                    yLevel == height - 3
                )
                {
                    for (int x = 0; x < width; x++)
                    {
                        texture.SetPixel(x, y, color);
                    }
                }
                else
                {
                    texture.SetPixel(0, y, color);
                    texture.SetPixel(width - 1, y, color);
                    texture.SetPixel(1, y, color);
                    texture.SetPixel(width - 2, y, color);
                    texture.SetPixel(2, y, color);
                    texture.SetPixel(width - 3, y, color);
                }
                yLevel++;
            }
            texture.Apply();

            return texture;
        }
    }
}