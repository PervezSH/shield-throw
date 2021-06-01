using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [Header("For tilting shield angle")]
    [SerializeField] Transform targetAngle;
    [SerializeField] float tiltingSpeed = 5f;

    [Header("For Catching Shield")]
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody shieldRb;
    [SerializeField] Transform curvePoint;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform parent;
    [SerializeField] float returnSpeed = 1f;
    Vector3 currentPos;
    bool isReturning = false;

    [SerializeField] float time = 0.0f;

    void Update()
    {
        if (this.gameObject.transform.parent == null && !isReturning)
            tiltShieldAngle();

        if (isReturning)
            returningThroughBezierCurve();
    }

    void tiltShieldAngle()
    {
        if (time < 1.0f)
        {
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetAngle.rotation, tiltingSpeed * Time.deltaTime);
            time += Time.deltaTime;
        }
    }

    void shieldReturn()
    {
        time = 0.0f;
        isReturning = true;
        currentPos = shieldRb.position;
        shieldRb.velocity = Vector3.zero;
        shieldRb.isKinematic = true;
    }

    void returningThroughBezierCurve()
    {
        if (time < 1.0f)
        {
            shieldRb.position = getBQCPoint(time, currentPos, curvePoint.position, targetPoint.position);
            shieldRb.rotation = Quaternion.Slerp(shieldRb.transform.rotation, targetPoint.rotation, 50 * Time.deltaTime);
            time += Time.deltaTime * returnSpeed;
        }
        else
            resetShield();
    }

    void resetShield()
    {
        isReturning = false;
        anim.SetBool("Catch_Ready", false);
        anim.SetBool("Catching", true);

        shieldRb.transform.parent = parent;
        shieldRb.transform.localPosition = Vector3.zero;
        shieldRb.rotation = targetPoint.rotation;

        Invoke("resetAnimParameters", 0.1f);
    }

    void resetAnimParameters()
    {
        anim.SetBool("Catching", false);
        time = 0.0f;
    }

    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Character" && collision.collider)
        {
            anim.SetBool("Catch_Ready", true);
            shieldReturn();
        }
    }
}
