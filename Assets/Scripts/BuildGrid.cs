using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    [SerializeField] private int _rows = 45;
    [SerializeField] private int _columns = 45;
    [SerializeField] private float _cellSize = 1f;
    public float cellSize
    {
        get
        {
            return _cellSize;
        }
    }

    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;

    public Vector3 GetStartPosition(int x, int y)
    {
        Vector3 position = transform.position;
        position += (transform.right.normalized * x * _cellSize) + (transform.forward.normalized * y * _cellSize);
        return position;
    }

    public Vector3 GetCenterPosition(int x, int y, int rows, int columns)
    {
        Vector3 position = GetStartPosition(x,y);
        position += (transform.right.normalized * columns * _cellSize / 2f) + (transform.forward.normalized * rows * _cellSize / 2f);
        return position;
    }

    public Vector3 GetEndPosition(int x, int y, int rows, int columns)
    {
        Vector3 position = GetStartPosition(x, y);
        position += (transform.right.normalized * columns * _cellSize) + (transform.forward.normalized * rows * _cellSize / 2f);
        return position;
    }

    public bool IsWorldPositionIsOn(Vector3 position, int x, int y, int rows, int columns)
    {
        position = transform.InverseTransformPoint(position);
        Rect rect = new Rect(x, y, rows, columns);
        if(rect.Contains(new Vector2(position.x, position.z)))
        {
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for(int i = 0; i <= _rows; i++)
        {
            Vector3 point = transform.position + transform.forward.normalized * _cellSize * (float)i;
            Gizmos.DrawLine(point, point + transform.right.normalized * _cellSize * (float)_columns);
        }
        for (int i = 0; i <= _columns; i++) 
        {
            Vector3 point = transform.position + transform.right.normalized * _cellSize * (float)i;
            Gizmos.DrawLine(point, point + transform.forward.normalized * _cellSize * (float)_rows);
        }
    }
#endif

}
