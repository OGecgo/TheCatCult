using UnityEngine;

public interface IMeleeWeapon
{
    public bool TestCollider(Collider colliderTarget);
    public void Attack(Collider colliderTarget);
}
