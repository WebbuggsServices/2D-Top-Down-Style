using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Transform past, present, future;
    Transform player;
    Animator anim;
    Rigidbody2D rb;
    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            StartCoroutine(PortalIn(past));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(PortalIn(present));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(PortalIn(future));
        }
    }

    IEnumerator PortalIn(Transform des)
    {
        rb.simulated = false;
        anim.Play("PortalIn");
        StartCoroutine(MoveInPorta());
        loading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (des != null)
        {
            player.transform.position = des.position;
        }
       
        anim.Play("PortalOut");
        yield return new WaitForSeconds(0.4f);
        rb.simulated = true;
        yield return new WaitForSeconds(0.2f);
        loading.SetActive(false);
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
