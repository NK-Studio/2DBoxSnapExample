using UnityEngine;

public class Box : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;

    /// <summary>
    /// 값이 클 수록 멀리있는 객체에게 스냅이 됩니다. 
    /// </summary>
    [SerializeField] private float snapPower = 1f;

    private void Awake() => _boxCollider2D = GetComponent<BoxCollider2D>();

    private void OnMouseDown() => SnapToBox();

    private void SnapToBox()
    {
        //자기 자신의 위치
        Vector3 origin = transform.position;

        //레이에 닿으면 정보를 받아올 hit 선언
        RaycastHit2D hit;
        
        //자신의 레이어 저장 (Box 레이어 수록)
        int myLayer = gameObject.layer;

        #region 오른쪽 체크

        //레이를 쏘는 x위치를 박스콜라이더 오른쪽 면 위치로 지정
        origin.x = _boxCollider2D.bounds.max.x;
        
        //레이가 자기 자신은 검출되지 않도록 잠시 레이어를 변경함 
        gameObject.layer = LayerMask.GetMask("Ignore Raycast");
        
        //오른쪽으로 레이를 발사
        hit = Physics2D.Raycast(origin, Vector2.right, snapPower, LayerMask.GetMask("Box"));
        
        //닿은 객체가 있을 경우
        if (hit)
        {
            //닿은 표면의 위치를 가져옴
            Vector2 hitPoint = hit.point;
            
            //박스 콜라이더의 반지름을 구함
            float radius = _boxCollider2D.bounds.size.x / 2;
            
            //적용
            hitPoint.x -= radius;

            //이동 적용
            transform.position = hitPoint;
        }

        #endregion

        #region 왼쪽 체크

        //레이를 쏘는 x위치를 박스콜라이더 왼쪽 면 위치로 지정
        origin.x = _boxCollider2D.bounds.min.x;

        //오른쪽으로 레이를 발사
        hit = Physics2D.Raycast(origin, Vector2.left, snapPower, LayerMask.GetMask("Box"));
        
        //닿은 객체가 있을 경우
        if (hit)
        {
            //닿은 표면의 위치를 가져옴
            Vector2 hitPoint = hit.point;
            
            //박스 콜라이더의 반지름을 구함
            float radius = _boxCollider2D.bounds.size.x / 2;
            
            //적용
            hitPoint.x += radius;

            //이동 적용
            transform.position = hitPoint;
        }

        #endregion

        //레이어를 원래대로 돌려 놓음
        gameObject.layer = myLayer;
    }
}