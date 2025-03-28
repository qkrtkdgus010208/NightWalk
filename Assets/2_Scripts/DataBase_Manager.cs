using UnityEngine;

public partial class DataBase_Manager : DB_Manager
{
    [Header("�÷��̾�")]
    public float moveSpeed = 5; // �÷��̾� �̵� �ӵ�
    public float stepSfxInterval = 0.5f; // �߼Ҹ� ȿ���� ����
    public int goal = 10; // ��ǥ ����

    [Header("����")]
    public int spawnTreeCount = 20; // ���� ���� ����
    public Tree_Script baseTree = null; // �⺻ ���� ������

    [Header("����")]
    public int monsterMaxCount = 10; // ���� �ִ� ����
    public float monsterSpawnInterval = 10f; // ���� ���� ����
    public float monsterMoveSpeed = 1f; // ���� �̵� �ӵ�
    public float monsterRunSpeed = 2f; // ���� �޸��� �ӵ�
    public Monster_Script baseMonster = null; // �⺻ ���� ������

    [Header("����")]
    public float keySpawnInterval = 1f; // ���� ���� ����
    public float keyDuration = 10f; // ���� ���� �ð�
    public float keyLightRadius = 5f; // ���� ����Ʈ �ݰ�
    public Key_Script baseKey = null; // �⺻ ���� ������

    [Header("����")]
    public float canSpawnInterval = 1f; // ���� ���� ����
    public float canDuration = 10f; // ���� ���� �ð�
    public float canCollideDuration = 3f; // ���� �浹 ���� �ð�
    public float notifyDistance = 1f; // ���� �˸� �Ÿ�
    public Can_Script baseCan = null; // �⺻ ���� ������
    [SerializeField] private SfxType[] canSfxTypeArr = null; // ���� ȿ���� Ÿ�� �迭
    [SerializeField] private Sprite collideCanSpriteArr = null; // �浹 �� ���� ��������Ʈ

    // ������ ���� ȿ���� Ÿ���� ��ȯ�ϴ� �Լ�
    public SfxType GetRandomCanSfxType_Func()
    {
        return this.canSfxTypeArr[Random.Range(0, this.canSfxTypeArr.Length)];
    }

    // �浹 �� ���� ��������Ʈ�� ��ȯ�ϴ� �Լ�
    public Sprite GetCollideCanSprite_Func()
    {
        return this.collideCanSpriteArr;
    }
}