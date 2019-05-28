using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "attack", menuName = "DamageObjects/PlayerAttack")]
public class PlayerDamageSCript : ScriptableObject
{
    public Vector2 damageRange = new Vector2(1, 3);
    public float knockbackScalar = 1f;
}
