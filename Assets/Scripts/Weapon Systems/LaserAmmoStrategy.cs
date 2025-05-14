
using UnityEngine;

public class LaserAmmoStrategy : IAmmoStrategy
{
    public void HandleAmmo(float angle, Vector2 initialPosition, ActiveWeaponData activeWeaponData)
    {
        GameObject g = MonoBehaviour.Instantiate(activeWeaponData.ProjectilePrefab, initialPosition, Quaternion.identity);

        BaseLaser laser = g.GetComponent<BaseLaser>();
        //get direction for the laser from the initial launch angle
        Vector2 dir = Helpers.GlobalHelper.DirectionFromAngle(angle);

        LineRenderer lineRenderer = g.GetComponent<LineRenderer>();

        //use wall layers as mask to set where the laser should end
        var layerMask = Helpers.GlobalHelper.WallObjectsLayerMask();
        RaycastHit2D hitPoint = Physics2D.Raycast(initialPosition, dir, Mathf.Infinity, layerMask);

        //the game is built in close arenas so there shouldn't be scenarios where the raycast doesn't hit something
        if (hitPoint)
        {
            SetLaserVisuals(lineRenderer, initialPosition, hitPoint.point);
        }

        g.SetActive(true);
        laser.CheckForObjectsInsideLaser(lineRenderer, angle, activeWeaponData.Damage, dir);
    }

    private void SetLaserVisuals(LineRenderer lineRenderer, Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
