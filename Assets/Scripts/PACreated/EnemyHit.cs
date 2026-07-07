using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnPoint;
  
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -25f)
        {
            Die();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }


    public void Die()
    {
        Player.transform.position = SpawnPoint.transform.position;
    }
}
