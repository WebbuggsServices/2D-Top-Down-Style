using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    public GameObject fragment1;
    public GameObject fragment2;
    public GameObject pos;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            if (fragment1 != null && fragment2 != null)
            {
                Instantiate(fragment1, pos.transform.position, Quaternion.identity);
                Instantiate(fragment2, pos.transform.position, Quaternion.identity);
                Instantiate(fragment2, pos.transform.position, Quaternion.identity);
                Instantiate(fragment1, pos.transform.position, Quaternion.identity);
                Instantiate(fragment2, pos.transform.position, Quaternion.identity);
                Instantiate(fragment1, pos.transform.position, Quaternion.identity);
                Instantiate(fragment1, pos.transform.position, Quaternion.identity);
                Instantiate(fragment2, pos.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

    }
}
