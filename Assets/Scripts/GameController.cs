using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController tilemapController;
    [SerializeField] CharacterController[] characterControllers;
    [SerializeField] HighlightTilemapController highlightTilemapController;
    private int characterIndex;

    void Start()
    {
        characterIndex = 0;
        DrawMoveHighlights();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int mousePosition = Utils.Vector3ToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            var currentCharacter = characterControllers[characterIndex];

            if (IsTileAccessible(mousePosition))
            {
                currentCharacter.Move(mousePosition);
                NewTurn();
            }
        }
    }

    private bool IsTileAccessible(Vector3Int tilePosition)
    {
        var currentCharacter = characterControllers[characterIndex];
        var tileTypeToMoveTo = tilemapController.GetTile(tilePosition);

        return tileTypeToMoveTo != null &&
               !IsTileOccupied(tilePosition) &&
               currentCharacter.IsInMoveRange(tilePosition);
    }

    private void NewTurn()
    {
        characterIndex = characterIndex < characterControllers.Length - 1 ? characterIndex + 1 : 0;
        highlightTilemapController.ClearHighlights();
        DrawMoveHighlights();
    }

    private void DrawMoveHighlights()
    {
        List<Vector3Int> allTiles = tilemapController.GetAllTilePositions();
        List<Vector3Int> accessibleTiles = allTiles.FindAll(IsTileAccessible);

        highlightTilemapController.DrawMoveHighlights(accessibleTiles);
    }

    private bool IsTileOccupied(Vector3Int tilePosition)
    {
        foreach (CharacterController character in characterControllers)
        {
            if (character.IsInTile(tilePosition))
            {
                return true;
            }
        }
        
        return false;
    }
}