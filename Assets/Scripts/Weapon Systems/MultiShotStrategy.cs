using UnityEngine;

public class MultiShotStrategy : IShootStrategy
{
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData)
    {
        //calculate how much the angle for shooting a projectile should increase per shot
        float angleIncrement = activeWeaponData.AmmoSpread / (activeWeaponData.AmmoCount - 1);

        //calculate initial projectiles shooting rotation
        float startRotation = initialAngle + activeWeaponData.AmmoSpread / 2;

        for (int i = 0; i < activeWeaponData.AmmoCount; i++)
        {
            float rotation = startRotation - angleIncrement * i;

            //calculate projectiles inaccuracy based on the SO_BulletSpread data
            float inAccuracy = spread.GetRandom();

            //create projectiles/ammo based on the IAmmoStrategy that weapon has
            activeWeaponData.AmmoStrategy.HandleAmmo(rotation + inAccuracy, initialPosition, activeWeaponData);
        }
    }

    //overloaded method that lets you override accuracy stats
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData, float overrideAccuracyMultiplier)
    {
        float angleIncrement = activeWeaponData.AmmoSpread / (activeWeaponData.AmmoCount - 1);
        float startRotation = initialAngle + activeWeaponData.AmmoSpread / 2;

        for (int i = 0; i < activeWeaponData.AmmoCount; i++)
        {
            float rotation = startRotation - angleIncrement * i;

            //increases or decreases the projectiles inaccuracy based on the provided multiplier
            float inAccuracy = spread.GetRandom(overrideAccuracyMultiplier);
            activeWeaponData.AmmoStrategy.HandleAmmo(rotation + inAccuracy, initialPosition, activeWeaponData);
        }
    }
}
