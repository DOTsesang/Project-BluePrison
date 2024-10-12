using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonRawImageOpacityController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RawImage parentRawImage;
    public float transparencyOnHover = 1f;
    public float transparencyNormal = 0.392f; // 100/255 ≈ 0.392

    private void Start()
    {
        // 부모 오브젝트에서 RawImage 컴포넌트를 찾습니다.
        parentRawImage = GetComponentInParent<RawImage>();

        if (parentRawImage == null)
        {
            Debug.LogError("Parent RawImage not found!");
            return;
        }

        // 초기 투명도 설정
        SetImageTransparency(transparencyNormal);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered");
        SetImageTransparency(transparencyOnHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited");
        SetImageTransparency(transparencyNormal);
    }

    private void SetImageTransparency(float alpha)
    {
        if (parentRawImage != null)
        {
            Color color = parentRawImage.color;
            color.a = alpha;
            parentRawImage.color = color;
        }
        else
        {
            Debug.LogError("Parent RawImage is null!");
        }
    }
}