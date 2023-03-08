using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField] private TextMeshProUGUI whosTurnText, whoWonText;

    [SerializeField] private GameObject restartButton;


    /// <summary>
    /// This function is bound to the restart game button.
    /// </summary>
    public void OnRestartButtonPressed()
    {
        GameManager.Instance.ResetBoard();
        SetRestartButton(false);
        SetWhoWonText("");
    }

    public void SetRestartButton(bool value)
    {
        restartButton.SetActive(value);
    }

    public void SetWhosTurnText(string text)
    {
        whosTurnText.text = text;
    }

    public void SetWhoWonText(string text)
    {
        whoWonText.text = text;
    }
}
