using UnityEngine;

public interface IShootStrategy
{
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData);
    public void Shoot(float initialAngle, Vector2 initialPosition, SO_BulletSpread spread, in ActiveWeaponData activeWeaponData, float overrideAccuracyMultiplier);
}
