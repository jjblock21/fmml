using FireworksMania.Common;
using FireworksMania.Core.Behaviors.Fireworks.Parts;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Input;
using FireworksMania.Interactions.Tools;
using FireworksMania.Interactions.Tools.FuseConnectorTool;
using FireworksMania.ScriptableObjects;
using FModApi;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace Main.FModApi
{
    /* Tool indexes
     *  1_HandTool
        2_PhysicsTool
        3_IgniteTool
        4_FuseConnectionTool
        5_EraserTool
     * */
    public class FuseConnector
    {
        private List<IHaveFuseConnectionPoint> _fuseConnectionPontList = new List<IHaveFuseConnectionPoint>();
        private FuseConnection _fuseConnectionPrefab = null;
        private FuseConnectionMetadata _fuseConnectionMetadata = null;

        private GameReflector gameReflector = null;

        public FuseConnector(FuseConnectionType fuseType)
        {
            AssignGameReflector();
            _fuseConnectionPrefab = FindFuseConnectionPrefab();
            _fuseConnectionMetadata = FindFuseConnectionMetadata((uint)fuseType);
        }

        public FuseConnector(FuseConnection fuseConnectionPrefab, FuseConnectionMetadata fuseConnectionMetadata)
        {
            AssignGameReflector();
            _fuseConnectionPrefab = fuseConnectionPrefab;
            _fuseConnectionMetadata = fuseConnectionMetadata;
        }

        private void AssignGameReflector()
        {
            ToolsManager manager = Object.FindObjectOfType<ToolsManager>();

            BaseTool[] tools = (BaseTool[])manager
                .GetType()
                .GetField("_toolItems", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(manager);

            FuseConnectionTool fuseTool = (FuseConnectionTool)tools[3];

            if (fuseTool == null)
            {
                Debug.LogError("Failed to find FuseTool");
                return;
            }
            gameReflector = new GameReflector(fuseTool);
        }


        public void AddFuseConnectionPoint(IHaveFuseConnectionPoint haveFuseConnection)
        {
            _fuseConnectionPontList.Add(haveFuseConnection);
        }

        public void RemoveFuseConnectionPoint(IHaveFuseConnectionPoint haveFuseConnection)
        {
            _fuseConnectionPontList.Remove(haveFuseConnection);
        }

        public void ClearFuseConnectionPoints()
        {
            _fuseConnectionPontList.Clear();
        }

        public void ConnectWithFuse(IFuseConnectionPoint connectionPoint1, IFuseConnectionPoint connectionPoint2)
        {
            if (connectionPoint1 != null && !connectionPoint1.Equals(null) &&
                connectionPoint2 != null && !connectionPoint2.Equals(null) &&
                connectionPoint1.Fuse != null && !connectionPoint1.Fuse.IsUsed && !connectionPoint1.Fuse.IsIgnited &&
                connectionPoint2.Fuse != null && !connectionPoint2.Fuse.IsUsed && !connectionPoint2.Fuse.IsIgnited)
            {
                Object.Instantiate(_fuseConnectionPrefab, FireworksManager.Instance.transform)
                .SetupConnection(connectionPoint1.Fuse, connectionPoint2.Fuse, _fuseConnectionMetadata);
            }
        }

        public void FuseAll()
        {
            IFuseConnectionPoint previousFuseConnectionPoint = null;
            foreach (IHaveFuseConnectionPoint connection in _fuseConnectionPontList)
            {
                IFuseConnectionPoint connectionPoint = connection.ConnectionPoint;
                if (previousFuseConnectionPoint == null)
                {
                    previousFuseConnectionPoint = connectionPoint;
                    continue;
                }
                ConnectWithFuse(connectionPoint, previousFuseConnectionPoint);
                previousFuseConnectionPoint = connectionPoint;
            }
        }

        public IEnumerator FuseAllCoroutine(int delayPerFuse)
        {
            IFuseConnectionPoint previousFuseConnectionPoint = null;
            foreach (IHaveFuseConnectionPoint connection in _fuseConnectionPontList)
            {
                IFuseConnectionPoint connectionPoint = connection.ConnectionPoint;
                if (previousFuseConnectionPoint == null)
                {
                    previousFuseConnectionPoint = connectionPoint;
                    continue;
                }
                ConnectWithFuse(connectionPoint, previousFuseConnectionPoint);
                previousFuseConnectionPoint = connectionPoint;
                if (delayPerFuse < 1) continue;
                yield return new WaitForSeconds(delayPerFuse);
            }
        }

        public FuseConnection FindFuseConnectionPrefab()
        {
            if (gameReflector == null) Debug.LogError("GameReflector has not been assigned.");
            FuseConnection fuseConnection = (FuseConnection)gameReflector.GetFieldValue("_fuseConnectionPrefab");
            if (fuseConnection == null)
            {
                Debug.LogError("Failed to find FuseConnectionPrefab");
                return null;
            }
            return fuseConnection;
        }

        public FuseConnectionMetadata FindFuseConnectionMetadata(uint fuseType)
        {
            if (gameReflector == null) Debug.LogError("GameReflector has not been assigned.");
            object fuseConnection = gameReflector.GetFieldValue("_fuseConnectionTypes");
            if (fuseConnection == null)
            {
                Debug.LogError("Failed to find FuseConnectionPrefab");
                return null;
            }
            FuseConnectionMetadata[] metadatas = (FuseConnectionMetadata[])fuseConnection;
            if (fuseType < 0 || fuseType > 3) return null;
            return metadatas[fuseType];
        }
    }

    public enum FuseConnectionType : uint
    {
        Slow = 0, Medium = 1, Fast = 2, Instant = 3
    }
}
