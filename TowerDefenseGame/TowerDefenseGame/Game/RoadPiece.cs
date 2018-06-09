using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class ConnectionPoint
    {
        public Vector2 Position;
        public bool IsTaken;
        public float ConnectAngle;

        public ConnectionPoint(Vector2 position, float connectAngle)
        {
            Position = position;
            ConnectAngle = connectAngle;
            IsTaken = false;
        }
    }

    public class RoadPieceData
    {
        public List<ConnectionPoint> ConnectionPoints;
        public Vector2 Position;

        public RoadPieceData(Vector2 position, params ConnectionPoint[] points)
        {
            ConnectionPoints = new List<ConnectionPoint>();
            foreach (ConnectionPoint point in points)
            {
                ConnectionPoints.Add(point);
            }
            Position = position;
        }
    }

    public class RoadPiece : Sprite
    {
        public RoadPieceData Data;

        public List<Vector2> Path;

        public RoadPiece(Texture2D texture, RoadPieceData data, List<Vector2> path) : base(texture, data.Position, Color.White, 0f, 1f)
        {
            Data = data;
            Path = path;
        }
    }
}