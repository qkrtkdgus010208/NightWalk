using System.Collections;
using UnityEngine;

public abstract class Item_Script : MonoBehaviour
{
    // 아이템의 지속 시간 가져오기 (상속받은 클래스에서 구현)
    protected abstract float GetDurationTime { get; }

    // 초기화 함수
    public void Init_Func()
    {
        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public virtual void Activate_Func()
    {
        // 아이템의 위치를 랜덤하게 설정
        this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // 지속 시간 코루틴 시작
        StartCoroutine(this.OnDuration_Cor());
    }

    // 지속 시간 코루틴
    private IEnumerator OnDuration_Cor()
    {
        float _durationTime = this.GetDurationTime; // 아이템의 지속 시간
        float _passTime = 0f; // 경과 시간

        // 지속 시간이 경과할 때까지 반복
        while (_passTime < _durationTime)
        {
            _passTime += Time.deltaTime;

            // 경과 시간 비율을 계산하여 함수 호출
            this.OnPassTime_Func(_passTime / _durationTime);

            yield return null;
        }

        // 지속 시간이 끝나면 아이템 비활성화
        this.Deactivate_Func();
    }

    // 시간이 경과할 때 호출되는 함수 (상속받은 클래스에서 구현)
    protected virtual void OnPassTime_Func(float _per)
    {

    }

    // 플레이어와 충돌했을 때 호출되는 함수 (상속받은 클래스에서 구현)
    public abstract void OnCollidePlayer_Func();

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