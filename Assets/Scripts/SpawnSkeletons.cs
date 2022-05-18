using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletons : MonoBehaviour
{
    public GameObject bossRoomSkeletons;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossRoomSkeletons.SetActive(true);
        }
    }
}
