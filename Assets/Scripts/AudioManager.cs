using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundEffects;
    public Sound[] dialog;

    public AudioMixer mixer;
    public AudioMixerGroup effectsGroup;
    public AudioMixerGroup fadeGroup;
    public AudioMixerGroup dialogGroup;

    AudioSource dSource;

    public bool dPlaying;
    // Start is called before the first frame update
    void Awake()
    {
        dPlaying = false;
        foreach(Sound s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }

        dSource = gameObject.AddComponent<AudioSource>();
        dSource.outputAudioMixerGroup = dialogGroup;
        foreach(Sound s in dialog)
        {
            s.source = dSource;
        }
    }

    public Sound soundLookUp(String s)
    {
        Sound a = Array.Find(dialog, sound => sound.name == s);
        if(a==null)
        {
            a = Array.Find(soundEffects, sound => sound.name == s);
            
        } 
        if(a == null)
        {
            Debug.Log("no sound found");
        }
        return a;
    }

    public void playEffect(string name)
    {
        if (GameState.paused == false || GameState.endGame == true)
        {
            Sound e = Array.Find(soundEffects, sound => sound.name == name);
            if (e == null)
            {
                Debug.Log("sound " + name + " not found in soundEffects");
                return;
            }
            e.source.outputAudioMixerGroup = effectsGroup;
            if (!dPlaying)
            {

                Sound[] currPlaying = EffectsCurrPlaying();

                if (currPlaying.Length != 0)
                {
                    StartCoroutine(FadeAndPlay("Fade", .025f, 0f, currPlaying, e));
                }
                else
                {
                    e.source.Play();
                }
            }
        }
        
    }

    Coroutine c;
    public void playDialog(string name)
    {
        if (GameState.paused == false || GameState.endGame == true)
        {
            Sound s = Array.Find(dialog, sound => sound.name == name);
            if (s == null)
            {
                return;
            }
            bool interrupt = dPlaying && c != null;
            if (interrupt)
            {
                StopCoroutine(c);

            }

            c = StartCoroutine(PlayDialog(s, interrupt));
        }
        
    }

    public Sound[] EffectsCurrPlaying()
    {
        float currVol;
        mixer.GetFloat("Effects", out currVol);
        mixer.SetFloat("Fade", currVol);
        Sound[] arr = Array.FindAll(soundEffects, sound => sound.source.isPlaying);
        foreach (Sound s in arr)
        {
            s.source.outputAudioMixerGroup = fadeGroup;
        }

        return arr;
    }


    //this code is taken from https://gamedevbeginner.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
    //this one just fades down or up the effect
    bool fading = false;
    public IEnumerator StartFade(string exposedParam, float duration, float targetVolume)
    {
        fading = true;
        //exposed parameter is the variable that links to the audioMixerGroup being controlled in this fade.
        //this code puts x(the volume) on a scale from 0.0001 to 10 it then uses logarithms to smooth the transition as well as revert the volume back to a scale of -80 to 20
        float currTime = 0;
        float currVol;
        mixer.GetFloat(exposedParam, out currVol);
        currVol = Mathf.Pow(10, currVol / 20);//this caps volume at 10(volume in the mixer is on a scale from -80 to 20)
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 10);//0.0001f stops it breaking at 0

        while (currTime < duration)
        {
            currTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currVol, targetValue, currTime / duration);
            mixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        fading = false;
        yield break;
    }

    //this one fades out and stops all sounds
    bool stoping = false;
    public IEnumerator FadeAndStop(string exposedParam, float duration, float targetVolume, Sound[] stop)
    {
        stoping = true;
        StartCoroutine(StartFade(exposedParam, duration, targetVolume));
        yield return new WaitForSeconds(duration+.001f);//that additional number is padding for any lag in the execution of the coroutine
        foreach(Sound s in stop)
        {
            s.source.Stop();
            s.source.outputAudioMixerGroup = null;
        }
        stoping = false;
        yield break;
    }

    //same as above but then it also plays a new sound
    public IEnumerator FadeAndPlay(string exposedParam, float duration, float targetVolume,Sound[] stop, Sound play)
    {
        float currVol;
        mixer.GetFloat(exposedParam, out currVol);
        StartCoroutine(FadeAndStop(exposedParam, duration, targetVolume, stop));
        while (stoping)
        {
            yield return null;
        }
        //if(exposedParam.Equals("Dialog"))
        //{
        //    mixer.SetFloat(exposedParam, currVol);
        //    play.source.clip = play.clip;
        //    play.source.volume = play.volume;
        //}
        play.source.Play();
        yield break;
    }

    public IEnumerator PlayDialog(Sound play, bool interrupt)
    {
        if (!play.playOnce || !play.played)
        {
            play.played = true;
            dPlaying = true;

            //if (!interrupt)
            //{
            //    EffectsCurrPlaying();//moves any sound effect currently playing to the fade group to prep for fading.
            //    StartCoroutine(StartFade("Fade", .025f, .1f));//fades down sound effects
            //}
            //if (dSource.isPlaying)
            //{
            //    dSource.Stop();
            //    //float dialogVol;
            //    //mixer.GetFloat("Dialog", out dialogVol);
            //    //StartCoroutine(StartFade("Dialog", .025f, 0f));
            //    //yield return new WaitForSeconds(.025f);//extra padding on the fade duration, i don't use a while loop here because two things are effecting fading at once
            //    //dSource.Stop();
            //    //mixer.SetFloat("Dialog", dialogVol);
            //}
            //while (fading)//in case the sound effect fade call is still going(this will probably only occur if the above if statement doesn't trigger)
            //{
            //    yield return null;
            //}
            dSource.clip = play.clip;
            dSource.volume = play.volume;
            dSource.Play();
            yield return new WaitForSeconds(play.clip.length);
            //any sound effects still playing are added to this array
            //Sound[] e = Array.FindAll(soundEffects, s => s.source.isPlaying && s.source.outputAudioMixerGroup.Equals(fadeGroup));

            //// if aything is in the array bring volume back up then return the sounds to the sound effect audio group
            //if (e != null)
            //{
            //    //get the volume to fade the audio effects up to
            //    float effectVol;
            //    mixer.GetFloat("Effects", out effectVol);
            //    effectVol = Mathf.Pow(10, effectVol / 20);//changes volume scale from -80 to 20 to 0.001 to 10
            //    StartCoroutine(StartFade("Fade", .025f, effectVol));
            //    while (fading)
            //    {
            //        yield return null;
            //    }

            //    //narrow the array to anything still playing then put that back in the effects group
            //    e = Array.FindAll(e, s => s.source.isPlaying);
            //    if (e != null)
            //    {
            //        foreach (Sound s in e)
            //        {
            //            s.source.outputAudioMixerGroup = effectsGroup;
            //        }
            //    }
            //}

            dPlaying = false;
        }
        yield break;
    }

}
