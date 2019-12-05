using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerDichoticListening : MonoBehaviour
{
    public AudioSource IntroAudio;
    public OVRCameraRig Player;
    public GameObject speaker1;
    public GameObject speaker2;
    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;
    public GameObject lv4;
    public bool gameEnds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        IntroAudio.Play();
        yield return new WaitForSeconds(IntroAudio.clip.length); // wait time
        //supposedly wait for player to set difficulty here
        selectTarget();
        //select difficulty
        //play audio to introduce difficulty selection I guess

        speaker1.SetActive(true);
    }

    private void selectDifficulty() {
    }

    private void selectTarget() {
        System.Random rnd = new System.Random();
        int target = rnd.Next(1, 21);  // creates a number between 1 and 20
        SpeakerTarget script1 = speaker1.GetComponent<SpeakerTarget>();
        script1.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnds) {
            speaker1.SetActive(false);
            //StartCoroutine(Result());
        }
    }
    
    IEnumerator Result()
    {
        //show'em the result!
        yield return new WaitForSeconds(0) ;
    }
}
