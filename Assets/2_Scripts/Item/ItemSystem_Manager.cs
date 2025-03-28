using System.Collections;
using UnityEngine;

public class ItemSystem_Manager : MonoBehaviour
{
    public static ItemSystem_Manager Instance;

    private Coroutine CanSpawnCor; // ĵ ���� �ڷ�ƾ
    private Coroutine keySpawnCor; // Ű ���� �ڷ�ƾ

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // �̱��� �ν��Ͻ� ����
        Instance = this;

        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        // ĵ ���� �ڷ�ƾ ����
        this.CanSpawnCor = StartCoroutine(this.OnSpawnItem_Cor(DataBase_Manager.Instance.baseCan, DataBase_Manager.Instance.canSpawnInterval));
        // Ű ���� �ڷ�ƾ ����
        this.keySpawnCor = StartCoroutine(this.OnSpawnItem_Cor(DataBase_Manager.Instance.baseKey, DataBase_Manager.Instance.keySpawnInterval));
    }

    // ������ ���� �ڷ�ƾ
    private IEnumerator OnSpawnItem_Cor<T>(T _baseItem, float _spawnInterval) where T : Item_Script
    {
        while (true)
        {
            // ������ �ν��Ͻ� ���� �� Ȱ��ȭ
            T _item = Instantiate<T>(_baseItem);
            _item.Activate_Func();

            // ���� ���� ���
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // �ڷ�ƾ ����
            StopCoroutine(this.CanSpawnCor);
            StopCoroutine(this.keySpawnCor);
        }

        // �ڷ�ƾ ���� �ʱ�ȭ
        this.CanSpawnCor = null;
        this.keySpawnCor = null;
    }
}