using UnityEngine;

public class Tree_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러

    // 초기화 함수
    public void Init_Func()
    {
        // 비활성화 함수 호출 (초기화 시)
        this.Deactivate_Func(true);
    }

    // 활성화 함수
    public void Activate_Func(bool _isRand)
    {
        // 랜덤 위치 설정이 필요한 경우
        if (_isRand)
            this.transform.position = Background_Manager.Instance.GetRandomPos_Func();

        // 스프라이트의 정렬 순서 설정
        this.srdr.sortingOrder = Background_Manager.Instance.GetOrderLayer_Func(this.transform);
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        // 초기화 시가 아닌 경우에만 실행
        if (!_isInit)
        {
            // 추가적인 비활성화 로직이 여기에 들어갈 수 있습니다.
        }
    }
}