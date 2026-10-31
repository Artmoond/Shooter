using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText; // Для UI на Canvas (или просто TextMeshPro, если это 3D-текст)
    private int _score = 0;

    private void Awake()
    {
        // Если поле не заполнено в инспекторе, пытаемся найти его автоматически
        if (_scoreText == null)
            _scoreText = GetComponent<TextMeshProUGUI>();

        UpdateScoreText();
    }

    // Метод, который будет увеличивать счет
    public void AddScore(int amount = 1)
    {
        _score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            // Форматируем вывод (например: "Kills: 5")
            _scoreText.text = $"Kills: {_score}";
        }
    }
}