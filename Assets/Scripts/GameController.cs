using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController tilemapController;
    [SerializeField] CharacterController[] characterControllers;
    private int characterIndex;

    void Start()
    {
        characterIndex = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int mousePosition = PositionToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            var tileTypeToMoveTo = tilemapController.GetTile(mousePosition);

            if (tileTypeToMoveTo != null)
            {
                characterControllers[characterIndex].Move(mousePosition);
                characterIndex = characterIndex < characterControllers.Length - 1 ? characterIndex + 1 : 0;
            }
        }
    }

    private Vector3Int PositionToVector3Int(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }
}
