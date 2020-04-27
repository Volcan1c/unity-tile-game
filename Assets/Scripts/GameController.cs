using System;
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
            Vector3Int mousePosition = Utils.Vector3ToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            var tileTypeToMoveTo = tilemapController.GetTile(mousePosition);
            var currentCharacter = characterControllers[characterIndex];

            if (
                tileTypeToMoveTo != null &&
                !IsTileOccupied(mousePosition) &&
                currentCharacter.IsInMoveRange(mousePosition)
               )
            {
                currentCharacter.Move(mousePosition);
                characterIndex = characterIndex < characterControllers.Length - 1 ? characterIndex + 1 : 0;
            }
        }
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