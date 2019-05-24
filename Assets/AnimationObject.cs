using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAnimation", menuName = "AnimationObjects/PlayerAnimation")]
public class AnimationObject : ScriptableObject
{
    public RuntimeAnimatorController animation;
    public Material material;
}
