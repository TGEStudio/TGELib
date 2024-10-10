using System.Collections.Generic;
using UnityEngine;

namespace TGELib.Grid
{
    public class TileSpawner
    {
        private GridManager _grid;
        private Transform _parent;
        public TileSpawner(GridManager grid, Transform parent)
        {
            _grid = grid;
            _parent = parent;
        }
        public void SpawnTile(Vector2 spawnPos, Sprite spawnSprite)
        {
            GameObject gridObj = GameObject.Instantiate(_grid.GetData().gSpawnObject, spawnPos, Quaternion.identity);
            gridObj.transform.SetParent(_parent);

            if (gridObj.TryGetComponent<TileDisplay>(out TileDisplay gridDisplay))
            {
                TileBase tile = new TileBase(spawnPos, spawnSprite);
                _grid.AddTile(tile);
                gridDisplay.tile = tile;
            }
            else
            {
                throw new MissingComponentException("Missing GridDisplay");
            }
        }
        public void SpawnTile(Vector2 spawnPos)
        {
            SpawnTile(spawnPos, _grid.GetData().gSpriteIn);
        }
    }
}
