using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform warpTarget;
    
    void OnTriggerEnter2D(Collider2D other )
    {
        Debug.Log("Enter");
        other.gameObject.transform.position = warpTarget.transform.position;

    }
}