using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private AudioClip gameplayMusic, tensionMusic, mergeSFX, comboSFX, gameOverSFX;
    [SerializeField][Range(0,1)] private float musicVol=0.5f, sfxVol=0.8f;
    private void Awake() { if(Instance==null)Instance=this;else{Destroy(gameObject);return;} musicVol=PlayerPrefs.GetFloat("MusicVol",0.5f); sfxVol=PlayerPrefs.GetFloat("SFXVol",0.8f); }
    public void PlaySFX(AudioClip c,float v=1) { if(sfxSource&&c) sfxSource.PlayOneShot(c,sfxVol*v); }
    public void PlayMergeSFX() => PlaySFX(mergeSFX);
    public void SetMusicVolume(float v) { musicVol=Mathf.Clamp01(v); if(musicSource)musicSource.volume=musicVol; PlayerPrefs.SetFloat("MusicVol",musicVol); }
    public void SetSFXVolume(float v) { sfxVol=Mathf.Clamp01(v); PlayerPrefs.SetFloat("SFXVol",sfxVol); }
}