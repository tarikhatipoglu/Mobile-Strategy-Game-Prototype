using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Turret : MonoBehaviour
{
    [SerializeField] private Units _units;
    [SerializeField] private GameObject turretBullet;

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_units._team == Units.Team.Team1)
        {
            if (other.gameObject.CompareTag("Team2Unit"))
            {
                Debug.Log("Fire at Team2");
            }
        }
        if (_units._team == Units.Team.Team2)
        {
            if (other.gameObject.CompareTag("Team1Unit"))
            {
                Debug.Log("Fire at Team2");
            }
        }
    }
}
