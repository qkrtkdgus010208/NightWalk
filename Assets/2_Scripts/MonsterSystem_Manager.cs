using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem_Manager : MonoBehaviour
{
    public static MonsterSystem_Manager Instance;

    private List<Monster_Script> monsterList; // 몬스터 리스트
    private Coroutine spawnCor; // 몬스터 스폰 코루틴

    // 초기화 함수
    public void Init_Func()
    {
        // 싱글톤 인스턴스 설정
        Instance = this;

        // 몬스터 리스트 초기화
        this.monsterList = new List<Monster_Script>();

        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public void Activate_Func()
    {
        // 몬스터 스폰 코루틴 시작
        this.spawnCor = StartCoroutine(OnSpawn_Cor());
    }

    // 몬스터 스폰 코루틴
    private IEnumerator OnSpawn_Cor()
    {
        // 몬스터 최대 수에 도달할 때까지 반복
        while (this.monsterList.Count < DataBase_Manager.Instance.monsterMaxCount)
        {
            // 몬스터 인스턴스 생성 및 활성화
            Monster_Script _baseMonster = DataBase_Manager.Instance.baseMonster;
            Monster_Script _monster = Instantiate<Monster_Script>(_baseMonster);
            _monster.Activate_Func();

            // 몬스터 리스트에 추가
            this.monsterList.Add(_monster);

            // 스폰 간격 대기
            yield return new WaitForSeconds(DataBase_Manager.Instance.monsterSpawnInterval);
        }
    }

    // 캔에 의해 몬스터를 알림
    public void OnNotifyByCan_Func(Vector2 _canPos)
    {
        bool _isNotify = false; // 알림 여부

        // 모든 몬스터에 대해 반복
        foreach (Monster_Script _monster in this.monsterList)
        {
            // 몬스터가 캔 위치와 일정 거리 이내에 있는지 확인
            if (Vector2.Distance(_monster.GetPos, _canPos) < DataBase_Manager.Instance.notifyDistance)
            {
                _isNotify = true;

                // 몬스터의 도착 위치 설정
                _monster.SetArrivePos_Func(_canPos);
            }
        }

        // 알림이 발생한 경우 효과음 재생
        if (_isNotify)
            SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.CanNotify);
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 모든 몬스터 비활성화
            foreach (Monster_Script _monster in monsterList)
                _monster.Deactivate_Func();

            // 몬스터 스폰 코루틴 중지
            StopCoroutine(this.spawnCor);
        }

        // 코루틴 변수 초기화
        this.spawnCor = null;

        // 몬스터 리스트 초기화
        this.monsterList.Clear();
    }
}