using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapController : MonoBehaviour
{
    [SerializeField] protected Tilemap tilemap;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public TileBase GetTile(Vector3Int position)
    {
        return tilemap.GetTile(position);
    }

    public List<Vector3Int> GetAllTilePositions()
    {
        List<Vector3Int> result = new List<Vector3Int>();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            if (GetTile(position) != null)
            {
                result.Add(position);
            }
        }

        return result;
    }
}
