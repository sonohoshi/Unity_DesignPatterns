using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Slime : Enemy
{
    private State<Slime> slimeState;

    private void Awake()
    {
        slimeState = new IdleState();
    }

    protected override void Update()
    {
        base.Update();
        State<Slime> nowState = slimeState.InputHandle(this);
        slimeState.action(this);

        if (!nowState.Equals(slimeState))
        {
            slimeState = nowState;
        }
    }
}

public partial class Slime : Enemy
{
    public class IdleState : State<Slime>
    {
        public override State<Slime> InputHandle(Slime t)
        {
            // IdleState에서 DeadState로 변화하는 이벤트이다!
            if(t.isDead)
                return new DeadState();

            // 접근 반경 내에 있는 모든 오브젝트를 받아온다...

            foreach (var col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    // IdleState에서 ChaseState로 변화하는 이벤트이다!
                    t.target = col.transform;
                    return new ChaseState();
                }
            }
            return this;
        }

        public override void Update(Slime t)
        {
            base.Update(t);
            // 점프를 하며 맵을 뛰어다닌다...
        }
    }

    public class ChaseState : State<Slime>
    {

        public override State<Slime> InputHandle(Slime t)
        {
            if(t.isDead)
                return new DeadState();

            return this;
        }

        public override void Update(Slime t)
        {
            base.Update(t);
            // 플레이어 방향으로 점프하며 뛰어간다...
        }
    }

    public class DeadState : State<Slime>
    {
        public override State<Slime> InputHandle(Slime t)
        {
            base.Update(t);
            return this;
        }

        public override void Enter(Slime t)
        {
            base.Enter(t);
            // 죽고 난 후 애니메이션을 실행시킨다...
        }
    }
}