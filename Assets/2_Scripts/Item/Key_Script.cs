using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Key_Script : Item_Script
{
    [SerializeField] private Light2D light2D = null; // 2D 라이트 컴포넌트

    // 키의 지속 시간 가져오기
    protected override float GetDurationTime => DataBase_Manager.Instance.keyDuration;

    // 시간이 경과할 때 호출되는 함수
    protected override void OnPassTime_Func(float _per)
    {
        // 부모 클래스의 OnPassTime_Func 호출
        base.OnPassTime_Func(_per);

        // 라이트의 반경을 시간 경과에 따라 줄이기
        this.light2D.pointLightOuterRadius = DataBase_Manager.Instance.keyLightRadius * _per;
    }

    // 플레이어와 충돌했을 때 호출되는 함수
    public override void OnCollidePlayer_Func()
    {
        // 키 효과음 재생
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.Key);

        // 점수 추가
        GameSystem_Manager.Instance.AddScore_Func(1);

        // 아이템 비활성화
        base.Deactivate_Func();
    }
}