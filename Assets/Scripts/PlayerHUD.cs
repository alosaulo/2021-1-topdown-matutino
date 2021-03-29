﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject prefabCoracao;
    public GameObject barraVida;
    public List<CoracaoController> coracoes;

    PlayerController player;

    float maxHealth;
    float currentHealth;
    float lastHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = gameManager.player;
        maxHealth = player.character.maxHealth;
        lastHealth = player.character.currentHealth;
        GenerateHearths();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.character.currentHealth;
        UpdateHearths();
    }

    void GenerateHearths() {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject hearth = Instantiate(prefabCoracao, barraVida.transform);
            CoracaoController controller = hearth.GetComponent<CoracaoController>();
            coracoes.Add(controller);
        }
    }

    void UpdateHearths() {
        if (lastHealth < currentHealth) {
            GainHealth();
            lastHealth = currentHealth;
        }
        if (lastHealth > currentHealth) {
            LoseHealth();
            lastHealth = currentHealth;
        }
    }

    void GainHealth() {
        for (int i = Mathf.RoundToInt(lastHealth); i < currentHealth; i++)
        {
            coracoes[i].coracaoFill.gameObject.SetActive(true);
        }
    }

    void LoseHealth() {
        for (int i = Mathf.RoundToInt(currentHealth); i < lastHealth; i++)
        {
            coracoes[i].coracaoFill.gameObject.SetActive(false);
        }
    }

}