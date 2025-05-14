using UnityEngine;

public class SingleShotStrategy : IShootStrategy
{
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData)
    {
        //calculate projectiles inaccuracy based on the SO_BulletSpread data
        var inAccuracy = spread.GetRandom();
        var newAngle = initialAngle + inAccuracy;

        //create projectiles/ammo based on the IAmmoStrategy that weapon has
        activeWeaponData.AmmoStrategy.HandleAmmo(newAngle, initialPosition, activeWeaponData);
    }

    //overloaded method that lets you override accuracy stats
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData, float overrideAccuracyMultiplier)
    {
        //increases or decreases the projectiles inaccuracy based on the provided multiplier
        var inAccuracy = spread.GetRandom(overrideAccuracyMultiplier);
        var newAngle = initialAngle + inAccuracy;

        activeWeaponData.AmmoStrategy.HandleAmmo(newAngle, initialPosition, activeWeaponData);
    }
}
