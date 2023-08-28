using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private BuildGrid _grid = null;

    [SerializeField] private GameObject laser;
    [SerializeField] Building _building;

    private void OnTriggerEnter(Collider other)
    {
        if(_building._team == Building.Team.Team1)
        {
            if(other.gameObject.CompareTag("Team2Unit"))
            {
                Debug.Log("Fire at Team2");
            }
        }

        if (_building._team == Building.Team.Team2)
        {
            if (other.gameObject.CompareTag("Team1Unit"))
            {
                Debug.Log("Fire at Team1");
            }
        }
    }
}
