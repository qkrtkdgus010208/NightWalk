using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem_Manager : Library_C.GameSystem_Manager
{
    public static GameSystem_Manager Instance;

    [SerializeField] private DataBase_Manager dataBase_Manager = null; // 데이터베이스 매니저
    [SerializeField] private PlayerSystem_Manager playerSystem_Manager = null; // 플레이어 시스템 매니저
    [SerializeField] private Background_Manager background_Manager = null; // 배경 매니저
    [SerializeField] private MonsterSystem_Manager monsterSystem_Manager = null; // 몬스터 시스템 매니저
    [SerializeField] private Animator gameOverAnim = null; // 게임 오버 애니메이터
    [SerializeField] private ItemSystem_Manager itemSystem_Manager = null; // 아이템 시스템 매니저
    [SerializeField] private TextMeshProUGUI scoreTmp = null; // 점수 텍스트
    [SerializeField] private GameObject clearobj = null; // 클리어 오브젝트
    private int currentScore = 0; // 현재 점수

    // 초기화 함수
    public override void Init_Func()
    {
        // 싱글톤 인스턴스 설정
        Instance = this;

        // 각 매니저 초기화
        this.dataBase_Manager.Init_Func();
        this.playerSystem_Manager.Init_Func();
        this.background_Manager.Init_Func();
        this.monsterSystem_Manager.Init_Func();
        this.gameOverAnim.gameObject.SetActive(false); // 게임 오버 애니메이션 비활성화
        this.itemSystem_Manager.Init_Func();
        this.clearobj.SetActive(false); // 클리어 오브젝트 비활성화

        // 부모 클래스의 초기화 함수 호출
        base.Init_Func();

        // 활성화 함수 호출
        this.Activate_Func();
    }

    // 활성화 함수
    public override void Activate_Func()
    {
        // 부모 클래스의 활성화 함수 호출
        base.Activate_Func();

        // 각 매니저 활성화
        this.playerSystem_Manager.Activate_Func();
        this.background_Manager.Activate_Func();
        this.monsterSystem_Manager.Activate_Func();
        this.itemSystem_Manager.Activate_Func();

        // 점수 초기화 및 텍스트 업데이트
        this.currentScore = 0;
        this.scoreTmp.text = $"{this.currentScore}/{DataBase_Manager.Instance.goal}";

        // 배경 음악 재생
        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Ingame);
    }

    // 점수 추가 함수
    public void AddScore_Func(int _score)
    {
        // 점수 추가 및 텍스트 업데이트
        currentScore += _score;
        this.scoreTmp.text = $"{this.currentScore}/{DataBase_Manager.Instance.goal}";

        // 목표 점수에 도달하면 클리어 처리
        if (this.currentScore >= DataBase_Manager.Instance.goal)
        {
            this.clearobj.SetActive(true); // 클리어 오브젝트 활성화

            // 비활성화 함수 호출
            this.Deactivate_Func();
        }
    }

    // 게임 오버 처리 함수
    public void OnGameOver_Func()
    {
        // 게임 오버 효과음 재생
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.GameOver);

        // 게임 오버 애니메이션 활성화
        this.gameOverAnim.gameObject.SetActive(true);

        // 비활성화 함수 호출
        this.Deactivate_Func();
    }

    // 비활성화 함수
    public override void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 각 매니저 비활성화
            this.playerSystem_Manager.Deactivate_Func();
            this.background_Manager.Deactivate_Func();
            this.monsterSystem_Manager.Deactivate_Func();
            this.itemSystem_Manager.Deactivate_Func();
        }

        // 부모 클래스의 비활성화 함수 호출
        base.Deactivate_Func(_isInit);
    }

    // 재시작 버튼 클릭 시 호출되는 함수
    public void CallBtn_Retry_Func()
    {
        // 씬 다시 로드
        SceneManager.LoadScene(0);
    }
}