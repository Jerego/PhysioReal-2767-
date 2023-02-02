using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadCurrentSettings : MonoBehaviour
{
    private Settings settings;
    #region references
    [SerializeField] private Slider sliderVitesse;
    [SerializeField] private Slider sliderNombre;
    [SerializeField] private Slider sliderOffset;
    [SerializeField] private FlexibleColorPicker fcpGauche;
    [SerializeField] private FlexibleColorPicker fcpDroite;
    [SerializeField] private CheckButton bilateralButton;
    [SerializeField] private CheckButton musicButton;
    [SerializeField] private CheckButton latButton;
    [SerializeField] private Transform cursorTransform;
    [SerializeField] private Transform secondCursorTransform;
    [SerializeField] private CheckButton pointilleButton;
    [SerializeField] private Slider pointilleSlider;
    #endregion
    
    private void Awake() {
        string json = File.ReadAllText("C:/Users/vince/OneDrive/Documents/ECE/ING5/PFE/certificationUnity/codeProto1/1.3_games/Assets/JSON/Settings.json");
        settings = JsonUtility.FromJson<Settings>(json);

        sliderVitesse.value = settings.vitesseDesNotes;
        sliderNombre.value = settings.nombreDeNotes;
        bilateralButton.check = settings.bilateral;
        musicButton.check = settings.musique;
        cursorTransform.localPosition = new Vector3(settings.biaisCenter.x, settings.biaisCenter.y, cursorTransform.localPosition.z);
        secondCursorTransform.localPosition = new Vector3(settings.secondBiaisCenter.x, settings.secondBiaisCenter.y, secondCursorTransform.localPosition.z);
        fcpGauche.color = settings.colorGauche;
        fcpDroite.color = settings.colorDroite;
        sliderOffset.value = settings.distanceOffset;
        pointilleButton.check = settings.pointilleOn;
        pointilleSlider.value = settings.pointilleValue;
        latButton.check = settings.latButton;
    }
}
