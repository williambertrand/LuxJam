using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Repair,
    Bomb
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Repair:
                    PlayerShipHealth.Instance.OnRepair(20);
                    break;
                case PickupType.Bomb:
                    PlayerInventory.Instance.OnBombPickup(1);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
