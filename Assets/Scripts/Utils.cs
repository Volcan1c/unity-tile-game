using UnityEngine;

public class Utils
{
    public static Vector3Int PositionToVector3Int(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }
}
