using System.Collections;
using UnityEngine;

public class Monster_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러
    private Vector2 arrivePos; // 몬스터의 도착 위치
    private bool isRun = false; // 몬스터가 달리는지 여부

    // 몬스터의 현재 위치를 반환하는 프로퍼티
    public Vector2 GetPos => this.transform.position;

    // 초기화 함수
    public void Init_Func()
    {
        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public void Activate_Func()
    {
        // 몬스터의 위치를 랜덤하게 설정
        this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // 이동 코루틴 시작
        StartCoroutine(OnMove_Cor());
    }

    // 몬스터 이동 코루틴
    private IEnumerator OnMove_Cor()
    {
        // 초기 도착 위치 설정
        this.arrivePos = Background_Manager.Instance.GetRandomPos_Func();

        while (true)
        {
            Vector2 _thisPos = this.transform.position; // 현재 위치

            // 도착 위치에 도달했는지 확인
            if (Vector2.Distance(arrivePos, _thisPos) < 0.1f)
            {
                this.isRun = false; // 달리는 상태 해제

                // 새로운 도착 위치 설정
                arrivePos = Background_Manager.Instance.GetRandomPos_Func();
            }

            // 이동 방향 및 속도 계산
            Vector2 _moveDir = (arrivePos - _thisPos).normalized;
            float _monsterMoveSpeed = this.isRun ? DataBase_Manager.Instance.monsterRunSpeed : DataBase_Manager.Instance.monsterMoveSpeed;
            Vector2 _moveVelocity = _moveDir * _monsterMoveSpeed * Time.deltaTime;
            this.transform.Translate(_moveVelocity); // 몬스터 이동

            // 스프라이트 정렬 순서 설정
            this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);

            yield return null; // 다음 프레임까지 대기
        }
    }

    // 몬스터의 도착 위치를 설정하는 함수
    public void SetArrivePos_Func(Vector2 _pos)
    {
        this.isRun = true; // 달리는 상태 설정

        this.arrivePos = _pos; // 도착 위치 설정
    }

    // 플레이어와 충돌했을 때 호출되는 함수
    public void OnCollidePlayer_Func()
    {
        // 게임 오버 처리
        GameSystem_Manager.Instance.OnGameOver_Func();
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 게임 오브젝트 파괴
            Destroy(this.gameObject);
        }
    }
}