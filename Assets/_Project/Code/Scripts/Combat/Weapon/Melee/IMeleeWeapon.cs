using UnityEngine;

public interface IMeleeWeapon
{
    public bool isReload{get;}
    public void ResetTimer();
    public bool TestCollider(Collider colliderTarget);
    public void Attack(Collider colliderTarget);
}
