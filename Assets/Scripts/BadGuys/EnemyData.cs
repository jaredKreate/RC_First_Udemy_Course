using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

    public string name = "Enemy";
    public int maxHealth = 100;
    public int health = 100;
    public int healDelta = 1;
    public float healRate = 0.5f;
    public int maxClips = 4;
    public int clips = 4;
    public int clipSize = 32;
    public int ammo = 250;
    public bool alive = true;

    float healTimer = 0;

    public void ModifyHealth(int mod)
    {
        health += mod;
        if (health > maxHealth)
            health = maxHealth;
        if (health < 0)
        {
            health = 0;
            alive = false;
//            Destroy(gameObject);
        }
    }

    public void ModifyAmmo(int mod)
    {
        ammo += mod;
        if (ammo > clipSize) //if clip is full
        {
            if (clips < maxClips) //check if we can fill a new clip
            {
                clips++; //add a clip
                ammo = ammo - clipSize; //set ammo to the difference
            }
            else //otherwise, cannot add a new clip, keep ammo at max
                ammo = clipSize;
        }

        if (ammo <= 0)//if the current clip runs out
        {
            if (clips > 0) //if we have another clip
            {
                ammo = clipSize + ammo; //set ammo to the max minus amount lost
                clips--; //remove the old clip
            }
            else//otherwise, if we are out of clips
            {
                ammo = 0; //out of ammo
            }
        }
    }

    void HealOverTime()
    {
        healTimer += Time.deltaTime;
        if (healTimer > healRate)
        {
            ModifyHealth(healDelta);
            healTimer = 0;
        }
    }

    void Update()
    {
        HealOverTime();
    }
}
