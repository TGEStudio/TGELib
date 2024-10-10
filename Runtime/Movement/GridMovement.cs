using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TGELib.Grid;


namespace TGELib.Movement
{
    public class GridMovement
    {
        private GridManager _obstacleTile;
        public GridManager obstacleTile { get { return _obstacleTile; } set { _obstacleTile = value; } }

        private List<TileNode> openTile = new List<TileNode>();
        private List<TileNode> closedTile = new List<TileNode>();

        public List<TileNode> ProcessingPath(Vector2 startPosition, Vector2 targetPosition, List<TileBase> grids)
        {
            openTile = new List<TileNode>();
            closedTile = new List<TileNode>();

            openTile.Add(new TileNode(startPosition) { g = 0, h = CalculateH(startPosition, targetPosition), parent = null });
            openTile[0].CalculateF();


            while (openTile.Count > 0)
            {
                //Get lowest f in open tile
                TileNode q = openTile.OrderBy(tile => tile.f).First();
                if (q.gPos == targetPosition)
                    return RetracePath(q);
                closedTile.Add(q);
                openTile.Remove(q);

                foreach (var s in CreateS(q.gPos, q, grids))
                {
                    if (closedTile.Any(r => r.gPos == s.gPos)) continue;

                    float tentativeG = CalculateG(q, s.GetMoveCost());
                    TileNode existingOpenTile = openTile.FirstOrDefault(r => r.gPos == s.gPos);

                    // If the tile is already in the open list and the tentative g is not better, skip.
                    if (existingOpenTile != null && tentativeG >= existingOpenTile.g)
                        continue;

                    s.g = tentativeG;
                    s.h = CalculateH(s.gPos, targetPosition);
                    s.CalculateF();
                    s.parent = q;

                    if (existingOpenTile == null)
                    {
                        openTile.Add(s);
                    }
                    else
                    {
                        existingOpenTile.g = s.g;
                        existingOpenTile.h = s.h;
                        existingOpenTile.CalculateF();
                        existingOpenTile.parent = s.parent;
                    }
                }
            }
            return null;
        }
        List<TileNode> RetracePath(TileNode endTile)
        {
            List<TileNode> path = new List<TileNode>();

            path.Add(endTile);
            TileNode currentTile = endTile;
            while (currentTile.parent != null)
            {
                path.Add(currentTile.parent);
                currentTile = currentTile.parent;
            }
            path.Reverse();
            return path;
        }
        List<TileNode> CreateS(Vector2 parentTilePos, TileNode parentTile, List<TileBase> grids)
        {
            List<TileNode> temp = new List<TileNode>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Vector2 dirVec = DirHelper.GetVecFromDir(direction);
                float x = parentTilePos.x + dirVec.x;
                float y = parentTilePos.y + dirVec.y;

                Vector2 sPos = new Vector2(x, y);
                if (!grids.Any(r => r.gPos == sPos)) continue;

                if (_obstacleTile != null)
                    if (_obstacleTile.GetTiles().Any(r => r.gPos == sPos)) continue;

                temp.Add(new TileNode(sPos, direction) { parent = parentTile });
            }
            return temp;
        }

        float CalculateG(TileNode parentTile, Direction dir)
        {
            return parentTile.g + DirHelper.GetCostFromDir(dir);
        }
        float CalculateH(Vector2 currentPos, Vector2 targetPos)
        {
            return Mathf.Sqrt(Mathf.Pow(currentPos.x - targetPos.x, 2) + Mathf.Pow(currentPos.y - targetPos.y, 2));
        }
    }
}


