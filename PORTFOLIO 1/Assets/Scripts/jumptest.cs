using UnityEngine;

public class jumptest : MonoBehaviour
{
    Rigidbody rb;
    public float _jumpforce;
    public bool isgrounded;
    public bool jump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetButtonDown("Jump") && isgrounded;
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * _jumpforce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isgrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isgrounded = false;
    }
}
