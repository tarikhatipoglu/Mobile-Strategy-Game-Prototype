using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects _instance;
    public enum Team
    {
        Team1,
        Team2,
    }
    public Team _team;

    public GameObject PlaySound_Building;
    public GameObject PlaySound_ConstructionComplete;
    public GameObject PlaySound_Insufficient;
    public GameObject PlaySound_TrainTroops;
    public GameObject PlaySound_TrainTanks;
    public GameObject PlaySound_Victory;
    public GameObject PlaySound_Defeat;

    private void Awake()
    {
        _instance = this;
    }
}
