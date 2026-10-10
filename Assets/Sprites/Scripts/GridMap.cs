using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMap : MonoBehaviour
{
    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap walls;
    public bool IsWalkable(Vector2Int cell)
    {
        Vector3Int c = new Vector3Int(cell.x, cell.y, 0);
        return ground.HasTile(c) && !walls.HasTile(c);
    }
    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        Vector3Int c = ground.WorldToCell(worldPos);
        return new Vector2Int(c.x, c.y);
    }
    public Vector3 CellToWorld(Vector2Int cell)
    {
        return ground.GetCellCenterWorld(new Vector3Int(cell.x, cell.y, 0));
    }
    void OnDrawGizmos()
    {
        if (ground == null || walls == null) return;

        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        foreach (Vector3Int c in ground.cellBounds.allPositionsWithin)
        {
            Vector2Int cell = new Vector2Int(c.x, c.y);
            if (IsWalkable(cell))
            {
                Gizmos.DrawCube(CellToWorld(cell), Vector3.one * 0.8f);
            }
        }
    }
}