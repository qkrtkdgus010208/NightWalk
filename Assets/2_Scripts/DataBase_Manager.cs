using UnityEngine;

public partial class DataBase_Manager : DB_Manager
{
    [Header("플레이어")]
    public float moveSpeed = 5; // 플레이어 이동 속도
    public float stepSfxInterval = 0.5f; // 발소리 효과음 간격
    public int goal = 10; // 목표 점수

    [Header("나무")]
    public int spawnTreeCount = 20; // 나무 스폰 개수
    public Tree_Script baseTree = null; // 기본 나무 프리팹

    [Header("몬스터")]
    public int monsterMaxCount = 10; // 몬스터 최대 개수
    public float monsterSpawnInterval = 10f; // 몬스터 스폰 간격
    public float monsterMoveSpeed = 1f; // 몬스터 이동 속도
    public float monsterRunSpeed = 2f; // 몬스터 달리기 속도
    public Monster_Script baseMonster = null; // 기본 몬스터 프리팹

    [Header("열쇠")]
    public float keySpawnInterval = 1f; // 열쇠 스폰 간격
    public float keyDuration = 10f; // 열쇠 지속 시간
    public float keyLightRadius = 5f; // 열쇠 라이트 반경
    public Key_Script baseKey = null; // 기본 열쇠 프리팹

    [Header("깡통")]
    public float canSpawnInterval = 1f; // 깡통 스폰 간격
    public float canDuration = 10f; // 깡통 지속 시간
    public float canCollideDuration = 3f; // 깡통 충돌 지속 시간
    public float notifyDistance = 1f; // 몬스터 알림 거리
    public Can_Script baseCan = null; // 기본 깡통 프리팹
    [SerializeField] private SfxType[] canSfxTypeArr = null; // 깡통 효과음 타입 배열
    [SerializeField] private Sprite collideCanSpriteArr = null; // 충돌 시 깡통 스프라이트

    // 랜덤한 깡통 효과음 타입을 반환하는 함수
    public SfxType GetRandomCanSfxType_Func()
    {
        return this.canSfxTypeArr[Random.Range(0, this.canSfxTypeArr.Length)];
    }

    // 충돌 시 깡통 스프라이트를 반환하는 함수
    public Sprite GetCollideCanSprite_Func()
    {
        return this.collideCanSpriteArr;
    }
}