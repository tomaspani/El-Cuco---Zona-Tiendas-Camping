using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sfx;

    public float mainVolume;
    public float sfxVolume;

    public void Start()
    {
        GetComponent<AudioSource>().volume = mainVolume;
        GetComponent<AudioSource>().Play();
    }


    public void PlaySound(string name)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].name == name)
            {
                if (GameObject.Find(name) == null)
                {
                    GameObject soundGameObject = new GameObject(name);
                    soundGameObject.transform.position = this.transform.position;
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.volume = sfxVolume;
                    audioSource.PlayOneShot(sfx[i]);


                }
                else
                {
                    if (!GameObject.Find(name).GetComponent<AudioSource>().isPlaying)
                    {
                        GameObject.Find(name).GetComponent<AudioSource>().PlayOneShot(sfx[i]);
                    }

                }
            }

        }

        

    }


    public void StopSound(string name)
    {
        if(GameObject.Find(name) != null)
        {
            GameObject soundGameObject = GameObject.Find(name);
            if (soundGameObject.GetComponent<AudioSource>().isPlaying)
            {
                soundGameObject.GetComponent<AudioSource>().Stop();

            }
        }
            
    }
}
