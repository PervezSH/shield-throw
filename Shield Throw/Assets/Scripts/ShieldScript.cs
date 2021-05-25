using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] Transform shieldTarget;

    private void Update()
    {
        transform.localPosition = new Vector3(0.15f, 0f, 0f) + shieldTarget.position;
        transform.localRotation = Quaternion.Euler(45f, 180f, 0f)*shieldTarget.rotation;
    }
}
