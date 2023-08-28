using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    [SerializeField] bool activateBuildings = false;
    [SerializeField] private TextMeshProUGUI _txtGold = null;
    public int _gold;
    [SerializeField] private Button btn_cm = null;

    [SerializeField] private Button btn_storage = null;
    [SerializeField] private Button btn_barrack = null;
    [SerializeField] private Button btn_factory = null;
    [SerializeField] private Button btn_turret = null;

    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;


    [SerializeField] public BuildGrid _grid = null;
    [SerializeField] public StartBuilding[] buildings = null;

    [SerializeField] private GameObject _buildingsObject;
    [SerializeField] private int cm_Amount;


    [SerializeField] private GameObject _workers;
    [SerializeField] private GameObject _troopers;
    [SerializeField] private GameObject _tanks;

    private static UI_Main _instance = null;
    public static UI_Main instanse
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        cm_Amount = 1;
    }

    private void Start()
    {
        _buildingsObject.SetActive(true);
        btn_storage.interactable = false;
        btn_turret.interactable = false;
        btn_barrack.interactable = false;
        btn_factory.interactable = false;
        _buildingsObject.SetActive(false);
    }

    private void Update()
    {
        _txtGold.text = "Gold: " + _gold.ToString();
    }

    public void BuildingsButton()
    {
        activateBuildings = !activateBuildings;
        if (activateBuildings == true)
        {
            _buildingsObject.SetActive(true);
        }
        if (activateBuildings == false)
        {
            _buildingsObject.SetActive(false);
        }
    }

    public void Build_CommandCenter(int requiredGold = 500)
    {
        if (cm_Amount == 1)
        {
            if (_gold > requiredGold)
            {
                _gold -= requiredGold;
                cm_Amount = 0;
                Vector3 position = Vector3.zero;

                StartBuilding sb = Instantiate(buildings[0], position, Quaternion.identity);
                StartBuilding.instanse = sb;
                CameraClamp.instanse.isPlacingBuilding = true;

                _buildingsObject.SetActive(false);
            }
            else
            {
                Instantiate(SoundEffects._instance.PlaySound_Insufficient);
            }
        }
        else
        {
            cm_Amount = 0;
            btn_cm.interactable = false;
        }
    }
    public void Build_Storage(int requiredGold = 500)
    {
        if (_gold > requiredGold)
        {
            _gold -= requiredGold;
            Vector3 position = Vector3.zero;
            Instantiate(buildings[1], position, Quaternion.identity);
            _buildingsObject.SetActive(false);
        }
        else
        {
            Instantiate(SoundEffects._instance.PlaySound_Insufficient);
        }
    }
    public void Build_Barracks(int requiredGold = 500)
    {
        if (_gold > requiredGold)
        {
            _gold -= requiredGold;
            Vector3 position = Vector3.zero;
            Instantiate(buildings[3], position, Quaternion.identity);
            _buildingsObject.SetActive(false);
        }
        else
        {
            Instantiate(SoundEffects._instance.PlaySound_Insufficient);
        }
    }
    public void Build_Factory(int requiredGold = 500)
    {
        if (_gold > requiredGold)
        {
            _gold -= requiredGold;
            Vector3 position = Vector3.zero;
            Instantiate(buildings[4], position, Quaternion.identity);
            _buildingsObject.SetActive(false);
        }
        else
        {
            Instantiate(SoundEffects._instance.PlaySound_Insufficient);
        }
    }
    public void Build_Turret(int requiredGold = 500)
    {
        if (_gold > requiredGold)
        {
            _gold -= requiredGold;
            Vector3 position = Vector3.zero;
            Instantiate(buildings[2], position, Quaternion.identity);
            _buildingsObject.SetActive(false);
        }
        else
        {
            Instantiate(SoundEffects._instance.PlaySound_Insufficient);
        }
    }
}
