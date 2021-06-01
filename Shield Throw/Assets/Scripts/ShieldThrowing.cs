using UnityEngine;

public class ShieldThrowing : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] Animator anim;
    [SerializeField] Transform throwSource;
    public Rigidbody shieldRb;

    [SerializeField] float throwPower;
    Vector3 hit_point;
    Vector3 throwDir;

    void Update()
    {
        //Aiming
        if (Input.GetMouseButtonDown(1))
            anim.SetBool("Aiming", true);
        else if (Input.GetMouseButtonUp(1))
            anim.SetBool("Aiming", false);

        //Shield Throwing
        if (Input.GetMouseButton(0))
            anim.SetBool("Throw", true);
        else
            anim.SetBool("Throw", false);

        //Get Direction
        if (anim.GetBool("Aiming"))
            returningThrowDir();

    }
    public void shieldThrow()
    {
        shieldRb.isKinematic = false;
        shieldRb.transform.parent = null;
        if (!anim.GetBool("Aiming"))
            shieldRb.AddForce(transform.forward * throwPower * Time.deltaTime, ForceMode.Impulse);
        else
            shieldRb.AddForce(throwDir.normalized * throwPower * Time.deltaTime, ForceMode.Impulse);
    }

    void returningThrowDir()
    {
        RaycastHit hit;
        Ray ray = new Ray(camTransform.position, camTransform.forward);
        if (Physics.Raycast(ray, out hit, 100f))
            hit_point = hit.point;
        else
            hit_point = ray.GetPoint(100f);

        throwDir = hit_point - throwSource.position;
    }
}
