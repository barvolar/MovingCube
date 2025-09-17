using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _scoreText;

    public static ScoreManager Instance;
    private int _score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        _score = _score + value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_scoreText != null)
            _scoreText.text = "Score: " + _score.ToString();
    }

}
