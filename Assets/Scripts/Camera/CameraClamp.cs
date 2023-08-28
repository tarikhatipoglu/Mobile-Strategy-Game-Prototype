using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    private static CameraClamp _instance = null;
    public static CameraClamp instanse
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _moveSpeed = 210;
    [SerializeField] private float _moveSmooth = 7;

    private Controls _inputs = null;

    [SerializeField] private bool _moving = false;

    private Vector3 _center = new Vector3(140, 150, 110);

    [SerializeField] private float _right = 750f;
    [SerializeField] private float _left = 0f;
    [SerializeField] private float _up = 850f;
    [SerializeField] private float _down = 0f;

    private float _angle = 75;

    private Transform _root = null;
    private Transform _pivot = null;
    private Transform _target = null;

    private bool _building = false;
    public bool isPlacingBuilding
    {
        get
        {
            return _building;
        }
        set
        {
            _building = value;
        }
    }
    private Vector3 _buildBasePosition = new Vector3(140, 150, 110);
    private bool _movingBuilding = false;

    void Awake()
    {
        _instance = this;

        //Yeni input deðeri alýyoruz
        _inputs = new Controls();

        //KAMERA ÝÇÝN 3 TANE OBJE OLUÞTURUYORUZ, KAMERA BU BÝRLEÞÝK 3 OBJEYE GÖRE HAREKET EDECEK
        _root = new GameObject("CameraRoot").transform;
        _pivot = new GameObject("CameraPivot").transform;
        _target = new GameObject("CameraTarget").transform;
    }

    void Start()
    {
        _instance = this;
        Initialized(_center, _right, _left, _up, _down, _angle);

        //KAMERANIN PERSPEKTÝF AÇISI
        _camera.orthographic = false;
        _camera.fieldOfView = 63;
    }

    //KAMERA ÝÇÝN AÇILAR VE YÖNLER
    private void Initialized(Vector3 center, float right, float left, float up, float down, float angle)
    {
        _center = center;
        _right = right;
        _left = left;
        _up = up;
        _down = down;
        _angle = angle;

        _moving = false;

        _pivot.SetParent(_root);
        _target.SetParent(_pivot);

        _root.position = center;
        _root.localEulerAngles = Vector3.zero;

        _pivot.localPosition = Vector3.zero;
        _pivot.localEulerAngles = new Vector3(_angle, 0, 0);

        _target.localPosition = Vector3.zero;
        _target.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        //KAMERA INPUT ÝLE HAREKET EDÝYORSA
        if (_moving)
        {
            Vector3 move = _inputs.Main.MoveDelta.ReadValue<Vector2>();
            if (move != Vector3.zero)
            {
                move.x /= Screen.width;
                move.y /= Screen.height;

                _root.position -= _root.right.normalized * move.x * _moveSpeed;
                _root.position -= _root.forward.normalized * move.y * _moveSpeed;
            }
        }

        if (_camera.transform.position != _target.position)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth * Time.deltaTime);
        }
        if (_camera.transform.rotation != _target.rotation)
        {
            _camera.transform.rotation = _target.rotation;
        }

        //KAMERANIN SINIRLANACAÐI FONKSÝYON
        ClampCamera();

        //BÝNA TASLAÐININ GRÝD'E YERLEÞTÝRÝLECEÐÝ KISIM
        if (_building && _movingBuilding)
        {
            Vector3 pos = _inputs.Main.PointerPosition.ReadValue<Vector2>();
            StartBuilding.instanse.UpdateGridPosition(_buildBasePosition, pos);

        }
    }

    //KOD AKTÝF OLDUÐU ZAMAN ÝNPUTMUZDAN Mouse veya Touch ÝLE KAMERAYI HAREKET ETTÝRECEZ
    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Main.Move.started += _ => MoveStarted();
        _inputs.Main.Move.canceled += _ => MoveCancelled();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    //INPUT KULLANILDIÐI ZAMAN BOOL AÇILACAK VEYA KAPANICAK
    void MoveStarted()
    {
        if (UI_Main.instanse.isActiveAndEnabled == true)
        {
            if(_building)
            {
                _buildBasePosition = _inputs.Main.PointerPosition.ReadValue<Vector2>();

                if (UI_Main.instanse._grid.IsWorldPositionIsOn(_buildBasePosition, StartBuilding.instanse.currentX, StartBuilding.instanse.currentY, StartBuilding.instanse.rows, StartBuilding.instanse.columns))
                {
                    StartBuilding.instanse.StartMovingOnGrid();
                    _movingBuilding = true;
                }
            }

            if (_movingBuilding == false)
            {
                _moving = true;
            }
        }
    }
    void MoveCancelled()
    {
        _moving = false;
        _movingBuilding = false;
    }

    private float PlaneSize()
    {
        float h = Mathf.Sin(_angle * Mathf.Deg2Rad) / 2f;
        return h;
    }

    //KAMERA BELÝRLÝ KOORDÝNATLAR ARASINDA KISITLANACAK
    void ClampCamera()
    {
        float h = PlaneSize();
        float w = _camera.aspect;

        Vector3 tr = _root.position + _root.right.normalized * w + _root.forward.normalized * h;
        Vector3 tl = _root.position - _root.right.normalized * w + _root.forward.normalized * h;
        Vector3 dr = _root.position + _root.right.normalized * w - _root.forward.normalized * h;
        Vector3 dl = _root.position - _root.right.normalized * w - _root.forward.normalized * h;

        //SAÐ SOL KISITLAMA
        if(tr.x > _center.x + _right)
        {
            _root.position += Vector3.left * Mathf.Abs(tr.x - (_center.x + _right));
        }
        if (tl.x < _center.x - _left)
        {
            _root.position += Vector3.right * Mathf.Abs((_center.x + _left) - tl.x);
        }

        //KUZEY GÜNEY KISITLAMA
        if (tr.z > _center.z + _up)
        {
            _root.position += Vector3.back * Mathf.Abs(tr.z - (_center.z + _up));
        }
        if (tl.z < _center.z - _down)
        {
            _root.position += Vector3.forward * Mathf.Abs((_center.z + _down) - tl.z);
        }
    }
}
