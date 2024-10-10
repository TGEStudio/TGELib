using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGELib.Grid
{
	public class GridManager 
	{
		public GridManager(GridData gridData, Transform transform)
		{
			_transform = transform;
			_gridData = gridData;
		}
		private Transform _transform;
		private GridData _gridData;
		private List<TileBase> _tiles = new List<TileBase>();
		public GridData GetData()
		{
			return _gridData;
		}
		public List<TileBase> GetTiles()
		{
			return _tiles;
		}
		public void AddTile(TileBase tile)
		{
			_tiles.Add(tile);
		}
		public void AddTiles(List<TileBase> tiles)
		{
			_tiles.AddRange(tiles);
		}
		
		public void SpawnGrid()
		{
			Vector2 spawnPos = GetSpawnPosition();
			TileSpawner tileSpawner = new TileSpawner(this, _transform);
			for (float y = spawnPos.y; y < spawnPos.y + _gridData.gSize.y; y++)
			{
				for (float x = spawnPos.x; x < spawnPos.x + _gridData.gSize.x; x++)
				{
					tileSpawner.SpawnTile(new Vector2(x, y));
				}
			}
		}
		private Vector2 GetSpawnPosition()
		{
			float x = -_gridData.gSize.x * _gridData.gSpawnAnchor.x;
			float y = -_gridData.gSize.y * _gridData.gSpawnAnchor.y;
			Vector2 spawnPos = new Vector2((int)x, (int)y);
			return spawnPos;
		}
	}
}
