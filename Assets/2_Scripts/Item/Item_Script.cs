using System.Collections;
using UnityEngine;

public abstract class Item_Script : MonoBehaviour
{
    // �������� ���� �ð� �������� (��ӹ��� Ŭ�������� ����)
    protected abstract float GetDurationTime { get; }

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public virtual void Activate_Func()
    {
        // �������� ��ġ�� �����ϰ� ����
        this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // ���� �ð� �ڷ�ƾ ����
        StartCoroutine(this.OnDuration_Cor());
    }

    // ���� �ð� �ڷ�ƾ
    private IEnumerator OnDuration_Cor()
    {
        float _durationTime = this.GetDurationTime; // �������� ���� �ð�
        float _passTime = 0f; // ��� �ð�

        // ���� �ð��� ����� ������ �ݺ�
        while (_passTime < _durationTime)
        {
            _passTime += Time.deltaTime;

            // ��� �ð� ������ ����Ͽ� �Լ� ȣ��
            this.OnPassTime_Func(_passTime / _durationTime);

            yield return null;
        }

        // ���� �ð��� ������ ������ ��Ȱ��ȭ
        this.Deactivate_Func();
    }

    // �ð��� ����� �� ȣ��Ǵ� �Լ� (��ӹ��� Ŭ�������� ����)
    protected virtual void OnPassTime_Func(float _per)
    {

    }

    // �÷��̾�� �浹���� �� ȣ��Ǵ� �Լ� (��ӹ��� Ŭ�������� ����)
    public abstract void OnCollidePlayer_Func();

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