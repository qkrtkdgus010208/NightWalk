using System.Collections;
using UnityEngine;

public class Can_Script : Item_Script
{
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러
    private bool isCollide = false; // 충돌 여부

    // 캔의 지속 시간 가져오기
    protected override float GetDurationTime => DataBase_Manager.Instance.canDuration;

    // 활성화 함수
    public override void Activate_Func()
    {
        // 충돌 여부 초기화
        this.isCollide = false;
        // 부모 클래스의 Activate_Func 호출
        base.Activate_Func();
    }

    // 플레이어와 충돌했을 때 호출되는 함수
    public override void OnCollidePlayer_Func()
    {
        // 이미 충돌한 경우 함수 종료
        if (this.isCollide)
            return;

        // 충돌 여부 설정
        this.isCollide = true;

        // 충돌 시 스프라이트 변경
        this.srdr.sprite = DataBase_Manager.Instance.GetCollideCanSprite_Func();

        // 랜덤한 캔 효과음 재생
        SfxType _canSfxType = DataBase_Manager.Instance.GetRandomCanSfxType_Func();
        SoundSystem_Manager.Instance.PlaySfx_Func(_canSfxType);

        // 몬스터 시스템에 알림
        MonsterSystem_Manager.Instance.OnNotifyByCan_Func(this.transform.position);

        // 충돌 지속 시간 코루틴 시작
        StartCoroutine(OnCollideDuration_Cor());
    }

    // 충돌 지속 시간 코루틴
    private IEnumerator OnCollideDuration_Cor()
    {
        // 충돌 지속 시간 대기
        yield return new WaitForSeconds(DataBase_Manager.Instance.canCollideDuration);

        // 아이템 비활성화
        base.Deactivate_Func();
    }
}