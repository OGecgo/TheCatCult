using UnityEngine;

public class DeathColliderTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHittable hittable))
        {
            hittable.OnAttack(1000000000);
        }
    }
}
