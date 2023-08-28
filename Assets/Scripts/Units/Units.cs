using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private NavMeshAgent agent;

    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;

    private void Start()
    {
        healthSlider.enabled = false;
    }

    private void Update()
    {
        healthSlider.value = health;
        if(healthSlider.value < healthSlider.maxValue)
        {
            healthSlider.enabled = true;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        FindEnemiesThenBuildings();
    }

    void FindEnemiesThenBuildings()
    {
        if (_team == Team.Team1)
        {

        }
        if (_team == Team.Team2)
        {

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
