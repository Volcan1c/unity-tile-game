using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController tilemapController;
    [SerializeField] CharacterController characterController;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int mousePosition = PositionToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            var tileToMoveTo = tilemapController.GetTile(mousePosition);
            
            if (tileToMoveTo != null) characterController.Move(mousePosition);
        }
    }

    private Vector3Int PositionToVector3Int(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }
}
