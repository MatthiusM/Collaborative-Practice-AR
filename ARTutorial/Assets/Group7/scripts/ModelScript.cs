using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    [Header("Sound File")]
    [SerializeField]
    private AudioClip m_Clip;

    private Vector3 rotationSpeed;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    

    private void Start()
    {
        rotationSpeed = new Vector3(RandomInt(0, 90), RandomInt(0, 90), RandomInt(0, 90));
    }

    // Update is called once per frame
    void Update()
    {
        //this code only gets compiled if your in the unity editor
#if UNITY_EDITOR
        /*
        run the code in unity editor
        click one of the prefabs on the left
        now on the right hand side the inspector you should see an unchecked box
        check the box to see the prefab on the screen
        press space to hear the audio from a prefab 
         */
        if (Input.GetKey(KeyCode.Space)) 
        {
            PlayAudio();
        }
#endif
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    int RandomInt(int min, int max)
    {
        return Random.Range(min, max);  
    }

    //my audio method is better (Stonk Emoji)
    public void PlayAudio()
    {
        audioSource.PlayOneShot(m_Clip);
    }

    //public void PlayAudio()
    //{
    //    if (!audioSource.isPlaying)
    //    {
    //        audioSource.Play();
    //    }
    //}

    //public void StopAudio()
    //{
    //    if (audioSource.isPlaying)
    //    {
    //        audioSource.Stop();
    //    }
    //}
}
