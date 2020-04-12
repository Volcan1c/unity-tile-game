using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController groundTilemap;
    [SerializeField] HighlightTilemap highlightTilemap;
    [SerializeField] CharacterController[] characterControllers;

    private int turnIndex;

    void Start()
    {
        turnIndex = 0;
        DrawAccessibleTiles();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CharacterController character = characterControllers[turnIndex];

            Vector3Int mousePosition = PositionToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            var tileToMoveTo = groundTilemap.GetTile(mousePosition);

            if (tileToMoveTo != null && !IsTileOccupied(mousePosition) && character.IsTileAccessible(mousePosition)) {
                highlightTilemap.ClearHighlights();
                characterControllers[turnIndex].Move(mousePosition);
                NextTurn();
            }
        }
    }

    private Vector3Int PositionToVector3Int(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }

    private bool IsTileOccupied(Vector3Int tilePosition)
    {
        foreach (CharacterController characterController in characterControllers)
        {
            if (PositionToVector3Int(characterController.GetPosition()).Equals(tilePosition))
            {
                return true;
            }
        }

        return false;
    }

    private void NextTurn()
    {
        turnIndex = turnIndex >= characterControllers.Length - 1 ? 0 : turnIndex + 1;
        DrawAccessibleTiles();
    }

    private void DrawAccessibleTiles()
    {
        CharacterController character = characterControllers[turnIndex];

        List<Vector3Int> positionsToDraw = groundTilemap.GetAllTilePositions().FindAll(
            (Vector3Int position) => !IsTileOccupied(position) && character.IsTileAccessible(position)
        );

        highlightTilemap.DrawMoveHighlight(positionsToDraw);
    }
}
