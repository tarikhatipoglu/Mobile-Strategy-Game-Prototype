using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //private static Building _instance = null;
    //public static Building instanse
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //    set
    //    {
    //        _instance = value;
    //    }
    //}

    [System.Serializable] public class BuildingIcon
    {
        public Sprite icon = null;
    }

    private BuildGrid _grid = null;
    [SerializeField] private int buildingHealth;
    [SerializeField] private Slider buildingHealthSlider;

    private int currentX = 0;
    private int currentY = 0;

    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;

    private void Update()
    {
        buildingHealthSlider.value = buildingHealth;
        if (buildingHealthSlider.value < buildingHealthSlider.maxValue)
        {
            buildingHealthSlider.enabled = true;
        }

        if (buildingHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Laser")
        {
            buildingHealth -= col.gameObject.GetComponent<Bullet>().damage;
        }
    }
}
