using UnityEngine;

public class ProjectileAmmoStrategy : IAmmoStrategy
{
    public void HandleAmmo(float angle, Vector2 initialPosition, ActiveWeaponData activeWeaponData)
    {
        GameObject projectile = MonoBehaviour.Instantiate(activeWeaponData.ProjectilePrefab, initialPosition, Quaternion.Euler(0, 0, angle));
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * activeWeaponData.AmmoSpeed, ForceMode2D.Impulse);
    }
}

