using UnityEngine;

public class PlayerSystem_Manager : MonoBehaviour
{
    public static PlayerSystem_Manager Instance;

    [SerializeField] private Animator anim = null; // �ִϸ����� ������Ʈ
    [SerializeField] private SpriteRenderer srdr = null; // ��������Ʈ ������ ������Ʈ
    [SerializeField] private Transform lightTrf = null; // ����Ʈ Ʈ������
    [SerializeField] private Transform[] lightPivotTrfArr = null; // ����Ʈ �ǹ� Ʈ������ �迭
    [SerializeField] private SfxType[] stepSfxTypeArr = null; // �߼Ҹ� ȿ���� Ÿ�� �迭
    private float stepSfxInterval = 0f; // �߼Ҹ� ȿ���� ����
    private bool isAlive = true; // �÷��̾� ���� ����

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
        // �÷��̾� ���� ���� ����
        this.isAlive = true;
    }

    // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�
    private void Update()
    {
        // �÷��̾ ������� ������ �Լ� ����
        if (!this.isAlive)
            return;

        Vector2 _moveDir = Vector2.zero; // �̵� ����
        string _aniTriggerStr = string.Empty; // �ִϸ��̼� Ʈ���� ���ڿ�
        Transform _lightPivotTrf = null; // ����Ʈ �ǹ� Ʈ������

        // �̵� �Է� ó��
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _moveDir = Vector2.up; // �������� �̵�
            _aniTriggerStr = "Up"; // �ִϸ��̼� Ʈ���� ����
            _lightPivotTrf = this.srdr.flipX ? this.lightPivotTrfArr[0] : this.lightPivotTrfArr[1]; // ����Ʈ �ǹ� ����
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _moveDir = Vector2.left; // �������� �̵�
            _aniTriggerStr = "Side"; // �ִϸ��̼� Ʈ���� ����
            this.srdr.flipX = false; // ��������Ʈ �¿� ���� ����
            _lightPivotTrf = this.lightPivotTrfArr[2]; // ����Ʈ �ǹ� ����
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _moveDir = Vector2.down; // �Ʒ������� �̵�
            _aniTriggerStr = "Down"; // �ִϸ��̼� Ʈ���� ����
            _lightPivotTrf = this.srdr.flipX ? this.lightPivotTrfArr[4] : this.lightPivotTrfArr[3]; // ����Ʈ �ǹ� ����
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _moveDir = Vector2.right; // ���������� �̵�
            _aniTriggerStr = "Side"; // �ִϸ��̼� Ʈ���� ����
            this.srdr.flipX = true; // ��������Ʈ �¿� ���� ����
            _lightPivotTrf = this.lightPivotTrfArr[5]; // ����Ʈ �ǹ� ����
        }

        // �̵� �� �ִϸ��̼� ó��
        if (!string.IsNullOrEmpty(_aniTriggerStr))
        {
            Vector2 _translationValue = _moveDir * DataBase_Manager.Instance.moveSpeed * Time.deltaTime; // �̵� �� ���
            Vector2 _arrivePos = (Vector2)this.transform.position + _translationValue; // ���� ��ġ ���

            Vector2 _mapMinPos = Background_Manager.Instance.GetMapMinPos_Func(); // �� �ּ� ��ġ
            Vector2 _mapMaxPos = Background_Manager.Instance.GetMapMaxPos_Func(); // �� �ִ� ��ġ

            // �� ��� ó��
            if (_arrivePos.x < _mapMinPos.x)
                _arrivePos.x = _mapMinPos.x;
            else if (_arrivePos.x > _mapMaxPos.x)
                _arrivePos.x = _mapMaxPos.x;

            if (_arrivePos.y < _mapMinPos.y)
                _arrivePos.y = _mapMinPos.y;
            else if (_arrivePos.y > _mapMaxPos.y)
                _arrivePos.y = _mapMaxPos.y;

            // �÷��̾� ��ġ ����
            this.transform.position = _arrivePos;

            // ��������Ʈ ���� ���� ����
            this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);

            // �ִϸ��̼� Ʈ���� ����
            this.anim.SetTrigger(_aniTriggerStr);

            // ����Ʈ ��ġ �� ȸ�� ����
            this.lightTrf.SetParent(_lightPivotTrf);
            this.lightTrf.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ (0,0,0)
            this.lightTrf.localRotation = Quaternion.identity; // ȸ�� �ʱ�ȭ (0,0,0,1)

            // �߼Ҹ� ȿ���� ���
            if (this.stepSfxInterval <= 0f)
            {
                this.stepSfxInterval = DataBase_Manager.Instance.stepSfxInterval; // �߼Ҹ� ���� �ʱ�ȭ
                SfxType _sfxType = this.stepSfxTypeArr[Random.Range(0, this.stepSfxTypeArr.Length)]; // ���� �߼Ҹ� Ÿ�� ����
                SoundSystem_Manager.Instance.PlaySfx_Func(_sfxType); // �߼Ҹ� ���
            }
            else
                this.stepSfxInterval -= Time.deltaTime; // �߼Ҹ� ���� ����
        }
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D _col)
    {
        string _tagStr = _col.tag; // �浹�� ��ü�� �±�

        switch (_tagStr)
        {
            case "Monster":
                {
                    if (_col.TryGetComponent(out Monster_Script _monster))
                        _monster.OnCollidePlayer_Func(); // ���Ϳ� �浹 ó��
                }
                break;

            case "Can":
                {
                    if (_col.TryGetComponent(out Can_Script _can))
                        _can.OnCollidePlayer_Func(); // ĵ�� �浹 ó��
                }
                break;

            case "Key":
                {
                    if (_col.TryGetComponent(out Key_Script _key))
                        _key.OnCollidePlayer_Func(); // Ű�� �浹 ó��
                }
                break;
        }
    }

    // ��Ȱ��ȭ �Լ�
    public void Deactivate_Func(bool _isInit = false)
    {
        // �ʱ�ȭ �ð� �ƴ� ��쿡�� ����
        if (!_isInit)
        {
            // �߰����� ��Ȱ��ȭ ������ ���⿡ �� �� �ֽ��ϴ�.
        }

        // �÷��̾� ���� ���� ����
        this.isAlive = false;
    }
}