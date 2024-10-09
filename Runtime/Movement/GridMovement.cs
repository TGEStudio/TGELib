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

        private List<GridNode> openTile = new List<GridNode>();
        private List<GridNode> closedTile = new List<GridNode>();

        public List<GridNode> ProcessingPath(Vector2 startPosition, Vector2 targetPosition, List<GridBase> grids)
        {
            openTile.Add(new GridNode(startPosition) { g = 0, h = CalculateH(startPosition, targetPosition), parent = null });
            openTile[0].CalculateF();

            while (openTile.Count > 0)
            {
                //Get lowest f in open tile
                GridNode q = openTile.OrderBy(tile => tile.f).First();
                if (q.gPos == targetPosition)
                    return RetracePath(q);
                closedTile.Add(q);
                openTile.Remove(q);

                foreach (var s in CreateS(q.gPos, q, grids))
                {
                    if (closedTile.Any(r => r.gPos == s.gPos)) continue;

                    float tentativeG = CalculateG(q, s.GetMoveCost());
                    GridNode existingOpenTile = openTile.FirstOrDefault(r => r.gPos == s.gPos);

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
            return new List<GridNode>();
        }
        List<GridNode> RetracePath(GridNode endTile)
        {
            List<GridNode> path = new List<GridNode>();

            path.Add(endTile);
            GridNode currentTile = endTile;
            while (currentTile.parent != null)
            {
                path.Add(currentTile.parent);
                currentTile = currentTile.parent;
            }
            path.Reverse();
            return path;
        }
        List<GridNode> CreateS(Vector2 parentTilePos, GridNode parentTile, List<GridBase> grids)
        {
            List<GridNode> temp = new List<GridNode>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Vector2 dirVec = DirHelper.GetVecFromDir(direction);
                float x = parentTilePos.x + dirVec.x;
                float y = parentTilePos.y + dirVec.y;

                Vector2 sPos = new Vector2(x, y);
                if (!grids.Any(r => r.gPos == sPos)) continue;

                if (_obstacleTile.GetGrids().Any(r => r.gPos == sPos)) continue;

                temp.Add(new GridNode(sPos, direction) { parent = parentTile });
            }
            return temp;
        }

        float CalculateG(GridNode parentTile, Direction dir)
        {
            return parentTile.g + DirHelper.GetCostFromDir(dir);
        }
        float CalculateH(Vector2 currentPos, Vector2 targetPos)
        {
            return Mathf.Sqrt(Mathf.Pow(currentPos.x - targetPos.x, 2) + Mathf.Pow(currentPos.y - targetPos.y, 2));
        }
    }
}


