using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BaseLaser : MonoBehaviour
{
    public virtual void CheckForObjectsInsideLaser(LineRenderer lineRenderer, float angle, float damage, Vector2 direction)
    {
        //get the laser damage instance width from the laser visual
        float width = lineRenderer.startWidth;

        //Cast box to find gameobjects inside the visual laser area
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(width, width), angle, direction, Mathf.Infinity, Helpers.GlobalHelper.DamageableObjectsLayerMask());
        
        var endPoint = lineRenderer.GetPosition(1);

        //determine max distance for the damageable objects to make sure we dont damage objects outside the visual laser range
        var distance = Vector2.Distance(endPoint, transform.position);

        for (int i = 0; i < hits.Length; i++)
        {
            //check if the gameobject hit by boxcast is within range (within visuals of the linerenderer)
            if (Helpers.GlobalHelper.IsObjectInRangeVector2(transform, hits[i].transform, distance))
            {
                if (hits[i].transform.TryGetComponent<IDamageable>(out var dmg))
                {
                    dmg.TakeDamage(damage, gameObject);
                }
            }
        }
    }
}
