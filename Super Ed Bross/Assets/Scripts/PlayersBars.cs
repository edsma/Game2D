using Assets.Scripts.Constans;
using Assets.Scripts.Enum;
using UnityEngine;
using UnityEngine.UI;
public class PlayersBars : MonoBehaviour
{
    private Slider slider;
    public BarType type;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch (this.type)
        {
            case BarType.health:
                this.slider.maxValue = Constants.CollectableLimits.MaxHealth;
                break;
            case BarType.mana:
                this.slider.maxValue = Constants.CollectableLimits.MaxMana;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BarType.health:
                this.slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.mana:
                this.slider.value = PlayerController.sharedInstance.GetMana();
                break;
            default:
                break;
        }
    }
}
