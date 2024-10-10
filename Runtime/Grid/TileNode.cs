using TGELib.Movement;
using UnityEngine;

namespace TGELib.Grid
{
    public class TileNode : TileBase
    {
        private TileNode _parent;
        public TileNode parent { get { return _parent; } set { _parent = value; } }
        private Direction _dirFrom;

        private float _g;
        private float _h;
        private float _f;

        public float g { get { return _g; } set { _g = value; } }
        public float h { get { return _h; } set { _h = value; } }
        public float f { get { return _f; } set { _f = value; } }

        public TileNode(Vector2 tPos) : base(tPos, null)
        { }
        public TileNode(Vector2 tPos, Direction dirFrom) : base(tPos, null)
        {
            _dirFrom = dirFrom;
        }

        public void CalculateF()
        {
            f = g + h;
        }
        public Direction GetMoveCost()
        {
            return _dirFrom;
        }
    }
}