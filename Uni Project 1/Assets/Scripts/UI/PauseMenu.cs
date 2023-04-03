/*
 Pause menu UI elements manager
 Such as Exit, Options, Resume and Weapon Inventory
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject weaponUI;

    [SerializeField] GameObject gameOverUI;

    [SerializeField] GameObject VolumeEffect;

    private bool pauseBool = false;
    private bool gameOverBool = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.health <= 0 && !gameOverBool)
        {
            gameOverBool = true;
            gameOverUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K)) WeaponInventory();
        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenuTrigger();
        if (Input.GetKeyDown(KeyCode.Space) && PlayerManager.health <= 0) GameOverUITrigger();
    }

    public void ExitGame()
    {
        Application.Quit();
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }

    public void Options()
    {
        OptionsUI.SetActive(true);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }

    public void OptionsBack()
    {
        OptionsUI.SetActive(false);
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }

    private void WeaponInventory()
    {
        if (weaponUI.activeSelf == false && !pauseBool && !StaticBoard.isBattleActive)
        {
            weaponUI.SetActive(true);
            StaticBoard.isCharacterActive = false;
            Time.timeScale = 0;
        }
        else
        {
            weaponUI.SetActive(false);
            StaticBoard.isCharacterActive = true;
            Time.timeScale = 1;
        }
    }

    private void PauseMenuTrigger()
    {
        if (!pauseBool)
        {
            GameObject.Find("BattleMusic").GetComponent<AudioSource>().volume = 0.1f;
            GameObject.Find("IdleMusic").GetComponent<AudioSource>().volume = 0.03f;
            GameObject.Find("CrowdSound").GetComponent<AudioSource>().volume = 0.0f;
            weaponUI.SetActive(false);
            gameUI.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            VolumeEffect.SetActive(false);
            VolumeEffect.SetActive(true);
            pauseBool = true;
            StaticBoard.isCharacterActive = false;
        }
        else
        {
            GameObject.Find("BattleMusic").GetComponent<AudioSource>().volume = 0.3f;
            GameObject.Find("IdleMusic").GetComponent<AudioSource>().volume = 0.1f;
            GameObject.Find("CrowdSound").GetComponent<AudioSource>().volume = 0.2f;
            gameUI.SetActive(true);
            pauseMenu.SetActive(false);
            OptionsUI.SetActive(false);
            Time.timeScale = 1;
            pauseBool = false;
            StaticBoard.isCharacterActive = true;
            if (!Input.GetKeyDown(KeyCode.Escape)) GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        }
    }

    void GameOverUITrigger()
    {
        VolumeEffect.SetActive(false);
        VolumeEffect.SetActive(true);
        gameOverBool = false;
        PlayerManager.health = 6;
        gameOverUI.SetActive(false);
        StaticBoard.isCharacterActive = true;
        GameObject.FindWithTag("Player").transform.position = FindObjectOfType<GameManager>().respawnPoint.position;
    }
}
