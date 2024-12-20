using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public CharacterMovement charMov;
    public void PlayerAttack()
    {
        Debug.Log("Player Attacked!");
        charMov.DoAttack();
    }
}
