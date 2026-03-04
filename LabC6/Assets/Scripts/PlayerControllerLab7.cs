using UnityEngine;

public class PlayerControllerLab7 : MonoBehaviour
{
    Animator anim;
    float speed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        float speed = Mathf.Abs(Input.GetAxis("Horizontal"));
        anim.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attack pressed");
            anim.SetTrigger("Attack");
        }
    }
}
