using UnityEngine;

namespace TGELib.Movement
{
    public static class DirHelper
    {
        public static Vector2Int GetVecFromDir(Direction dir)
        {
            return dir switch
            {
                Direction.Left => Vector2Int.left,
                Direction.Right => Vector2Int.right,
                Direction.Up => Vector2Int.up,
                Direction.Down => Vector2Int.down,
                Direction.LeftUp => new Vector2Int(-1, 1),
                Direction.LeftDown => new Vector2Int(-1, -1),
                Direction.RightDown => new Vector2Int(1, -1),
                Direction.RightUp => new Vector2Int(1, 1),
                _ => Vector2Int.zero
            };
        }
        public static float GetCostFromDir(Direction dir)
        {
            return dir switch
            {
                Direction.Left => 1,
                Direction.Right => 1,
                Direction.Up => 1,
                Direction.Down => 1,
                Direction.LeftUp => 1.4f,
                Direction.LeftDown => 1.4f,
                Direction.RightDown => 1.4f,
                Direction.RightUp => 1.4f,
                _ => 0
            };
        }
    }
}