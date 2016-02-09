using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public enum PickUpType { Health, Ammo }
    public PickUpType pickupType;
    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("pickup"))
        {
            GetObject();
        }
    }

    void GetObject()
    {
        switch (pickupType)
        {
            case PickUpType.Ammo: break;
            case PickUpType.Health: break;
        }
    }
}
