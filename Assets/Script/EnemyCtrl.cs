using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    Status status;
    PlayerAnimation charaAnimation;
    PlayerMove characterMove;
    Transform attackTarget;

    // �ҋ@���Ԃ͂Q�b�Ƃ���
    public float waitBaseTime = 2.0f;
    // �c��ҋ@����
    float waitTime;
    // �ړ��͈͂T���[�g��
    public float walkRange = 5.0f;
    // �����ʒu��ۑ����Ă����ϐ�
    public Vector3 basePosition;
    // �����̃A�C�e���������悤�ɔz��ɂ��܂��傤�B
    public GameObject[] dropItemPrefab;


    // �X�e�[�g�̎��.
    enum State
    {
        Walking,	// �T��
        Chasing,	// �ǐ�
        Attacking,	// �U��
        Died,       // ���S
    };

    State state = State.Walking;        // ���݂̃X�e�[�g.
    State nextState = State.Walking;    // ���̃X�e�[�g.


    // Use this for initialization
    void Start()
    {
        status = GetComponent<Status>();
        charaAnimation = GetComponent<PlayerAnimation>();
        characterMove = GetComponent<PlayerMove>();
        // �����ʒu��ێ�
        basePosition = transform.position;
        // �ҋ@����
        waitTime = waitBaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Walking:
                Walking();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attacking();
                break;
        }

        if (state != nextState)
        {
            state = nextState;
            switch (state)
            {
                case State.Walking:
                    WalkStart();
                    break;
                case State.Chasing:
                    ChaseStart();
                    break;
                case State.Attacking:
                    AttackStart();
                    break;
                case State.Died:
                    Died();
                    break;
            }
        }
    }


    // �X�e�[�g��ύX����.
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    void WalkStart()
    {
        StateStartCommon();
    }

    void Walking()
    {
        // �ҋ@���Ԃ��܂���������
        if (waitTime > 0.0f)
        {
            // �ҋ@���Ԃ����炷
            waitTime -= Time.deltaTime;
            // �ҋ@���Ԃ������Ȃ�����
            if (waitTime <= 0.0f)
            {
                // �͈͓��̉�����
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // �ړ���̐ݒ�
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                //�@�ړI�n�̎w��.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // �ړI�n�֓���
            if (characterMove.Arrived())
            {
                // �ҋ@��Ԃ�
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }
            // �^�[�Q�b�g�𔭌�������ǐ�
            if (attackTarget)
            {
                ChangeState(State.Chasing);
            }
        }
    }
    // �ǐՊJ�n
    void ChaseStart()
    {
        StateStartCommon();
    }
    // �ǐՒ�
    void Chasing()
    {
        // �ړ�����v���C���[�ɐݒ�
        SendMessage("SetDestination", attackTarget.position);
        // 2m�ȓ��ɋ߂Â�����U��
        if (Vector3.Distance(attackTarget.position, transform.position) <= 2.0f)
        {
            ChangeState(State.Attacking);
        }
    }

    // �U���X�e�[�g���n�܂�O�ɌĂяo�����.
    void AttackStart()
    {
        StateStartCommon();
        status.attacking = true;

        // �G�̕����ɐU���������.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        SendMessage("SetDirection", targetDirection);

        // �ړ����~�߂�.
        SendMessage("StopMove");
    }

    // �U�����̏���.
    void Attacking()
    {
        if (charaAnimation.IsAttacked())
            ChangeState(State.Walking);
        // �ҋ@���Ԃ��Đݒ�
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        // �^�[�Q�b�g�����Z�b�g����
        attackTarget = null;
    }

    void dropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    void Died()
    {
        status.died = true;
        dropItem();
        Destroy(gameObject);
    }

    void Damage(AttackArea.AttackInfo attackInfo)
    {
        status.HP -= attackInfo.attackPower;
        if (status.HP <= 0)
        {
            status.HP = 0;
            // �̗͂O�Ȃ̂Ŏ��S
            ChangeState(State.Died);
        }
    }

    // �X�e�[�g���n�܂�O�ɃX�e�[�^�X������������.
    void StateStartCommon()
    {
        status.attacking = false;
        status.died = false;
    }
    // �U���Ώۂ�ݒ肷��
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}
