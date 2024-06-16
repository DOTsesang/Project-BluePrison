using UnityEngine;

public class FootstepParticles : MonoBehaviour
{
    public ParticleSystem footstepParticle; // ��ƼŬ �ý��� ����
    public Transform leftFoot; // ���� �� ��ġ
    public Transform rightFoot; // ������ �� ��ġ

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� �޼���
    public void EmitFootstep(string foot)
    {
        if (foot == "left")
        {
            EmitAtPosition(leftFoot.position);
        }
        else if (foot == "right")
        {
            EmitAtPosition(rightFoot.position);
        }
    }

    private void EmitAtPosition(Vector3 position)
    {
        footstepParticle.transform.position = position;
        footstepParticle.Play();
    }
}
