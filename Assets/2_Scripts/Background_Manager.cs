using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Background_Manager : MonoBehaviour
{
    public static Background_Manager Instance;

    [SerializeField] private Tree_Script[] treeArr = null; // 고정된 나무 배열
    [SerializeField] private Transform[] mapRangeTrfArr = null; // 맵 범위 트랜스폼 배열
    [SerializeField] private Animation[] grassAnimArr = null; // 풀 애니메이션 배열
    [SerializeField] private Transform randomTreeGroup = null; // 랜덤 나무 그룹 트랜스폼
    private List<Tree_Script> randomTreeArr = null; // 랜덤 나무 리스트

    // 초기화 함수
    public void Init_Func()
    {
        // 싱글톤 인스턴스 설정
        Instance = this;

        // 랜덤 나무 리스트 초기화
        randomTreeArr = new List<Tree_Script>();

        // 설정된 개수만큼 랜덤 나무 생성
        for (int i = 0; i < DataBase_Manager.Instance.spawnTreeCount; i++)
        {
            Tree_Script _baseTree = DataBase_Manager.Instance.baseTree;
            Tree_Script _tree = Instantiate(_baseTree);
            _tree.transform.SetParent(randomTreeGroup);
            randomTreeArr.Add(_tree);
        }

        // 고정된 나무 초기화
        foreach (Tree_Script _tree in this.treeArr)
            _tree.Init_Func();

        // 랜덤 나무 초기화
        foreach (Tree_Script _tree in this.randomTreeArr)
            _tree.Init_Func();

        // 풀 애니메이션 속도 랜덤 설정
        foreach (Animation _grassAnim in this.grassAnimArr)
        {
            AnimationState _state = _grassAnim[_grassAnim.clip.name];
            _state.speed = Random.Range(0.8f, 1.2f);
        }

        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public void Activate_Func()
    {
        // 고정된 나무 활성화
        foreach (Tree_Script _tree in this.treeArr)
            _tree.Activate_Func(false);

        // 랜덤 나무 활성화
        foreach (Tree_Script _tree in this.randomTreeArr)
            _tree.Activate_Func(true);
    }

    // 맵의 최소 위치 반환 함수
    public Vector2 GetMapMinPos_Func()
    {
        return this.mapRangeTrfArr[0].position;
    }

    // 맵의 최대 위치 반환 함수
    public Vector2 GetMapMaxPos_Func()
    {
        return this.mapRangeTrfArr[1].position;
    }

    // 랜덤 위치 반환 함수
    public Vector2 GetRandomPos_Func()
    {
        Vector2 _minPos = this.GetMapMinPos_Func();
        Vector2 _maxPos = this.GetMapMaxPos_Func();
        float _xPos = Random.Range(_minPos.x, _maxPos.x);
        float _yPos = Random.Range(_minPos.y, _maxPos.y);
        return new Vector2(_xPos, _yPos);
    }

    // 정렬 순서 반환 함수
    public int GetOrderLayer_Func(Transform _trf)
    {
        return -(int)(_trf.position.y * 10f);
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 고정된 나무 비활성화
            foreach (Tree_Script _tree in this.treeArr)
                _tree.Deactivate_Func();

            // 랜덤 나무 비활성화
            foreach (Tree_Script _tree in this.randomTreeArr)
                _tree.Deactivate_Func();
        }
    }
}