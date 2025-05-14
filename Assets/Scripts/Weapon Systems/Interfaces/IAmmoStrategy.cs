using UnityEngine;

public interface IAmmoStrategy
{
    public void HandleAmmo(float angle, Vector2 initialPosition, ActiveWeaponData activeWeaponData);
}