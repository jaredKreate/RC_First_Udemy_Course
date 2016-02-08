using UnityEngine;
using System.Collections;

/// <summary>
/// Controls gun aiming, aim variance, firing rate, bullet type, and input handling.
/// </summary>
public class Gun : MonoBehaviour {

    public Transform bulletSpawn;
    public GameObject Bullet;
    public float firingSpeed = 0.2f;
    public float aimDistance = 10;
    public float accuracyDeviation = 1;
    public int ammoPerShot = 1;

    Vector3 aimDirection = Vector3.zero; //direction for the bullet to travel on
    Vector3 aimDeviation = Vector3.zero; //additional deviation to be added to aimDirection based on accuracyDeviation
    Vector3 aimPosition = Vector3.zero; //target aim position relative to the camera angle based on aimDistance
    float fireInput = 0;
    float fireTimer = 0;

    RaycastHit hitInfo; //will be used to detect if an object comes in our sights

    /// <summary>
    /// Get shooting input
    /// </summary>
    void GetInput()
    {
        fireInput = Input.GetAxis("Fire1");
    }

    /// <summary>
    /// Returns a vector3 direction for the path of the bullet to travel on.
    /// </summary>
    /// <returns></returns>
    Vector3 GetAimDirection()
    {
        aimPosition = Camera.main.transform.position + Camera.main.transform.forward * aimDistance;
        aimDirection = (aimPosition + GetAimDeviation()) - bulletSpawn.position;

        //if an object comes between our aim, we will adjust the aim direction to focus on the new object
        if (Physics.Raycast(Camera.main.transform.position, aimPosition - Camera.main.transform.position, out hitInfo))
        {
            //but do not adjust if we are too close to the hit point
            if (Vector3.Distance(hitInfo.point, bulletSpawn.position) > 5)
                aimDirection = ((hitInfo.point + GetAimDeviation()) - bulletSpawn.position);
        }
        return aimDirection;
    }

    /// <summary>
    /// Returns a vector3 offset based on the accuracy of the gun. 
    /// This will be added to the shot direction for a resulting path for the bullet.
    /// </summary>
    /// <returns></returns>
    Vector3 GetAimDeviation()
    {
        aimDeviation.x = Random.Range(-accuracyDeviation, accuracyDeviation);
        aimDeviation.y = Random.Range(-accuracyDeviation, accuracyDeviation);
        return aimDeviation;
    }
    
    /// <summary>
    /// Called when fireInput is active. 
    /// Will spawn a bullet and set the bullets path direction.
    /// Must reset fireTimer each time a bullet is spawned
    /// </summary>
    void Shoot()
    {
        if (fireTimer >= firingSpeed && PlayerData.Instance.ammo > 0)
        {
            GameObject bullet = Instantiate(Bullet, bulletSpawn.position, Quaternion.identity) as GameObject;
            bullet.transform.rotation = Quaternion.LookRotation(GetAimDirection());
            PlayerData.Instance.ModifyAmmo(-ammoPerShot); //subtract from ammo;
            fireTimer = 0;
        }
    }

    void Update()
    {
        /*aimPosition = Camera.main.transform.position + Camera.main.transform.forward * aimDistance;
        Debug.DrawLine(Camera.main.transform.position, aimPosition, Color.green);
        aimDirection = aimPosition - bulletSpawn.position;
        Debug.DrawRay(bulletSpawn.position, aimDirection, Color.blue);*/

        GetInput();
        if (fireTimer < firingSpeed)
            fireTimer += Time.deltaTime;
        if (fireInput > 0)
        {
            Shoot();
        }
    }

}
