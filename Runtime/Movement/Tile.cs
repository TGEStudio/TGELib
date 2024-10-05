using UnityEngine;

namespace TGElib.Movement
{
    public class Tile
    {
        public Tile parent = null;
        public Direction dirFrom;
        public Vector2Int pos = Vector2Int.zero;
        public float g = 0;
        public float h = 0;
        public float f = 0;
        public void CalculateF()
        {
            f = g + h;
        }
    }
}