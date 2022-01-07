using System.Collections.Generic;
using UnityEngine;

namespace Main.Miscellaneous
{
    public class LineManager
    {
        private List<GameObject> lines = new List<GameObject>();

        public int CreateLine(float thickness, Material material, Vector3 startPos, Vector3 endPos)
        {
            GameObject line = new GameObject();
            line.transform.position = startPos;
            var lineRenderer = line.AddComponent<UnityEngine.LineRenderer>();

            lineRenderer.material = material;
            lineRenderer.startWidth = thickness;
            lineRenderer.endWidth = thickness;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);

            lines.Add(line);
            return lines.Count - 1;
        }

        public void RemoveLine(int index)
        {
            Object.Destroy(lines[index]);
        }

        public void ClearLines()
        {
            foreach (GameObject obj in lines)
            {
                Object.Destroy(obj);
            }
        }

        public void DestroyLine(int index)
        {
            Object.Destroy(lines[index]);
        }

        public int TryDrawDebugLine(Vector3 origin, Vector3 direction, Color color, int prev, Vector3 rayOrigin)
        {
            if (prev == -1) goto drawLine;
            RemoveLine(prev);
        drawLine:
            if (Physics.Raycast(new Ray(rayOrigin, direction), out RaycastHit hitinfo))
            {
                Vector3 endPoint = hitinfo.point;
                return CreateLine(0.0025f, Utilities.GetUnlitMaterial(color), origin, endPoint);
            }
            else return -1;
        }
    }
}
