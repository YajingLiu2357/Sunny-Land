using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerControl>().GemCount();
        Destroy(gameObject);
    }
}
