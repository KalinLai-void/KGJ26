using KalinKonta.Stationery;
using UnityEngine;

public class WeldedGroupHealthProxy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var hitCollider = collision.contacts[0].thisCollider;

        if (hitCollider.TryGetComponent<Stationery>(out var stationery))
        {
            if (collision.gameObject.GetComponent<ZhengHua.ProjectileObject>())
            {
                stationery.Damage(3); // TODO: Intergrating damage from where
            }
        }
    }
}
