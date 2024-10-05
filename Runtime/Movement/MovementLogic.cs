using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TGElib.Grid;


namespace TGElib.Movement
{
    public class MovementLogic : MonoBehaviour
    {
        [SerializeField]
        protected GridSpawner gridSpawner;
        private List<Tile> openTile = new List<Tile>();
        private List<Tile> closedTile = new List<Tile>();
        [SerializeField]
        protected Obstacle[] obstacleTile;

        protected List<Tile> ProcessingPath(Vector2 startPosition, Vector2 targetPosition)
        {
            Vector2Int startingPos = new Vector2Int((int)startPosition.x, (int)startPosition.y);
            Vector2Int targetPos = new Vector2Int((int)targetPosition.x, (int)targetPosition.y);

            openTile.Add(new Tile() { pos = startingPos, g = 0, h = CalculateH(startingPos, targetPos), parent = null });
            openTile[0].CalculateF();

            while (openTile.Count > 0)
            {
                //Get lowest f in open tile
                Tile q = openTile.OrderBy(tile => tile.f).First();
                if (q.pos == targetPos)
                    return RetracePath(q);
                closedTile.Add(q);
                openTile.Remove(q);

                foreach (var s in CreateS(q.pos, q))
                {
                    if (closedTile.Any(r => r.pos == s.pos)) continue;

                    float tentativeG = CalculateG(q, s.dirFrom);
                    Tile existingOpenTile = openTile.FirstOrDefault(r => r.pos == s.pos);

                    // If the tile is already in the open list and the tentative g is not better, skip.
                    if (existingOpenTile != null && tentativeG >= existingOpenTile.g)
                        continue;

                    s.g = tentativeG;
                    s.h = CalculateH(s.pos, targetPos);
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
            return new List<Tile>();
        }
        List<Tile> RetracePath(Tile endTile)
        {
            List<Tile> path = new List<Tile>();

            path.Add(endTile);
            Tile currentTile = endTile;
            while (currentTile.parent != null)
            {
                path.Add(currentTile.parent);
                currentTile = currentTile.parent;
            }
            path.Reverse();
            return path;
        }
        List<Tile> CreateS(Vector2Int parentTilePos, Tile parentTile)
        {
            List<Tile> temp = new List<Tile>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                Vector2Int dirVec = DirHelper.GetVecFromDir(direction);
                int x = parentTilePos.x + dirVec.x;
                int y = parentTilePos.y + dirVec.y;

                Vector2Int sPos = new Vector2Int(x, y);
                if (!gridSpawner.grids.Any(r => r.gPos == sPos)) continue;

                if (obstacleTile.Any(r => r.pos == sPos)) continue;

                temp.Add(new Tile() { pos = sPos, parent = parentTile, dirFrom = direction });
            }
            return temp;
        }

        float CalculateG(Tile parentTile, Direction dir)
        {
            return parentTile.g + DirHelper.GetCostFromDir(dir);
        }
        float CalculateH(Vector2 currentPos, Vector2 targetPos)
        {
            return Mathf.Sqrt(Mathf.Pow(currentPos.x - targetPos.x, 2) + Mathf.Pow(currentPos.y - targetPos.y, 2));
        }
    }
}


