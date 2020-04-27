using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
}
