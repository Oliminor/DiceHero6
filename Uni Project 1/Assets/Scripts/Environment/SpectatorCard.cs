/*
 Spectator cards changeing animation after random amount of time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCard : MonoBehaviour
{
    private Animator anim;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 12);
        GetComponent<MeshRenderer>().materials[1].SetTexture("_BaseMap", FindObjectOfType<GameManager>().cardTexture[random]);
        GetComponent<Animator>().Play(0, -1, Random.value);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            ChangeAnimation();
            time = Random.Range(10, 20);
        }
    }

    void ChangeAnimation()
    {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                anim.SetTrigger("cheer01");
                break;
            case 1:
                anim.SetTrigger("cheer02");
                break;
        }
    }
}
