using UnityEngine;

public class SporeContainer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.3f);
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}