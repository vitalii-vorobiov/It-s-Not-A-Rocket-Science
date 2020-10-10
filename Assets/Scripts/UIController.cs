using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController> {
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject setupPanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private Text mainGameText;
    

    private void Awake() {
        gamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Start() {
        SelectBuilding();
    }

    public void OpenSettings() {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings() {
        settingsPanel.SetActive(false);
    }

    public void FinishSceneSetup() {
        ARController.Instance.DisableSceneSetup();
        ARController.Instance.StartGame();
        setupPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gamePanel.SetActive(true);
        
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BindingCore"), Vector3.zero, Quaternion.identity);
        }
    }

    public void StartSceneSetup() {
        ARController.Instance.FinishGame();
        ARController.Instance.EnableSceneSetup();
        settingsPanel.SetActive(false);
        setupPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void SelectWelding() {
        mainGameText.text = "Welding mode selected";
    }

    public void SelectBuilding() {
        mainGameText.text = "Building mode selected";
    }

    public void SelectDrilling() {
        mainGameText.text = "Drilling mode selected";
    }
}
