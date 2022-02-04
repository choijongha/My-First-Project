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

        int _random = Random.Range(0, 4); // ���, Ǯ���, �θ���, �ȱ�

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            Peek();
        else if (_random == 3)
            TryWalk();
    }

    private void Wait()  // ���
    {
        currentTime = waitTime;
        Debug.Log("���");
    }

    private void Eat()  // Ǯ ���
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ǯ ���");
    }

    private void Peek()  // �θ���
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }
}
