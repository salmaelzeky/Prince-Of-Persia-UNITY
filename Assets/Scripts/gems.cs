using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gems : MonoBehaviour
{
    public GameObject scoreText;
    public AudioSource collectSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Character")
        {
            print("Entered");
            collectSound.Play();
            other.GetComponent<playerController>().score = 10 + other.GetComponent<playerController>().score;
            scoreText.GetComponent<Text>().text = "Score: " + other.GetComponent<playerController>().score;
            Destroy(gameObject); //This destroys things.
        }
    }
}
