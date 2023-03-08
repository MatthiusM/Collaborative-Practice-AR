using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    [Header("Sound File")]
    [SerializeField]
    private AudioClip m_Clip;

    private Vector3 rotationSpeed;


    private void Start()
    {
        rotationSpeed = new Vector3(RandomInt(0, 90), RandomInt(0, 90), RandomInt(0, 90));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    int RandomInt(int min, int max)
    {
        return Random.Range(min, max);  
    }

    public void PlayAudio()
    {
        this.GetComponent<AudioSource>().PlayOneShot(m_Clip);
    }
}
