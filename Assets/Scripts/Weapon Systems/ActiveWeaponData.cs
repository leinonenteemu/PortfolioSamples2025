using System.Collections;
using UnityEngine;

public class ActiveWeaponData
{
    #region Private Properties
    private float damage;
    private int ammoCount;
    [Tooltip("This does nothing if ammo count is set to 1. It defines how wide the overall spread is in multi-shot weapons")]
    private float ammoSpread;
    [Tooltip("This does nothing if weapon is laser weapon. Sets projectile launch speed")]
    private float ammoSpeed;
    private GameObject projectilePrefab;
    #endregion

    #region Public Getters
    public float Damage => damage;
    public float AmmoSpeed => ammoSpeed;
    public int AmmoCount => ammoCount;
    public float AmmoSpread => ammoSpread;
    public GameObject ProjectilePrefab => projectilePrefab;
    #endregion

    //these will never change so we use readonly
    public readonly IShootStrategy ShootStrategy;
    public readonly IAmmoStrategy AmmoStrategy;


    public ActiveWeaponData(float damage, int ammoCount, float ammoSpread, float ammospeed, GameObject projectilePrefab, IShootStrategy shootStrategy, IAmmoStrategy ammoStrategy)
    {
        this.damage = damage;
        this.ammoCount = ammoCount;
        this.ammoSpread = ammoSpread;
        this.projectilePrefab = projectilePrefab;
        this.ammoSpeed = ammospeed;
        ShootStrategy = shootStrategy;
        AmmoStrategy = ammoStrategy;
    }
}
