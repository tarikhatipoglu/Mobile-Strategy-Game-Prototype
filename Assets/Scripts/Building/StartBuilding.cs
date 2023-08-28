using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBuilding : MonoBehaviour
{
    private static StartBuilding _instance = null;
    public static StartBuilding instanse
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    [SerializeField] private bool placeConfirmed = false;

    [SerializeField] private float buildTime;
    [SerializeField] private Slider buildTimeSlider;
    [SerializeField] private MeshRenderer _baseArea = null;
    [SerializeField] private GameObject buildingPrefab = null;

    [SerializeField] private int _rows; public int rows { get { return _rows; } }
    [SerializeField] private int _columns; public int columns { get { return _columns; } }

    private int _currentX = 0; public int currentX { get { return _currentX; } }
    private int _currentY = 0; public int currentY { get { return _currentY; } }
    private int _X = 0;
    private int _Y = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(placeConfirmed)
        {
            StartConstruction();
        }
    }

    public void PlacedOnGrid(int x, int y)
    {
        _currentX = x; 
        _currentY = y;
        _X = x;
        _Y = y;
        Vector3 position = UI_Main.instanse._grid.GetCenterPosition(x, y, _rows, _columns);
        transform.position = position;
    }

    public void StartMovingOnGrid()
    {
        _X = _currentX;
        _Y = _currentY;
    }

    public void RemovedFromGrid()
    {
        _instance = null;
        CameraClamp.instanse.isPlacingBuilding = false;
        Destroy(gameObject);
    }

    public void UpdateGridPosition(Vector3 basePosition, Vector3 currentPosition)
    {
        Vector3 dir = UI_Main.instanse._grid.transform.TransformPoint(currentPosition) - UI_Main.instanse._grid.transform.TransformPoint(basePosition);

        int xDis = Mathf.RoundToInt(-dir.z / UI_Main.instanse._grid.cellSize);
        int yDis = Mathf.RoundToInt(dir.x / UI_Main.instanse._grid.cellSize);

        _currentX = _X + xDis;
        _currentY = _Y + xDis;

        Vector3 position = UI_Main.instanse._grid.GetCenterPosition(_currentX, _currentY, _rows, _columns);
        transform.position = position;
    }

    void StartConstruction()
    {
        buildTimeSlider.value = buildTime;
        buildTime += Time.deltaTime;
        if(buildTime >= 10)
        {
            Instantiate(buildingPrefab);
            Destroy(this.gameObject);
        }
    }
}
