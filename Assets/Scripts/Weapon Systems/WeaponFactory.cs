using System.Collections;
using System.Collections.Generic;

public static class WeaponFactory
{
    //Gives weapon instances data on how to handle everything related to shooting that weapon
    //Could improve this by creating single instances of the strategies and providing references to those


    public static ActiveWeaponData CreateWeapon(SO_WeaponProperties weaponProperties)
    {
        IAmmoStrategy ammoStrategy = null;
        IShootStrategy shootStrategy = null;

        //Returns an interface that handles how that type of ammo is created
        switch (weaponProperties._WeaponType)
        {
            case WeaponType.Projectile: ammoStrategy = new ProjectileAmmoStrategy(); break;
            case WeaponType.Laser: ammoStrategy = new LaserAmmoStrategy(); break;
        }

        //Returns an interface that handles how the ammo(s) get shot 
        switch (weaponProperties.AmmoCount)
        {
            case 1: shootStrategy = new SingleShotStrategy(); break;
            case >1: shootStrategy = new MultiShotStrategy(); break;
        }


        return new ActiveWeaponData(weaponProperties.Damage, weaponProperties.AmmoCount, weaponProperties.AmmoSpread, weaponProperties.AmmoSpeed,weaponProperties.ProjectilePrefab, shootStrategy, ammoStrategy);
        
    }
}
