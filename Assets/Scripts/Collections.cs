using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collections : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
