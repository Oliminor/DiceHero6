/*
 This script starts the battle after the player interact with the crystal
 Instantiate the arena, which is contains the enemy characters and every arena component
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLudoArena : MonoBehaviour
{
    [SerializeField] GameObject arena;
    [SerializeField] GameObject interactButton;
    [SerializeField] GameObject crystal;
    [SerializeField] GameObject crystalParticle;
    private Transform player;
    GameObject tempArena;
    bool arenaActive = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerP = new Vector2(player.position.x, player.position.z);
        Vector2 transfgormP = new Vector2(crystal.transform.position.x, crystal.transform.position.z);
        float distance = Vector2.Distance(playerP, transfgormP);

        if (distance < 8 && !arenaActive)
        {
            interactButton.SetActive(true);

            Vector3 crystalPosition = Camera.main.WorldToScreenPoint(crystal.transform.position);
            interactButton.GetComponent<RectTransform>().position = crystalPosition;

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("BattleMusic").GetComponent<AudioSource>().Play();
                GameObject.Find("IdleMusic").GetComponent<AudioSource>().Stop();
                spawnArena();
            }
        }
        else interactButton.SetActive(false);

        if (StaticBoard.LudoIndex >= 16 || PlayerManager.health <= 0)
        {
            GameObject.Find("BattleMusic").GetComponent<AudioSource>().Stop();
            GameObject.Find("IdleMusic").GetComponent<AudioSource>().Play();
            Destroy(tempArena);
            StaticBoard.LudoIndex = 0;
            crystal.gameObject.SetActive(true);
            arenaActive = false;
            StaticBoard.isBattleActive = false;
        }
    }

    private void spawnArena()
    {
        arenaActive = true;
        tempArena = Instantiate(arena, gameObject.transform.position, Quaternion.identity, transform);
        tempArena.transform.localRotation = new Quaternion(0, 0, 0, 0);
        interactButton.SetActive(false);
        crystal.gameObject.SetActive(false);
        GameObject particle = Instantiate(crystalParticle, crystal.transform.position, crystal.transform.rotation, transform.parent);
        Destroy(particle, 2);
        StaticBoard.isBattleActive = true;
    }
}
