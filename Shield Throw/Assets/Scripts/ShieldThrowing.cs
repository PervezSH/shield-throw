using UnityEngine;

public class ShieldThrowing : MonoBehaviour
{
    [SerializeField] Animator anim;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Throw", true);
        }
        else
        {
            anim.SetBool("Throw", false);
        }
    }
}
