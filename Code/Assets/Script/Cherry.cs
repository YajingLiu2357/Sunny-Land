using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerControl>().CherryCount();
        Destroy(gameObject);
    }
}
