using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _timerText;

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        float timeInSeconds = Time.time;

        
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

        
        _timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}
