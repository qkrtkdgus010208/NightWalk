using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem_Manager : Library_C.GameSystem_Manager
{
    public static GameSystem_Manager Instance;

    [SerializeField] private DataBase_Manager dataBase_Manager = null; // �����ͺ��̽� �Ŵ���
    [SerializeField] private PlayerSystem_Manager playerSystem_Manager = null; // �÷��̾� �ý��� �Ŵ���
    [SerializeField] private Background_Manager background_Manager = null; // ��� �Ŵ���
    [SerializeField] private MonsterSystem_Manager monsterSystem_Manager = null; // ���� �ý��� �Ŵ���
    [SerializeField] private Animator gameOverAnim = null; // ���� ���� �ִϸ�����
    [SerializeField] private ItemSystem_Manager itemSystem_Manager = null; // ������ �ý��� �Ŵ���
    [SerializeField] private TextMeshProUGUI scoreTmp = null; // ���� �ؽ�Ʈ
    [SerializeField] private GameObject clearobj = null; // Ŭ���� ������Ʈ
    private int currentScore = 0; // ���� ����

    // �ʱ�ȭ �Լ�
    public override void Init_Func()
    {
        // �̱��� �ν��Ͻ� ����
        Instance = this;

        // �� �Ŵ��� �ʱ�ȭ
        this.dataBase_Manager.Init_Func();
        this.playerSystem_Manager.Init_Func();
        this.background_Manager.Init_Func();
        this.monsterSystem_Manager.Init_Func();
        this.gameOverAnim.gameObject.SetActive(false); // ���� ���� �ִϸ��̼� ��Ȱ��ȭ
        this.itemSystem_Manager.Init_Func();
        this.clearobj.SetActive(false); // Ŭ���� ������Ʈ ��Ȱ��ȭ

        // �θ� Ŭ������ �ʱ�ȭ �Լ� ȣ��
        base.Init_Func();

        // Ȱ��ȭ �Լ� ȣ��
        this.Activate_Func();
    }

    // Ȱ��ȭ �Լ�
    public override void Activate_Func()
    {
        // �θ� Ŭ������ Ȱ��ȭ �Լ� ȣ��
        base.Activate_Func();

        // �� �Ŵ��� Ȱ��ȭ
        this.playerSystem_Manager.Activate_Func();
        this.background_Manager.Activate_Func();
        this.monsterSystem_Manager.Activate_Func();
        this.itemSystem_Manager.Activate_Func();

        // ���� �ʱ�ȭ �� �ؽ�Ʈ ������Ʈ
        this.currentScore = 0;
        this.scoreTmp.text = $"{this.currentScore}/{DataBase_Manager.Instance.goal}";

        // ��� ���� ���
        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Ingame);
    }

    // ���� �߰� �Լ�
    public void AddScore_Func(int _score)
    {
        // ���� �߰� �� �ؽ�Ʈ ������Ʈ
        currentScore += _score;
        this.scoreTmp.text = $"{this.currentScore}/{DataBase_Manager.Instance.goal}";

        // ��ǥ ������ �����ϸ� Ŭ���� ó��
        if (this.currentScore >= DataBase_Manager.Instance.goal)
        {
            this.clearobj.SetActive(true); // Ŭ���� ������Ʈ Ȱ��ȭ

            // ��Ȱ��ȭ �Լ� ȣ��
            this.Deactivate_Func();
        }
    }

    // ���� ���� ó�� �Լ�
    public void OnGameOver_Func()
    {
        // ���� ���� ȿ���� ���
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.GameOver);

        // ���� ���� �ִϸ��̼� Ȱ��ȭ
        this.gameOverAnim.gameObject.SetActive(true);

        // ��Ȱ��ȭ �Լ� ȣ��
        this.Deactivate_Func();
    }

    // ��Ȱ��ȭ �Լ�
    public override void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // �� �Ŵ��� ��Ȱ��ȭ
            this.playerSystem_Manager.Deactivate_Func();
            this.background_Manager.Deactivate_Func();
            this.monsterSystem_Manager.Deactivate_Func();
            this.itemSystem_Manager.Deactivate_Func();
        }

        // �θ� Ŭ������ ��Ȱ��ȭ �Լ� ȣ��
        base.Deactivate_Func(_isInit);
    }

    // ����� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void CallBtn_Retry_Func()
    {
        // �� �ٽ� �ε�
        SceneManager.LoadScene(0);
    }
}