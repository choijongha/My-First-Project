using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNyang : NonAttackEnemy
{
    protected override void Reset()
    {
        base.Reset();
        RandomAction();
    }

    private void RandomAction()
    {

        int _random = Random.Range(0, 2);

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }

    private void Wait()  // 대기
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

}
