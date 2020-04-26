using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Transform characterTransform;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Move(Vector3Int position)
    {
        characterTransform.position = new Vector3(position.x + 0.5f, position.y + 0.5f, characterTransform.position.z);
    }

    public bool IsInTile(Vector3Int tilePosition)
    {
        Vector3Int characterPositionInt = Utils.Vector3ToVector3Int(characterTransform.position);

        return characterPositionInt.Equals(tilePosition);
    }
}
