using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;
  // Start is called before the first frame update
  private void Awake()
  {
    foreach (Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;

      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
    }
  }

  void Start()
  {
    if (SceneManager.GetActiveScene().buildIndex == 0)
      Play("BackgroundMusic");
  }

  // Update is called once per frame
  public void Play(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    // if ( s.name == "BackgroundMusic")
    // {
    //     s.source.loop = true;
    // }
    // else
    // {
    //     s.source.loop = false;
    // }
    // Debug.Log(s.name);
    // s.source.Play();
    if (s.audioType == 0)
    {
      s.source.loop = true;
    }
    else
    {
      s.source.loop = false;
    }
    s.source.Play();
  }

  public void PlayOneShot(string name)
  {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s.source.isPlaying == false)
      s.source.PlayOneShot(s.clip);
  }

}
