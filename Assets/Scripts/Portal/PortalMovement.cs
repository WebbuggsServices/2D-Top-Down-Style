using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    public Transform destination;
    Transform player;
    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
              
            }
        }
    }

    IEnumerator PortalIn()
    {
        rb.simulated = false;
        anim.Play("PortalIn");
        StartCoroutine(MoveInPorta());
        yield return new WaitForSeconds(0.5f);
        if (destination != null)
        {
            player.transform.position = destination.position;
        }
        anim.Play("PortalOut");
        yield return new WaitForSeconds(0.4f);
        rb.simulated = true;
    }

    IEnumerator MoveInPorta()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
