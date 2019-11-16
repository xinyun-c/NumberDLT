using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerTarget : MonoBehaviour
{
    public int target;
    public int last_num;

    public GameObject numberAudio;
    private List<AudioSource> audioList;

    public int iter;
    public GameObject player;
    public GameObject manager;
    public OVRInput.Controller controller;

    private bool responded;
    public int correctNumber;
    public int incorrectNumber;

    public AudioSource TargetAnnouncement;
    public AudioSource ReadyStart;
    public AudioSource Result_1;
    public AudioSource Result_2;
    public AudioSource Result_3;
    public AudioSource Lose;
    public AudioSource Win;

    public GameObject speaker2;
    private bool gameStart;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        audioList = numberAudio.GetComponent<NumberAudio>().audioList;
        StartCoroutine(audioPlay(iter));
    }

    IEnumerator audioPlay(int num) {
        TargetAnnouncement.Play();
        yield return new WaitForSeconds(TargetAnnouncement.clip.length);
        audioList[target].Play();
        yield return new WaitForSeconds(audioList[target].clip.length + 1);
        ReadyStart.Play();
        yield return new WaitForSeconds(ReadyStart.clip.length);

        gameStart = true;

        speaker2.SetActive(true);
        speaker2.GetComponent<AudioSource>().Play();
        for (int i = 1; i <= num; i++)
        {
            responded = false;
            SelectRandomNumber();
            audioList[last_num].Play();
            yield return new WaitForSeconds(audioList[last_num].clip.length + 1); // wait audioclip length time plus one second
            if (responded == false) {
                if (target != last_num)
                {
                    correctNumber = correctNumber + 1;
                }
                else {
                    incorrectNumber = incorrectNumber + 1;
                }
            }
        }
        speaker2.SetActive(false);
        if (correctNumber == 0)
        {
            Lose.Play();
            yield return new WaitForSeconds(Lose.clip.length);
        }
        else if (incorrectNumber == 0)
        {
            Win.Play();
            yield return new WaitForSeconds(Win.clip.length);
        }
        else {
            Result_1.Play();
            yield return new WaitForSeconds(Result_1.clip.length);
            audioList[correctNumber].Play();
            yield return new WaitForSeconds(audioList[correctNumber].clip.length);
            Result_2.Play();
            yield return new WaitForSeconds(Result_2.clip.length);
            audioList[incorrectNumber].Play();
            yield return new WaitForSeconds(audioList[incorrectNumber].clip.length);
            Result_3.Play();
            yield return new WaitForSeconds(Result_3.clip.length);
        }
        
        manager.GetComponent<EventManagerDichoticListening>().gameEnds = true;

    }

    private void SelectRandomNumber() {
        float probTarget = Random.Range(0.0f, 1.0f);
        if (probTarget < 0.3)
        {
            last_num = target;
        }
        else {
            System.Random rnd = new System.Random();
            last_num = rnd.Next(1, 21);  // creates a number between 1 and 20
        }
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (gameStart && !responded) {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) >= 0.9 || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, controller) >= 0.9)
            {
                Debug.Log("A button pressed.");
                if (target == last_num)
                {
                    correctNumber += 1;
                }
                else {
                    incorrectNumber += 1;
                }
                responded = true;
            }
        }
    }
}
