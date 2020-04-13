using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Transform characterTransform;
    [SerializeField] int movementRange;
    [SerializeField] int attackRange;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public Vector3 GetPosition()
    {
        return characterTransform.position;
    }

    public void Move(Vector3Int position)
    {        
        characterTransform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, characterTransform.position.z);
    }

    public float CalculateDistance(Vector3Int positionToMove)
    {
        float verticalDistance = Mathf.Abs(Mathf.FloorToInt(characterTransform.position.x) - positionToMove.x);
        float horizontalDistance = Mathf.Abs(Mathf.FloorToInt(characterTransform.position.y) - positionToMove.y);

        return verticalDistance + horizontalDistance;
    }

    public bool IsTileAccessible(Vector3Int positionToMove)
    {
        return CalculateDistance(positionToMove) <= movementRange;
    }

    public bool IsTileAttackable(Vector3Int positionToAttack)
    {
        return CalculateDistance(positionToAttack) <= attackRange;
    }
}
