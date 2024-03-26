using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    void Start()
    {
        turnSpeed = Random.Range(0.1f,0.3f);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * turnSpeed;
    }
}