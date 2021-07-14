using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Injected
{
    public static class ShapeDrawer
    {
        private static int[] planePrevLines = { -1, -1, -1, -1 };

        public static void DrawPlane(Vector3 pos1, Vector3 pos2, Vector3 pos3, Vector3 pos4, Color color)
        {
            var mat = Utils.GetUnlitMaterial(color);
            if (planePrevLines[0] != -1) Utils.RemoveLine(planePrevLines[0]);
            if (planePrevLines[1] != -1) Utils.RemoveLine(planePrevLines[1]);
            if (planePrevLines[2] != -1) Utils.RemoveLine(planePrevLines[2]);
            if (planePrevLines[3] != -1) Utils.RemoveLine(planePrevLines[3]);
            planePrevLines[0] = Utils.DrawLine(0.025f, mat, pos1, pos2);
            planePrevLines[1] = Utils.DrawLine(0.025f, mat, pos2, pos3);
            planePrevLines[2] = Utils.DrawLine(0.025f, mat, pos3, pos4);
            planePrevLines[3] = Utils.DrawLine(0.025f, mat, pos4, pos1);
        }

        public static void RemovePlane()
        {
            if (planePrevLines[0] != -1) Utils.RemoveLine(planePrevLines[0]);
            if (planePrevLines[1] != -1) Utils.RemoveLine(planePrevLines[1]);
            if (planePrevLines[2] != -1) Utils.RemoveLine(planePrevLines[2]);
            if (planePrevLines[3] != -1) Utils.RemoveLine(planePrevLines[3]);
            planePrevLines[0] = -1;
            planePrevLines[1] = -1;
            planePrevLines[2] = -1;
            planePrevLines[3] = -1;
        }
    }
}
