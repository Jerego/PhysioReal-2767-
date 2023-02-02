using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ApplySettings : MonoBehaviour
{
    [SerializeField] private Button button; 
    [SerializeField] private CheckButton checkButtonBilat;
    [SerializeField] private CheckButton checkButtonMusique;
    [SerializeField] private CheckButton latButton;
    [SerializeField] private ChoseColor choseColorGauche;
    [SerializeField] private ChoseColor choseColorDroite;
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private CursorManager secondCursorManager;
    [SerializeField] private Slider sliderVitesse;
    [SerializeField] private Slider sliderNombre;
    [SerializeField] private Slider sliderOffset;
    [SerializeField] private CheckButton pointilleButton;
    [SerializeField] private Slider pointilleSlider;

    
    private void Start() {
        button.onClick.AddListener(Appliquer);
    }

    private void Appliquer() {
        Settings settingsJson = new Settings();
        settingsJson.vitesseDesNotes = sliderVitesse.value;
        settingsJson.nombreDeNotes = sliderNombre.value;
        settingsJson.bilateral = checkButtonBilat.check;
        settingsJson.musique = checkButtonMusique.check;
        settingsJson.colorGauche = choseColorGauche.fcp.color;
        settingsJson.colorDroite = choseColorDroite.fcp.color;
        settingsJson.biaisCenter = new Vector2(cursorManager.transform.localPosition.x, cursorManager.transform.localPosition.y);
        settingsJson.secondBiaisCenter = new Vector2(secondCursorManager.transform.localPosition.x, secondCursorManager.transform.localPosition.y);
        settingsJson.distanceOffset = sliderOffset.value;
        settingsJson.pointilleOn = pointilleButton.check;
        settingsJson.pointilleValue = pointilleSlider.value;
        settingsJson.latButton = latButton.check;

        string json = JsonUtility.ToJson(settingsJson);
        File.WriteAllText("C:/Users/vince/OneDrive/Documents/ECE/ING5/PFE/certificationUnity/codeProto1/1.3_games/Assets/JSON/Settings.json", json);

        Debug.Log("Enregistré");

        LevelManager.Instance.LoadScene("GeneralPatientStats");
    }
}
