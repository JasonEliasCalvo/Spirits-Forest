using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] Pointsmovement;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float smoothTime;

    private int NextPoint = 1;
    private bool orderPlatform = true;
    private Vector3 lastPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (orderPlatform && NextPoint + 1 >= Pointsmovement.Length)
            orderPlatform = false;
        if (!orderPlatform && NextPoint <= 0)
            orderPlatform = true;

        transform.position = Vector3.Lerp(transform.position, Pointsmovement[NextPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Pointsmovement[NextPoint].position) < smoothTime)
        {
            if (orderPlatform)
                NextPoint += 1;
            else
                NextPoint -= 1;
        }
    }

    public Vector3 GetVelocity()
    {
        Vector3 currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        return currentVelocity;
    }
}
