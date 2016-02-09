using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public enum PickUpType { Health, Ammo }
    public PickUpType pickupType;
    public int reward = 10;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            GetObject();
            Destroy(gameObject);
        }
    }

    void GetObject()
    {
        switch (pickupType)
        {
            case PickUpType.Ammo: PlayerData.Instance.ModifyAmmo(reward); break;
            case PickUpType.Health: PlayerData.Instance.ModifyHealth(reward); break;
        }
    }
}
