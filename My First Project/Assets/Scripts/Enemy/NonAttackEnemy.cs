using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAttackEnemy : Unit
{
    public void Run(Vector3 targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;

        anim.SetBool("Running", isRunning);
    }

    public override void Damage(int dmg, Vector3 targetPos)
    {
        base.Damage(dmg, targetPos);
        if (!isDead) Run(targetPos);
    }
}
