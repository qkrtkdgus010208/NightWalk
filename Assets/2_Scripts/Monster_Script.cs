using System.Collections;
using UnityEngine;

public class Monster_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // ��������Ʈ ������
    private Vector2 arrivePos; // ������ ���� ��ġ
    private bool isRun = false; // ���Ͱ� �޸����� ����

    // ������ ���� ��ġ�� ��ȯ�ϴ� ������Ƽ
    public Vector2 GetPos => this.transform.position;

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        // ������ ��ġ�� �����ϰ� ����
        this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // �̵� �ڷ�ƾ ����
        StartCoroutine(OnMove_Cor());
    }

    // ���� �̵� �ڷ�ƾ
    private IEnumerator OnMove_Cor()
    {
        // �ʱ� ���� ��ġ ����
        this.arrivePos = Background_Manager.Instance.GetRandomPos_Func();

        while (true)
        {
            Vector2 _thisPos = this.transform.position; // ���� ��ġ

            // ���� ��ġ�� �����ߴ��� Ȯ��
            if (Vector2.Distance(arrivePos, _thisPos) < 0.1f)
            {
                this.isRun = false; // �޸��� ���� ����

                // ���ο� ���� ��ġ ����
                arrivePos = Background_Manager.Instance.GetRandomPos_Func();
            }

            // �̵� ���� �� �ӵ� ���
            Vector2 _moveDir = (arrivePos - _thisPos).normalized;
            float _monsterMoveSpeed = this.isRun ? DataBase_Manager.Instance.monsterRunSpeed : DataBase_Manager.Instance.monsterMoveSpeed;
            Vector2 _moveVelocity = _moveDir * _monsterMoveSpeed * Time.deltaTime;
            this.transform.Translate(_moveVelocity); // ���� �̵�

            // ��������Ʈ ���� ���� ����
            this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);

            yield return null; // ���� �����ӱ��� ���
        }
    }

    // ������ ���� ��ġ�� �����ϴ� �Լ�
    public void SetArrivePos_Func(Vector2 _pos)
    {
        this.isRun = true; // �޸��� ���� ����

        this.arrivePos = _pos; // ���� ��ġ ����
    }

    // �÷��̾�� �浹���� �� ȣ��Ǵ� �Լ�
    public void OnCollidePlayer_Func()
    {
        // ���� ���� ó��
        GameSystem_Manager.Instance.OnGameOver_Func();
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // ���� ������Ʈ �ı�
            Destroy(this.gameObject);
        }
    }
}