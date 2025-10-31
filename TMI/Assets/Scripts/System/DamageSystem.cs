using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<Dealdamage>(DealDamagePerformer);

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<Dealdamage>();

    }
    private IEnumerator DealDamagePerformer(Dealdamage dealdamagega)
    {
        foreach (var target in dealdamagega.Targets)
        {
           target.takedamage(dealdamagega);
            yield return new WaitForSeconds(0.5f);
        }
    }
}