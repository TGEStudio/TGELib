using System.Collections.Generic;
using UnityEngine;

namespace TGElib.Grid
{
    public class GridSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject gridObject;
        [SerializeField]
        private Vector2 gridSpawnAnchor;
        [SerializeField]
        private Vector2 gridSize;
        [SerializeField]
        private Sprite gridSpriteIn;
        [SerializeField]
        private Sprite gridSpriteOut;
        private List<Grid> _grids = new List<Grid>();
        public List<Grid> grids { get { return _grids; } }

        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            for (float y = gridSpawnAnchor.y; y < gridSpawnAnchor.y + gridSize.y; y++)
            {
                for (float x = gridSpawnAnchor.x; x < gridSpawnAnchor.x + gridSize.x; x++)
                {
                    Spawn(new Vector2(x, y), gridSpriteIn);
                }
            }
        }
        private void Spawn(Vector2 spawnPos, Sprite spawnSprite)
        {
            GameObject gridObj = Instantiate(gridObject, spawnPos, Quaternion.identity);
            gridObj.transform.SetParent(transform);

            if (gridObj.TryGetComponent<GridDisplay>(out GridDisplay gridDisplay))
            {
                Grid grid = new Grid(spawnPos, spawnSprite);
                _grids.Add(grid);
                gridDisplay.grid = grid;
            }
            else
            {
                throw new MissingComponentException("Missing GridDisplay");
            }
        }
    }
}
