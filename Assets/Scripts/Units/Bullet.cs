using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public int damage;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Building" || col.gameObject.tag == "Unit")
        {
            Instantiate(explosion);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
