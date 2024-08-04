using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CollisionAnimation : MonoBehaviour
{
    public Animator animator;  // Animator 컴포넌트를 참조합니다.
    public string animationClipName;  // 재생할 애니메이션 클립 이름입니다.
    public PlayableDirector playableDirector;  // PlayableDirector를 참조합니다.
    public TrackAsset trackToActivate;  // 활성화할 트랙을 참조합니다.

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 이 오브젝트에 없습니다!");
        }

        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirector가 설정되지 않았습니다!");
        }

        if (trackToActivate == null)
        {
            Debug.LogError("TrackToActivate가 설정되지 않았습니다!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animator != null)
        {
            animator.Play(animationClipName, -1, 0f);  // 애니메이션 클립을 타임라인 0에서 재생합니다.
        }

        if (playableDirector != null && trackToActivate != null)
        {
            TimelineAsset timelineAsset = (TimelineAsset)playableDirector.playableAsset;
            foreach (var track in timelineAsset.GetOutputTracks())
            {
                if (track == trackToActivate)
                {
                    track.muted = false;  // 트랙을 활성화합니다.
                }
            }

            playableDirector.time = 0;  // 타임라인을 처음으로 되돌립니다.
            playableDirector.Play();  // 타임라인을 재생합니다.
        }
    }
}
