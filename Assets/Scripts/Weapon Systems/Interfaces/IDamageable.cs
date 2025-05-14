using UnityEngine;

public interface IDamageable
{
    //returns bool to give information on if target dies fr
    public bool TakeDamage(float damage, GameObject damageGiver);
}
