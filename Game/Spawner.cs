using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{
    #region private
    public GameObject[] cubes;
    [SerializeField] Material materialGauche;
    [SerializeField] Material materialDroite;
    [SerializeField] private List<Transform> spawnPosTransform;
    [SerializeField] private AlternativeSpawner secondSpawner;
    #endregion

    //-------------------------------PARAMETRES A MODIFIER POUR LA DIFFICULTE------------------------------------------------------
    public float distanceOffset = .3f; //L'écartement au centre des cubes metres
    public bool bicolor = true; //Pour avoir les 2 couleurs
    public bool musicOn = true;  //Pour jouer avec ou sans la musique
    public Vector2 biais; //Pour avoir le biais par rapport à 2 directions -> -x = vers la gauche et -y = vers le bas
    public Vector2 secondBiais;
    public float WorkingArea = -12.69f; //Distance de base : 12.69f;
    public bool twoSpawners = true;
    
    //----------------------------------------------------------------------------------------------------------------------------



    //------------------------------PARAMETRES A MODIFIER POUR QUE CA SOIT AGREABLE-----------------------------------------------
    private float decalageTemporel = 5f; //Correspond à l'offset temporel des cubes du à la distance entre instantiation et joueur
    private float facteurCorrection = -.1f; //Correspond à des décalages désagréables possiblement du à une position du joueur ou autre
    //----------------------------------------------------------------------------------------------------------------------------

    #region private
    [SerializeField] private Movement cubePrefab;
    private List<Vector3> spawnPos = new List<Vector3>();
    [SerializeField] private Saber mainDroite;
    [SerializeField] private Saber mainGauche;
    [SerializeField] private Transform center;
    private MusicData musicData;
    private GameManager gameManager;
    private float musicTimer = 0f;
    public int rate = 0;
    public int nombreNotesTotal;
    public List<float> timerTook;
    private bool takeDistance = true;
    #endregion
  

    void Awake() {

        //-----------------------------------------METTRE LE BON PATH POUR LE JSON DE LA MUSIQUE----------------------------------------
        string json = File.ReadAllText(Application.dataPath + "/JSON/musicData.json");
        musicData = JsonUtility.FromJson<MusicData>(json); //Ne pas toucher
        //------------------------------------------------------------------------------------------------------------------------------

        nombreNotesTotal = musicData.timeStamps.Count;

        //Mettre le bon gameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {

        //si 2 spawners alors on instantie un second spawner
        if (twoSpawners)
        {
            //La c'est pour determiner la distance à laquelle doit être le spawner en fonction de la vitesse des cubes
            transform.position = new Vector3(transform.position.x + biais.x, transform.position.y + biais.y, WorkingArea + decalageTemporel*cubePrefab.cubeVelocity);
            SetSpawnPos();        
            transform.LookAt(new Vector3(0, 1.55f, -14f), Vector3.forward);

            //Partie pour le second spawner
            print(secondBiais);
            secondSpawner.transform.position = new Vector3(secondBiais.x, 3f + secondBiais.y, WorkingArea + decalageTemporel*cubePrefab.cubeVelocity);
            secondSpawner.SetSpawnPos();
            secondSpawner.transform.LookAt(new Vector3(.4f, 1.55f, -14f), Vector3.forward);
        } else
        {
            //La c'est pour determiner la distance à laquelle doit être le spawner en fonction de la vitesse des cubes
            transform.position = new Vector3(transform.position.x + biais.x, transform.position.y + biais.y, WorkingArea + decalageTemporel*cubePrefab.cubeVelocity);
            SetSpawnPos();        
            transform.LookAt(new Vector3(0, 1.55f, -14f), Vector3.forward);
        }
    }

    private void SetSpawnPos() {
        //On génère les premiers spawns de manière normale
        spawnPosTransform[0].localPosition = new Vector3(-distanceOffset, distanceOffset, 0);
        spawnPosTransform[1].localPosition = new Vector3(-distanceOffset, 0, 0);
        spawnPosTransform[2].localPosition = new Vector3(-distanceOffset, -distanceOffset, 0);
        spawnPosTransform[3].localPosition = new Vector3(0, distanceOffset, 0);
        spawnPosTransform[4].localPosition = new Vector3(0, 0, 0);
        spawnPosTransform[5].localPosition = new Vector3(0, -distanceOffset, 0);
        spawnPosTransform[6].localPosition = new Vector3(distanceOffset, distanceOffset, 0);
        spawnPosTransform[7].localPosition = new Vector3(distanceOffset, 0, 0);
        spawnPosTransform[8].localPosition = new Vector3(distanceOffset, -distanceOffset, 0); 
    }

    void Update() {
        if(gameManager.isRunning) {
            musicTimer += Time.deltaTime;
            //if (Mathf.Abs(musicTimer % 2) < 0.1f && takeDistance) //Permet d'ajouter la distance au centre = amplitude si temps modulo 2s
            //{
            if(takeDistance)
            {
                takeDistance = false;
                mainGauche.distanceHandCenter.Add(Vector3.Distance(mainGauche.transform.position, center.position) -0.1f);
                mainDroite.distanceHandCenter.Add(Vector3.Distance(mainDroite.transform.position, center.position) -0.1f);
                timerTook.Add(musicTimer);
                StartCoroutine(WaitS(.2f));
            }
            //}
        }

        if (bicolor) //Spawn differentiel avec bicolor ou pas
        {
            if (twoSpawners)
            {
                if(Mathf.Abs(musicData.timeStamps[0] - musicTimer - decalageTemporel - facteurCorrection) < .05f)
                {
                    if (musicData.leftRight[0] == 0)
                    {
                        Instantiate(cubes[musicData.leftRight[0]], spawnPosTransform[musicData.spawnPosNumb[0]-1].position, spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);    

                        musicData.leftRight.Remove(musicData.leftRight[0]);
                        musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                        musicData.timeStamps.Remove(musicData.timeStamps[0]);                        
                    } else
                    {
                        Instantiate(cubes[musicData.leftRight[0]], secondSpawner.spawnPosTransform[musicData.spawnPosNumb[0]-1].position, secondSpawner.spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);    

                        musicData.leftRight.Remove(musicData.leftRight[0]);
                        musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                        musicData.timeStamps.Remove(musicData.timeStamps[0]);                          
                    }
                } 
            } else
            {
                if(Mathf.Abs(musicData.timeStamps[0] - musicTimer - decalageTemporel - facteurCorrection) < .05f)
                {
                    Instantiate(cubes[musicData.leftRight[0]], spawnPosTransform[musicData.spawnPosNumb[0]-1].position, spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);    

                    musicData.leftRight.Remove(musicData.leftRight[0]);
                    musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                    musicData.timeStamps.Remove(musicData.timeStamps[0]);  
                }
            }
        } else if(!bicolor) 
        {
            mainDroite.isGauche = true; //NOTE A MOI MEME -> mettre l'inverse si ça marche pas
            if(twoSpawners)
            {
                if(Mathf.Abs(musicData.timeStamps[0] - musicTimer - decalageTemporel - facteurCorrection) < .05f) 
                {
                    if(musicData.leftRight[0] == 0)
                    {
                        Instantiate(cubes[0], spawnPosTransform[musicData.spawnPosNumb[0]-1].position, spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);

                        musicData.leftRight.Remove(musicData.leftRight[0]);
                        musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                        musicData.timeStamps.Remove(musicData.timeStamps[0]);
                    } else
                    {
                        Instantiate(cubes[0], secondSpawner.spawnPosTransform[musicData.spawnPosNumb[0]-1].position, secondSpawner.spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);

                        musicData.leftRight.Remove(musicData.leftRight[0]);
                        musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                        musicData.timeStamps.Remove(musicData.timeStamps[0]);                        
                    }
                }
            } else
            {
                if(Mathf.Abs(musicData.timeStamps[0] - musicTimer - decalageTemporel - facteurCorrection) < .05f) 
                {
                    Instantiate(cubes[0], spawnPosTransform[musicData.spawnPosNumb[0]-1].position, spawnPosTransform[musicData.spawnPosNumb[0]-1].rotation);

                    musicData.leftRight.Remove(musicData.leftRight[0]);
                    musicData.spawnPosNumb.Remove(musicData.spawnPosNumb[0]);
                    musicData.timeStamps.Remove(musicData.timeStamps[0]);
                }
            }            
        }

        //Bug ici je sais pas pourquoi il me fait un count restant de 37 alors que je voudrais 0
        //-----------------DORONE : A CHANGER SI TU VEUX FAIRE LE TUTO ----------------------------------------
        if(musicData.timeStamps.Count == 37 && gameManager.isRunning) {
            StartCoroutine(DecalageFin());
        }

    }

    private IEnumerator WaitS(float number) {
        yield return new WaitForSeconds(number);
        takeDistance = true;
    }

    private IEnumerator DecalageFin() {
        yield return new WaitForSeconds(6);
        gameManager.isRunning = false;
        gameManager.accuracy = 1 - ((float)rate / (float)(nombreNotesTotal-37));
        gameManager.OnEnd();
    }

    public class MusicData //A modifier selon ce qu'on veut envoyer à la fin de la musique
    {
        public List<float> timeStamps;
        public List<int> leftRight;
        public List<int> spawnPosNumb;
    }   
}