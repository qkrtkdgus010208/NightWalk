using System.Collections;
using UnityEngine;

public class ItemSystem_Manager : MonoBehaviour
{
    public static ItemSystem_Manager Instance;

    private Coroutine CanSpawnCor; // 캔 스폰 코루틴
    private Coroutine keySpawnCor; // 키 스폰 코루틴

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
        // 캔 스폰 코루틴 시작
        this.CanSpawnCor = StartCoroutine(this.OnSpawnItem_Cor(DataBase_Manager.Instance.baseCan, DataBase_Manager.Instance.canSpawnInterval));
        // 키 스폰 코루틴 시작
        this.keySpawnCor = StartCoroutine(this.OnSpawnItem_Cor(DataBase_Manager.Instance.baseKey, DataBase_Manager.Instance.keySpawnInterval));
    }

    // 아이템 스폰 코루틴
    private IEnumerator OnSpawnItem_Cor<T>(T _baseItem, float _spawnInterval) where T : Item_Script
    {
        while (true)
        {
            // 아이템 인스턴스 생성 및 활성화
            T _item = Instantiate<T>(_baseItem);
            _item.Activate_Func();

            // 스폰 간격 대기
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 코루틴 중지
            StopCoroutine(this.CanSpawnCor);
            StopCoroutine(this.keySpawnCor);
        }

        // 코루틴 변수 초기화
        this.CanSpawnCor = null;
        this.keySpawnCor = null;
    }
}