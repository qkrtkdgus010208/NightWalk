using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Key_Script : Item_Script
{
    [SerializeField] private Light2D light2D = null; // 2D ����Ʈ ������Ʈ

    // Ű�� ���� �ð� ��������
    protected override float GetDurationTime => DataBase_Manager.Instance.keyDuration;

    // �ð��� ����� �� ȣ��Ǵ� �Լ�
    protected override void OnPassTime_Func(float _per)
    {
        // �θ� Ŭ������ OnPassTime_Func ȣ��
        base.OnPassTime_Func(_per);

        // ����Ʈ�� �ݰ��� �ð� ����� ���� ���̱�
        this.light2D.pointLightOuterRadius = DataBase_Manager.Instance.keyLightRadius * _per;
    }

    // �÷��̾�� �浹���� �� ȣ��Ǵ� �Լ�
    public override void OnCollidePlayer_Func()
    {
        // Ű ȿ���� ���
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.Key);

        // ���� �߰�
        GameSystem_Manager.Instance.AddScore_Func(1);

        // ������ ��Ȱ��ȭ
        base.Deactivate_Func();
    }
}