using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class HistoricWriting : MonoBehaviour
{
    #region referencesPourCalcul
    [SerializeField] private Spawner spawner;
    [SerializeField] private Saber mainGauche;
    [SerializeField] private Saber mainDroite;
    [SerializeField] private GameManager gameManager;

    #endregion

    private string json;
    private string jsonScore;


    public class PartyHistoric {
        public string name = null;
        public float accuracy = 0;
        public int score = 0;
        public int scoreGauche = 0;
        public int scoreDroite = 0;
        public List<float> distanceRightHand = null;
        public List<float> distanceLeftHand = null;
        public List<float> musicTimer = null;

        public PartyHistoric(string name = null, float accuracy = 0, int score = 0, List<float> distanceRightHand = null, List<float> distanceLeftHand = null, List<float> musicTimer = null) {
            name = this.name;
            accuracy = this.accuracy;
            score = this.score;
            distanceRightHand = this.distanceRightHand;
            distanceLeftHand = this.distanceLeftHand;
            musicTimer = this.musicTimer;
        }
    }

    public class ScoreHistoric 
    {
        public int scoreGauche;
        public int scoreDroite;
        public float accuracy;
        public float combo;
    }

    ScoreHistoric scoreHistoric = new ScoreHistoric();
    PartyHistoric historic = new PartyHistoric();

    //-----------------------------------------ECRIRE UN JSON----------------------------------------
    public void OutputJson() {
        historic.accuracy = gameManager.accuracy;
        historic.name = "music1";
        historic.distanceLeftHand = mainGauche.distanceHandCenter;
        historic.distanceRightHand = mainDroite.distanceHandCenter;
        historic.score = gameManager.score;
        historic.musicTimer = spawner.timerTook;
        historic.scoreGauche = mainGauche.individualScore;
        historic.scoreDroite = mainDroite.individualScore;

        //PartyHistoric test = new PartyHistoric("music1", gameManager.accuracy, gameManager.score, mainDroite.distanceHandCenter, mainGauche.distanceHandCenter, spawner.timerTook);
        json = JsonUtility.ToJson(historic);

        File.WriteAllText(Application.dataPath + "/historic3.json", json);

        //Pour le score individuel + accuracy + combo
        scoreHistoric.scoreGauche = mainGauche.individualScore;
        scoreHistoric.scoreDroite = mainDroite.individualScore;
        scoreHistoric.accuracy = gameManager.accuracy;
        scoreHistoric.combo = gameManager.maxCombo;

        jsonScore = JsonUtility.ToJson(scoreHistoric);

        File.WriteAllText(Application.dataPath + "/scoreHistoric.json", jsonScore);

    }
}
