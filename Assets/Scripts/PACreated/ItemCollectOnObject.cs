using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectOnObject : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public void Collect()
    {
        Player.GetComponent<Collect>().CollectItem(gameObject);
    }

}
