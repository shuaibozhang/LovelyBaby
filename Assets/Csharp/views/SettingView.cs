using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingView : MonoBehaviour {
    public Slider _barSound;
    public Slider _barEffect;
    // Use this for initialization
    void Start () {
        _barSound.value = UserData.getInstance().getSoundValue();
        _barEffect.value = UserData.getInstance().getEffectValue();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnOnClose()
    {
        var ani = gameObject.GetComponent<Animator>();
        ani.Play("settingout");
    }

    public void onValueChangeSound()
    {
        UserData.getInstance().setSoundValue(_barSound.value);
    }

    public void onValueChangeEffect()
    {
        UserData.getInstance().setEffectValue(_barEffect.value);
    }

    public void removeSelf()
    {
        gameObject.SetActive(false);
    }
}
