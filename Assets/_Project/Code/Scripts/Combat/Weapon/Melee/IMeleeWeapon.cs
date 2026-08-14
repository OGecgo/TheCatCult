using UnityEngine;

public interface IMeleeWeapon
{
    public void ResetTimer();
    public bool TestCollider(Collider colliderTarget);
    public void Attack(Collider colliderTarget);
}
