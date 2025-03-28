using System.Collections;
using UnityEngine;

public class Can_Script : Item_Script
{
    [SerializeField] private SpriteRenderer srdr = null; // ��������Ʈ ������
    private bool isCollide = false; // �浹 ����

    // ĵ�� ���� �ð� ��������
    protected override float GetDurationTime => DataBase_Manager.Instance.canDuration;

    // Ȱ��ȭ �Լ�
    public override void Activate_Func()
    {
        // �浹 ���� �ʱ�ȭ
        this.isCollide = false;
        // �θ� Ŭ������ Activate_Func ȣ��
        base.Activate_Func();
    }

    // �÷��̾�� �浹���� �� ȣ��Ǵ� �Լ�
    public override void OnCollidePlayer_Func()
    {
        // �̹� �浹�� ��� �Լ� ����
        if (this.isCollide)
            return;

        // �浹 ���� ����
        this.isCollide = true;

        // �浹 �� ��������Ʈ ����
        this.srdr.sprite = DataBase_Manager.Instance.GetCollideCanSprite_Func();

        // ������ ĵ ȿ���� ���
        SfxType _canSfxType = DataBase_Manager.Instance.GetRandomCanSfxType_Func();
        SoundSystem_Manager.Instance.PlaySfx_Func(_canSfxType);

        // ���� �ý��ۿ� �˸�
        MonsterSystem_Manager.Instance.OnNotifyByCan_Func(this.transform.position);

        // �浹 ���� �ð� �ڷ�ƾ ����
        StartCoroutine(OnCollideDuration_Cor());
    }

    // �浹 ���� �ð� �ڷ�ƾ
    private IEnumerator OnCollideDuration_Cor()
    {
        // �浹 ���� �ð� ���
        yield return new WaitForSeconds(DataBase_Manager.Instance.canCollideDuration);

        // ������ ��Ȱ��ȭ
        base.Deactivate_Func();
    }
}