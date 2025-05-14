using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    //has properties like fire rate, type of ammunition, how many shots and with what kind of spread etc
    [SerializeField] SO_WeaponProperties _weaponProperties;

    //used for determining where to spawn the projectiles when shooting
    [SerializeField] Transform _barrel;

    #region Private properties
    private ActiveWeaponData _weaponData;
    private bool _isInitialized;
    private float _nextFire;
    bool _readyToShoot;
    private float _timeBetweenShots;
    #endregion


    #region Public Getters
    public SO_WeaponProperties SO_WeaponProperties => _weaponProperties;
    public float NextFire => _nextFire;
    public AmmoType AmmoType => _weaponProperties.AmmoType;
    public bool IsReadyToShoot => _readyToShoot;
    #endregion

    private void OnEnable()
    {
        InitializeWeapon();
    }


    public void ToggleReadyToShoot(bool readyToShoot) => _readyToShoot = readyToShoot;
    public void UpdateNextFire() 
    {
        _nextFire = Time.time + _timeBetweenShots;
    }

    //Updates the cooldown time between each shot
    private void UpdateFireRate()
    {
        _timeBetweenShots = 1 / (_weaponProperties.RPM / 60);
        if (_timeBetweenShots <= 0) _timeBetweenShots = 0.05f;
    }

    public void ShootWeapon(bool isAccurate)
    {
        if (_weaponData == null)
        {
            Debug.LogError($"{gameObject.name} PlayerWeapon has no weapon data, check initialization");
            return;
        }

        if (!_isInitialized) InitializeWeapon();
        
        //use the weapons angle to give projectiles an initial rotation
        float angle = transform.rotation.eulerAngles.z;

        //if weapon is inaccurate due to player moving, increase the inaccuracy
        if (!isAccurate) _weaponData.ShootStrategy.Shoot(angle, _barrel.position, _weaponProperties.BulletSpreadPattern, _weaponData, _weaponProperties.BulletSpreadPattern.spreadMultiplier * 2f);
        else _weaponData.ShootStrategy.Shoot(angle, _barrel.position, _weaponProperties.BulletSpreadPattern, _weaponData);
        UpdateNextFire();
    }

    private void InitializeWeapon()
    {
        _weaponData = WeaponFactory.CreateWeapon(_weaponProperties);
        UpdateFireRate();
        _isInitialized = true;
        ToggleReadyToShoot(true);
    }
}
