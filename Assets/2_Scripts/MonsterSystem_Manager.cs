using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem_Manager : MonoBehaviour
{
    public static MonsterSystem_Manager Instance;

    private List<Monster_Script> monsterList; // ���� ����Ʈ
    private Coroutine spawnCor; // ���� ���� �ڷ�ƾ

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // �̱��� �ν��Ͻ� ����
        Instance = this;

        // ���� ����Ʈ �ʱ�ȭ
        this.monsterList = new List<Monster_Script>();

        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        // ���� ���� �ڷ�ƾ ����
        this.spawnCor = StartCoroutine(OnSpawn_Cor());
    }

    // ���� ���� �ڷ�ƾ
    private IEnumerator OnSpawn_Cor()
    {
        // ���� �ִ� ���� ������ ������ �ݺ�
        while (this.monsterList.Count < DataBase_Manager.Instance.monsterMaxCount)
        {
            // ���� �ν��Ͻ� ���� �� Ȱ��ȭ
            Monster_Script _baseMonster = DataBase_Manager.Instance.baseMonster;
            Monster_Script _monster = Instantiate<Monster_Script>(_baseMonster);
            _monster.Activate_Func();

            // ���� ����Ʈ�� �߰�
            this.monsterList.Add(_monster);

            // ���� ���� ���
            yield return new WaitForSeconds(DataBase_Manager.Instance.monsterSpawnInterval);
        }
    }

    // ĵ�� ���� ���͸� �˸�
    public void OnNotifyByCan_Func(Vector2 _canPos)
    {
        bool _isNotify = false; // �˸� ����

        // ��� ���Ϳ� ���� �ݺ�
        foreach (Monster_Script _monster in this.monsterList)
        {
            // ���Ͱ� ĵ ��ġ�� ���� �Ÿ� �̳��� �ִ��� Ȯ��
            if (Vector2.Distance(_monster.GetPos, _canPos) < DataBase_Manager.Instance.notifyDistance)
            {
                _isNotify = true;

                // ������ ���� ��ġ ����
                _monster.SetArrivePos_Func(_canPos);
            }
        }

        // �˸��� �߻��� ��� ȿ���� ���
        if (_isNotify)
            SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.CanNotify);
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // ��� ���� ��Ȱ��ȭ
            foreach (Monster_Script _monster in monsterList)
                _monster.Deactivate_Func();

            // ���� ���� �ڷ�ƾ ����
            StopCoroutine(this.spawnCor);
        }

        // �ڷ�ƾ ���� �ʱ�ȭ
        this.spawnCor = null;

        // ���� ����Ʈ �ʱ�ȭ
        this.monsterList.Clear();
    }
}