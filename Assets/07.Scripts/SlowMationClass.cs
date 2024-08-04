using UnityEngine;
using System.Collections; // 이 부분을 추가

public class SlowMationClass : MonoBehaviour
{
    public float slowFactor = 0.05f;
    public float slowDuration = 2f; // 슬로우 모션 지속 시간

    private float originalTimeScale;
    private float originalFixedDeltaTime;

    void Start()
    {
        // 원래의 TimeScale과 FixedDeltaTime 저장
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = originalFixedDeltaTime * slowFactor;
        StartCoroutine(ResetTimeScaleAfterDuration());
    }

    private IEnumerator ResetTimeScaleAfterDuration()
    {
        yield return new WaitForSecondsRealtime(slowDuration); // 실시간으로 대기
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}
