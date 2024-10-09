using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGELib.Grid
{
    public class GridManager : MonoBehaviour
    {
        private List<GridBase> _grids = new List<GridBase>();
        public List<GridBase> GetGrids()
        {
            return _grids;
        }
        public void AddGrid(GridBase grid)
        {
            _grids.Add(grid);
        }
        public void AddGrids(List<GridBase> grids)
        {
            _grids.AddRange(grids);
        }
    }
}
