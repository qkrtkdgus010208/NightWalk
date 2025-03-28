using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Background_Manager : MonoBehaviour
{
    public static Background_Manager Instance;

    [SerializeField] private Tree_Script[] treeArr = null; // ������ ���� �迭
    [SerializeField] private Transform[] mapRangeTrfArr = null; // �� ���� Ʈ������ �迭
    [SerializeField] private Animation[] grassAnimArr = null; // Ǯ �ִϸ��̼� �迭
    [SerializeField] private Transform randomTreeGroup = null; // ���� ���� �׷� Ʈ������
    private List<Tree_Script> randomTreeArr = null; // ���� ���� ����Ʈ

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        // �̱��� �ν��Ͻ� ����
        Instance = this;

        // ���� ���� ����Ʈ �ʱ�ȭ
        randomTreeArr = new List<Tree_Script>();

        // ������ ������ŭ ���� ���� ����
        for (int i = 0; i < DataBase_Manager.Instance.spawnTreeCount; i++)
        {
            Tree_Script _baseTree = DataBase_Manager.Instance.baseTree;
            Tree_Script _tree = Instantiate(_baseTree);
            _tree.transform.SetParent(randomTreeGroup);
            randomTreeArr.Add(_tree);
        }

        // ������ ���� �ʱ�ȭ
        foreach (Tree_Script _tree in this.treeArr)
            _tree.Init_Func();

        // ���� ���� �ʱ�ȭ
        foreach (Tree_Script _tree in this.randomTreeArr)
            _tree.Init_Func();

        // Ǯ �ִϸ��̼� �ӵ� ���� ����
        foreach (Animation _grassAnim in this.grassAnimArr)
        {
            AnimationState _state = _grassAnim[_grassAnim.clip.name];
            _state.speed = Random.Range(0.8f, 1.2f);
        }

        // ��Ȱ��ȭ �Լ� ȣ�� (�ʱ�ȭ ��)
        this.Deactivate_Func(true);
    }

    // Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        // ������ ���� Ȱ��ȭ
        foreach (Tree_Script _tree in this.treeArr)
            _tree.Activate_Func(false);

        // ���� ���� Ȱ��ȭ
        foreach (Tree_Script _tree in this.randomTreeArr)
            _tree.Activate_Func(true);
    }

    // ���� �ּ� ��ġ ��ȯ �Լ�
    public Vector2 GetMapMinPos_Func()
    {
        return this.mapRangeTrfArr[0].position;
    }

    // ���� �ִ� ��ġ ��ȯ �Լ�
    public Vector2 GetMapMaxPos_Func()
    {
        return this.mapRangeTrfArr[1].position;
    }

    // ���� ��ġ ��ȯ �Լ�
    public Vector2 GetRandomPos_Func()
    {
        Vector2 _minPos = this.GetMapMinPos_Func();
        Vector2 _maxPos = this.GetMapMaxPos_Func();
        float _xPos = Random.Range(_minPos.x, _maxPos.x);
        float _yPos = Random.Range(_minPos.y, _maxPos.y);
        return new Vector2(_xPos, _yPos);
    }

    // ���� ���� ��ȯ �Լ�
    public int GetOrderLayer_Func(Transform _trf)
    {
        return -(int)(_trf.position.y * 10f);
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // ������ ���� ��Ȱ��ȭ
            foreach (Tree_Script _tree in this.treeArr)
                _tree.Deactivate_Func();

            // ���� ���� ��Ȱ��ȭ
            foreach (Tree_Script _tree in this.randomTreeArr)
                _tree.Deactivate_Func();
        }
    }
}