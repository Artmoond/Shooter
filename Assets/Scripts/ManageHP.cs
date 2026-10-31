using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ManageHP : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        if (_hpSlider == null)
            _hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();

            if (_playerHealth != null)
            {
                
                _playerHealth.OnHealthChanged += UpdateHPBar;
            }
        }
        else
        {
            Debug.LogError("Нет игрока");
        }
    }

    private void OnDestroy()
    {
        
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateHPBar;
        }
    }

    
    private void UpdateHPBar(float currentHealth, float maxHealth)
    {
        if (_hpSlider != null)
        {
            
            _hpSlider.value = currentHealth / maxHealth;
        }
    }
}
