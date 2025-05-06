using UnityEngine;

public class RandomStart : MonoBehaviour
{
    public Animator animator; 

    void Start()
    {
        animator.speed = Random.Range(0.8f, 1.2f);
    }
}