using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocity = 0.0f;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rigidbody.velocity = new Vector2(velocity, 0);
        float parentPosition = this.transform.parent.transform.position.x;

        if (this.transform.position.x - parentPosition  >= 19.5f)
        {
            this.transform.position = new Vector3(-30.0f, this.transform.position.y,this.transform.position.z);
        }
    }
}
