using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberAudio : MonoBehaviour
{
    public List<AudioSource> audioList;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            audioList.Add(child.gameObject.GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
