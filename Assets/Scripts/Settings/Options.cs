using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Button buttonBack;
    [Header("Music")]
    public Button buttonMusic;
    public AudioSource musicSOurce;
    public Image imageMusicIcon;
    public Sprite musicOff;
    public Sprite musicOn;

    [Header("Vibration")]
    public Button buttonVibration;
    public Image imageVibrationIcon;
    public Sprite vibrationOff;
    public Sprite vibrationOn;

    [Space(10)]
    public Button buttonShareApp;
    public Button buttonRateUs;
    public Button buttonUsagePolicy;

    [SerializeField] string _policyString = "https://www.termsfeed.com/live/397e77d8-4b88-43cf-a43f-64a3784fa74d";
    [SerializeField] string _termsString = "https://www.termsfeed.com/live/277c7336-26a6-435e-850e-feb0ee10f3ef";

    private void Start()
    {
        buttonUsagePolicy.onClick.AddListener(PolicyView);
        buttonRateUs.onClick.AddListener(TermsView);

        buttonMusic.onClick.AddListener(OptionMusic);
        buttonVibration.onClick.AddListener(OptionVibration);
        buttonShareApp.onClick.AddListener(ShareApp);
    }

    void OptionVibration()
    {
        if (imageVibrationIcon.sprite == vibrationOff)
        {
            imageVibrationIcon.sprite = vibrationOn;
            Controller.Instance.isVibration = true;
        }
        else
        {
            imageVibrationIcon.sprite = vibrationOff;
            Controller.Instance.isVibration = false;
        }
    }
    void OptionMusic()
    {
        if (imageMusicIcon.sprite == musicOff)
        {
            imageMusicIcon.sprite = musicOn;
            musicSOurce.volume = 1f;
        }
        else
        {
            imageMusicIcon.sprite = musicOff;
            musicSOurce.volume = 0;
        }
    }

    void ShareApp()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }

    void PolicyView()
    {
        Application.OpenURL(_policyString);
    }
    void TermsView()
    {
        Application.OpenURL(_termsString);
    }
}
