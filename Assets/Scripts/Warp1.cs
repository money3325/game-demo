using UnityEngine;

public class Warp1 : MonoBehaviour
{
    public Transform warpTarget;
    private bool canWarp = false; // 新增标志位，控制是否可以传送

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canWarp)
        {
            Debug.Log("Enter");
            other.gameObject.transform.position = warpTarget.transform.position;
        }
    }

    // 提供一个公共方法用于开启传送功能
    public void EnableWarp()
    {
        canWarp = true;
    }
}
