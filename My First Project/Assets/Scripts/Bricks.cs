using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    public void MakeDot (Vector3 pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(pos);
        tilemap.SetTile(cellPosition, null);
        Debug.Log("SetTile");
    }
}
