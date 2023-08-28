using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Workers : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int gatheredGold;
    [SerializeField] UI_Main PlayerAsset;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GoldMine[] _goldMines;

    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;

    private void Start()
    {
        if(_team == Team.Team1)
        {
            PlayerAsset = GameObject.FindGameObjectWithTag("Player1").GetComponent<UI_Main>();
        }
        if (_team == Team.Team2)
        {
            PlayerAsset = GameObject.FindGameObjectWithTag("Player2").GetComponent<UI_Main>();
        }
    }

    private void Update()
    {
        healthSlider.value = health;
        if (healthSlider.value < healthSlider.maxValue)
        {
            healthSlider.enabled = true;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        FindGoldMine();
    }

    void FindGoldMine()
    {
        GameObject[] mines = GameObject.FindGameObjectsWithTag("GoldMine");
        _goldMines = new GoldMine[mines.Length];

        for(int i = 0; i < mines.Length; i++)
        {
            _goldMines[i] = mines[i].GetComponent<GoldMine>();
        }


        if(_goldMines.Length > 0)
        {
            //agent.FindClosestEdge(_goldMines[0], out);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("GoldMine"))
        {
            gatheredGold += 50;
        }

        if(_team == Team.Team1)
        {
            if (other.gameObject.CompareTag(""))
            {
                if(other.gameObject.name == "Team1_building_commandcenter")
                {
                    PlayerAsset._gold += gatheredGold;
                    gatheredGold = 0;
                }
            }
        }
        if(_team == Team.Team2)
        {
            if (other.gameObject.CompareTag(""))
            {
                if (other.gameObject.name == "Team2_building_commandcenter")
                {
                    PlayerAsset._gold += gatheredGold;
                    gatheredGold = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            health -= other.gameObject.GetComponent<Bullet>().damage;
        }
    }
}
