using UnityEngine;

public class Tree_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // ��������Ʈ ������

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public void Activate_Func(bool _isRand)
    {
        // ���� ��ġ ������ �ʿ��� ���
        if (_isRand)
            this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // ��������Ʈ�� ���� ���� ����
        this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // �߰����� ��Ȱ��ȭ ������ ���⿡ �� �� �ֽ��ϴ�.
        }
    }
}