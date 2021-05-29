using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    //For tilting shield angle
    [SerializeField] Transform targetAngle;
    [SerializeField] float tiltingSpeed = 5f;

    float time = 0.0f;

    void Update()
    {
        if (this.gameObject.transform.parent == null)
            tiltShieldAngle();
    }

    void tiltShieldAngle()
    {
        if (time < 1.0f)
        {
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetAngle.rotation, tiltingSpeed * Time.deltaTime);
            time += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
    }
}
