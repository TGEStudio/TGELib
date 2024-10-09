using System.Collections.Generic;
using UnityEngine;

namespace TGELib.Grid
{
    [RequireComponent(typeof(GridManager))]
    public class GridSpawner : MonoBehaviour
    {
        [Header("Spawn Object")]
        [SerializeField]
        private GameObject gridObject;
        [Header("Spawn Position")]
        [SerializeField]
        private Vector2 spawnAnchor;
        [Header("Grid Asset")]
        [SerializeField]
        private Vector2 gridSize;
        [SerializeField]
        private Sprite gridSpriteIn;
        [SerializeField]
        private Sprite gridSpriteOut;
        [Header("Other")]
        [SerializeField]
        private bool drawGizmos = false;
        [Range(0f, 0.5f)]
        [SerializeField]
        private float gizmosSize;

        protected void SpawnGrids()
        {
            Vector2 spawnPos = GetSpawnPosition();
            for (float y = spawnPos.y; y < spawnPos.y + gridSize.y; y++)
            {
                for (float x = spawnPos.x; x < spawnPos.x + gridSize.x; x++)
                {
                    SpawnGrid(new Vector2(x, y), gridSpriteIn);
                }
            }
        }
        protected Vector2 GetMiddlePosition()
        {
            Vector2 offset = Vector2.zero;

            if (gridSize.x % 2 == 0)
                offset += new Vector2(-0.5f, 0);
            if (gridSize.y % 2 == 0)
                offset += new Vector2(0, -0.5f);

            return transform.position + (Vector3)offset;
        }
        private Vector2 GetSpawnPosition()
        {
            float x = -gridSize.x * spawnAnchor.x;
            float y = -gridSize.y * spawnAnchor.y;
            Vector2 spawnPos = new Vector2((int)x, (int)y);
            return spawnPos;
        }
        private void SpawnGrid(Vector2 spawnPos, Sprite spawnSprite)
        {
            GameObject gridObj = Instantiate(gridObject, spawnPos, Quaternion.identity);
            gridObj.transform.SetParent(transform);

            if (gridObj.TryGetComponent<GridDisplay>(out GridDisplay gridDisplay))
            {
                GridBase grid = new GridBase(spawnPos, spawnSprite);
                GetComponent<GridManager>().AddGrid(grid);
                gridDisplay.grid = grid;
            }
            else
            {
                throw new MissingComponentException("Missing GridDisplay");
            }
        }
        public void SpawnGrid(Vector2 spawnPos)
        {
            SpawnGrid(spawnPos, gridSpriteIn);
        }
        void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetMiddlePosition(), gizmosSize);
        }
    }
}
