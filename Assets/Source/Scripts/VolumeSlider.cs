using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour {
    public AudioMixer AudioMixer;
    public AudioSource PreviewSound;
    private static float GameVolume = 1;

    void Start(){
        GetComponent<Slider>().value = GameVolume;
        GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float sliderValue) {
        //Clamp the value so we don't break the audio
        float clampedValue = Mathf.Max(sliderValue, 0.0001f);
        
        float volumeDb = Mathf.Log10(clampedValue) * 20f;
        
        //"MasterVolume" must be explicitly exposed in the AudioMixer window
        AudioMixer.SetFloat("MasterVolume", volumeDb);

        if(PreviewSound != null && !PreviewSound.isPlaying) PreviewSound.Play();
    }
}