using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour
{
    [SerializeField] int gold;

    private void Update()
    {
        if (gold <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
