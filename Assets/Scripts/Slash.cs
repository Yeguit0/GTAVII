using UnityEngine;
using System.Collections;
using System;

public class Slash : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Destruyete());
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = GetComponent<Transform>();
        transform.position = spawn.transform.position;
        transform.localScale = player.transform.localScale;
    }

    IEnumerator Destruyete()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
