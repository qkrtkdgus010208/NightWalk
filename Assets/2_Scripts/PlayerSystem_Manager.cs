using UnityEngine;

public class PlayerSystem_Manager : MonoBehaviour
{
    public static PlayerSystem_Manager Instance;

    [SerializeField] private Animator anim = null; // 애니메이터 컴포넌트
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러 컴포넌트
    [SerializeField] private Transform lightTrf = null; // 라이트 트랜스폼
    [SerializeField] private Transform[] lightPivotTrfArr = null; // 라이트 피벗 트랜스폼 배열
    [SerializeField] private SfxType[] stepSfxTypeArr = null; // 발소리 효과음 타입 배열
    private float stepSfxInterval = 0f; // 발소리 효과음 간격
    private bool isAlive = true; // 플레이어 생존 여부

    // 초기화 함수
    public void Init_Func()
    {
        // 싱글톤 인스턴스 설정
        Instance = this;

        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public void Activate_Func()
    {
        // 플레이어 생존 여부 설정
        this.isAlive = true;
    }

    // 매 프레임마다 호출되는 업데이트 함수
    private void Update()
    {
        // 플레이어가 살아있지 않으면 함수 종료
        if (!this.isAlive)
            return;

        Vector2 _moveDir = Vector2.zero; // 이동 방향
        string _aniTriggerStr = string.Empty; // 애니메이션 트리거 문자열
        Transform _lightPivotTrf = null; // 라이트 피벗 트랜스폼

        // 이동 입력 처리
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _moveDir = Vector2.up; // 위쪽으로 이동
            _aniTriggerStr = "Up"; // 애니메이션 트리거 설정
            _lightPivotTrf = this.srdr.flipX ? this.lightPivotTrfArr[0] : this.lightPivotTrfArr[1]; // 라이트 피벗 설정
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _moveDir = Vector2.left; // 왼쪽으로 이동
            _aniTriggerStr = "Side"; // 애니메이션 트리거 설정
            this.srdr.flipX = false; // 스프라이트 좌우 반전 해제
            _lightPivotTrf = this.lightPivotTrfArr[2]; // 라이트 피벗 설정
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _moveDir = Vector2.down; // 아래쪽으로 이동
            _aniTriggerStr = "Down"; // 애니메이션 트리거 설정
            _lightPivotTrf = this.srdr.flipX ? this.lightPivotTrfArr[4] : this.lightPivotTrfArr[3]; // 라이트 피벗 설정
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _moveDir = Vector2.right; // 오른쪽으로 이동
            _aniTriggerStr = "Side"; // 애니메이션 트리거 설정
            this.srdr.flipX = true; // 스프라이트 좌우 반전 설정
            _lightPivotTrf = this.lightPivotTrfArr[5]; // 라이트 피벗 설정
        }

        // 이동 및 애니메이션 처리
        if (!string.IsNullOrEmpty(_aniTriggerStr))
        {
            Vector2 _translationValue = _moveDir * DataBase_Manager.Instance.moveSpeed * Time.deltaTime; // 이동 값 계산
            Vector2 _arrivePos = (Vector2)this.transform.position + _translationValue; // 도착 위치 계산

            Vector2 _mapMinPos = Background_Manager.Instance.GetMapMinPos_Func(); // 맵 최소 위치
            Vector2 _mapMaxPos = Background_Manager.Instance.GetMapMaxPos_Func(); // 맵 최대 위치

            // 맵 경계 처리
            if (_arrivePos.x < _mapMinPos.x)
                _arrivePos.x = _mapMinPos.x;
            else if (_arrivePos.x > _mapMaxPos.x)
                _arrivePos.x = _mapMaxPos.x;

            if (_arrivePos.y < _mapMinPos.y)
                _arrivePos.y = _mapMinPos.y;
            else if (_arrivePos.y > _mapMaxPos.y)
                _arrivePos.y = _mapMaxPos.y;

            // 플레이어 위치 설정
            this.transform.position = _arrivePos;

            // 스프라이트 정렬 순서 설정
            this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);

            // 애니메이션 트리거 설정
            this.anim.SetTrigger(_aniTriggerStr);

            // 라이트 위치 및 회전 설정
            this.lightTrf.SetParent(_lightPivotTrf);
            this.lightTrf.localPosition = Vector3.zero; // 위치 초기화 (0,0,0)
            this.lightTrf.localRotation = Quaternion.identity; // 회전 초기화 (0,0,0,1)

            // 발소리 효과음 재생
            if (this.stepSfxInterval <= 0f)
            {
                this.stepSfxInterval = DataBase_Manager.Instance.stepSfxInterval; // 발소리 간격 초기화
                SfxType _sfxType = this.stepSfxTypeArr[Random.Range(0, this.stepSfxTypeArr.Length)]; // 랜덤 발소리 타입 선택
                SoundSystem_Manager.Instance.PlaySfx_Func(_sfxType); // 발소리 재생
            }
            else
                this.stepSfxInterval -= Time.deltaTime; // 발소리 간격 감소
        }
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D _col)
    {
        string _tagStr = _col.tag; // 충돌한 객체의 태그

        switch (_tagStr)
        {
            case "Monster":
                {
                    if (_col.TryGetComponent(out Monster_Script _monster))
                        _monster.OnCollidePlayer_Func(); // 몬스터와 충돌 처리
                }
                break;

            case "Can":
                {
                    if (_col.TryGetComponent(out Can_Script _can))
                        _can.OnCollidePlayer_Func(); // 캔과 충돌 처리
                }
                break;

            case "Key":
                {
                    if (_col.TryGetComponent(out Key_Script _key))
                        _key.OnCollidePlayer_Func(); // 키와 충돌 처리
                }
                break;
        }
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 추가적인 비활성화 로직이 여기에 들어갈 수 있습니다.
        }

        // 플레이어 생존 여부 설정
        this.isAlive = false;
    }
}