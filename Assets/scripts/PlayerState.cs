using UnityEngine;

[System.Serializable]
public class PlayerState
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;

    public PlayerState(Vector3 pos, Quaternion rot, float t)
    {
        position = pos;
        rotation = rot;
        time = t;
    }
}
