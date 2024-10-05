using TGElib.Movement;
using UnityEngine;

namespace TGElib.Grid
{
    public class GridNode : GridBase
    {
        private GridNode _parent;
        public GridNode parent { get { return _parent; } set { _parent = value; } }
        private Direction _dirFrom;

        private float _g;
        private float _h;
        private float _f;

        public float g { get { return _g; } set { _g = value; } }
        public float h { get { return _h; } set { _h = value; } }
        public float f { get { return _f; } set { _f = value; } }

        public GridNode(Vector2 gPos) : base(gPos, null)
        { }
        public GridNode(Vector2 gPos, Direction dirFrom) : base(gPos, null)
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