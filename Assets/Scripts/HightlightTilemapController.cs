using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HightlightTilemapController : TilemapController
{
    [SerializeField] TileBase moveHighlight;

    void Start()
    {
        tilemap.SetTile(new Vector3Int(1, 1, 0), moveHighlight);
        //tilemap.SetTile(new Vector3Int(1, 1, 0), null);
        //tilemap.ClearAllTiles();
    }

   
    void Update()
    {
        
    }
}
