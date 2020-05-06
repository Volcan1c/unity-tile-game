using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] protected Tilemap tilemap;
    private List<Vector3Int> allTilePositions;

    void Start()
    {
        allTilePositions = new List<Vector3Int>();

        foreach (var tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            allTilePositions.Add(tilePosition);
        }
    }

    public TileBase GetTile(Vector3Int position)
    {
        return tilemap.GetTile(position);
    }

    public List<Vector3Int> GetAllTilePositions()
    {
        return allTilePositions;
    }
}
