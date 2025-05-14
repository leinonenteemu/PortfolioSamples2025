using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/New Weapon")]
public class SO_WeaponProperties : ScriptableObject
{
    #region Serialized Base Properties
    [SerializeField, Range(0,10000)] private float _damage;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private SO_BulletSpread _bulletSpreadPattern;
    [SerializeField, Range(0,2000)] private float _rpm;
    
    [Tooltip("This does nothing if weapon is laser weapon. Sets projectile launch speed")]
    [SerializeField, Range(0,100)] private float _ammoSpeed;
    [SerializeField, Range(0,32)] private int _ammoCount;
    [SerializeField] private WeaponType _weaponType;
    
    [Tooltip("This does nothing if ammo count is set to 1. It defines how wide the overall spread is in multi-shot weapons")]
    [SerializeField, Range(0,1000)] private float _ammoSpread;
    [SerializeField] private AudioClip _shotSFX;
    [SerializeField] private AmmoType _ammoType;
    #endregion

    #region Public Getters
    public float Damage => _damage;
    public float RPM => _rpm;
    public float AmmoSpeed => _ammoSpeed;
    public float AmmoSpread => _ammoSpread;
    public int AmmoCount => _ammoCount;

    public GameObject ProjectilePrefab => _projectilePrefab;
    public SO_BulletSpread BulletSpreadPattern => _bulletSpreadPattern;
    public AudioClip ShotSFX => _shotSFX;
    public WeaponType _WeaponType => _weaponType;
    public AmmoType AmmoType => _ammoType;
    #endregion

}

//this is used to determine what ammostrategy we use
public enum WeaponType { Projectile, Laser };

//this determines what ammo resource the weapon consumes
public enum AmmoType { Energy, Shells, Rifle };
