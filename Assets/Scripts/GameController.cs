using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilemapController groundTilemap;
    [SerializeField] HighlightTilemap highlightTilemap;
    [SerializeField] TeamController friendlyTeam;
    [SerializeField] TeamController enemyTeam;

    private TeamController currentTeam;
    private PhaseEnum currentPhase;

    #region Start / Update

    void Start()
    {
        currentTeam = friendlyTeam;
        MovePhase();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3Int mousePosition = Utils.PositionToVector3Int(mainCamera.ScreenToWorldPoint(Input.mousePosition));

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
            currentTeam.MoveCurrentUnit(mousePosition);
            
            AttackPhase();
        }
    }

    private void AttemptAttack(Vector3Int mousePosition)
    {
        var tileToAttack = groundTilemap.GetTile(mousePosition);

        if (tileToAttack != null && IsTileAttackable(mousePosition))
        {
            currentTeam.AttackUnitAtPosition(mousePosition);

            NextTurn();
        }
    }

    #endregion

    #region Game Control

    private void MovePhase()
    {
        currentPhase = PhaseEnum.Move;

        List<Vector3Int> accessibleTiles = groundTilemap.GetAllTilePositions().FindAll(
            (Vector3Int position) => IsTileAccessible(position)
        );

        highlightTilemap.ClearHighlights();
        highlightTilemap.DrawMoveHighlight(accessibleTiles);
    }

    private void AttackPhase()
    {
        currentPhase = PhaseEnum.Attack;

        List<Vector3Int> attackableTiles = groundTilemap.GetAllTilePositions().FindAll(
            (Vector3Int position) => IsTileAttackable(position)
        );

        if (attackableTiles.Count == 0)
        {
            NextTurn();

            return;
        }

        highlightTilemap.ClearHighlights();
        highlightTilemap.DrawAttackHighlight(attackableTiles);
    }

    private void NextTurn()
    {
        currentTeam.SwitchUnit();
        SwitchTeam();
        MovePhase();
    }

    #endregion

    #region Character Methods

    private void SwitchTeam()
    {
        currentTeam = GetOppositeTeam();
    }

    private TeamController GetOppositeTeam()
    {
        return currentTeam.team == TeamEnum.Enemy ? friendlyTeam : enemyTeam;
    }

    #endregion

    #region Tile Status

    private bool IsTileAccessible(Vector3Int tilePosition)
    {
        return !IsTileOccupied(tilePosition) && currentTeam.IsTileAccessible(tilePosition);
    }

    private bool IsTileAttackable(Vector3Int tilePosition)
    {
        return GetOppositeTeam().IsTileOccupiedByTeam(tilePosition) &&
            currentTeam.IsTileAttackable(tilePosition);
    }

    private bool IsTileOccupied(Vector3Int tilePosition)
    {
        return friendlyTeam.IsTileOccupiedByTeam(tilePosition) || enemyTeam.IsTileOccupiedByTeam(tilePosition);
    }

    #endregion
}

public enum PhaseEnum
{
    Move,
    Attack
}