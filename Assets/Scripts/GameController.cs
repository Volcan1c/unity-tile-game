using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController groundTilemap;
    [SerializeField] HighlightTilemap highlightTilemap;
    [SerializeField] CharacterController[] friendlyCharacterControllers;
    [SerializeField] CharacterController[] enemyCharacterControllers;
    [SerializeField] TurnEnum currentTurn;

    private int friendlyIndex;
    private int enemyIndex;
    private CharacterController currentCharacter;
    private PhaseEnum currentPhase;

    #region Start / Update

    void Start()
    {
        friendlyIndex = 0;
        enemyIndex = 0;
        SetCurrentCharacter();
        MovePhase();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int mousePosition = PositionToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));

            switch (currentPhase)
            {
                case PhaseEnum.Move:
                    AttemptMovement(mousePosition);
                    break;
                case PhaseEnum.Attack:
                    AttemptAttack(mousePosition);
                    break;
            }
            
        }
    }

    #endregion

    #region Attempt Action

    private void AttemptMovement(Vector3Int mousePosition)
    {
        var tileToMoveTo = groundTilemap.GetTile(mousePosition);

        if (tileToMoveTo != null && IsTileAccessible(mousePosition))
        {
            highlightTilemap.ClearHighlights();
            currentCharacter.Move(mousePosition);
            AttackPhase();
        }
    }

    private void AttemptAttack(Vector3Int mousePosition)
    {
        var tileToAttack = groundTilemap.GetTile(mousePosition);

        if (tileToAttack != null && IsTileAttackable(mousePosition))
        {
            highlightTilemap.ClearHighlights();
            NextTurn();
        }
    }

    #endregion

    #region Game Control

    private void NextTurn()
    {
        if (currentTurn == TurnEnum.Friendly)
        {
            friendlyIndex = friendlyIndex >= friendlyCharacterControllers.Length - 1 ? 0 : friendlyIndex + 1;
            currentTurn = TurnEnum.Enemy;
        } else
        {
            enemyIndex = enemyIndex >= enemyCharacterControllers.Length - 1 ? 0 : enemyIndex + 1;
            currentTurn = TurnEnum.Friendly;
        }

        SetCurrentCharacter();
        MovePhase();
    }

    private void MovePhase()
    {
        currentPhase = PhaseEnum.Move;
        DrawAccessibleTiles();
    }

    private void AttackPhase()
    {
        currentPhase = PhaseEnum.Attack;
        DrawAttackTiles();
    }

    #endregion

    #region Draw Highlights

    private void DrawAccessibleTiles()
    {
        List<Vector3Int> positionsToDraw = groundTilemap.GetAllTilePositions().FindAll(
            (Vector3Int position) => IsTileAccessible(position)
        );

        highlightTilemap.DrawMoveHighlight(positionsToDraw);
    }

    private void DrawAttackTiles()
    {
        List<Vector3Int> positionsToDraw = groundTilemap.GetAllTilePositions().FindAll(
            (Vector3Int position) => IsTileAttackable(position)
        );

        if (positionsToDraw.Count > 0) {
            highlightTilemap.DrawAttackHighlight(positionsToDraw);
        } else
        {
            NextTurn();
        }
    }

    #endregion

    #region Character Methods

    private void SetCurrentCharacter()
    {
        if (currentTurn == TurnEnum.Friendly)
        {
            currentCharacter = friendlyCharacterControllers[friendlyIndex];
        }
        else
        {
            currentCharacter = enemyCharacterControllers[enemyIndex];
        }
    }

    private CharacterController[] GetOppositeCharacters()
    {
        if (currentTurn == TurnEnum.Friendly)
        {
            return enemyCharacterControllers;
        }
        else
        {
            return friendlyCharacterControllers;
        }
    }

    #endregion

    #region Tile Status

    private bool IsTileAccessible(Vector3Int tilePosition)
    {
        return !IsTileOccupied(tilePosition) && currentCharacter.IsTileAccessible(tilePosition);
    }

    private bool IsTileAttackable(Vector3Int tilePosition)
    {
        return IsTileOccupiedBy(GetOppositeCharacters(), tilePosition) &&
            currentCharacter.IsTileAttackable(tilePosition) &&
            !IsTileOccupiedByCurrentCharacter(tilePosition);
    }

    private bool IsTileOccupied(Vector3Int tilePosition)
    {
        return IsTileOccupiedBy(friendlyCharacterControllers, tilePosition) ||
            IsTileOccupiedBy(enemyCharacterControllers, tilePosition);
    }

    private bool IsTileOccupiedBy(CharacterController[] characters, Vector3Int tilePosition)
    {
        foreach (CharacterController characterController in characters)
        {
            if (PositionToVector3Int(characterController.GetPosition()).Equals(tilePosition))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsTileOccupiedByCurrentCharacter(Vector3Int tilePosition)
    {
        return PositionToVector3Int(currentCharacter.GetPosition()).Equals(tilePosition);
    }

    #endregion

    private Vector3Int PositionToVector3Int(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }
}

public enum TurnEnum
{
    Friendly,
    Enemy
}

public enum PhaseEnum
{
    Move,
    Attack
}