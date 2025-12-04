using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl Instance;

    public AudioSource audioSourcePrefab;
    public int initialPoolSize = 10;

    private AudioSource bgmSource;
    private List<AudioSource> sfxPool = new List<AudioSource>();

    private Dictionary<string, AudioClip> bgmCache = new();
    private Dictionary<string, AudioClip> sfxCache = new();

    public void ActiveSound(bool isActive)
    {
        foreach (var sfx in sfxPool)
            sfx.volume = isActive ? 1 : 0;
    }

    public void ActiveBgm(bool isActive)
    {
        bgmSource.volume = isActive ? 1 : 0;
    }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        bgmSource = Instantiate(audioSourcePrefab, transform);
        bgmSource.loop = true;

        for (int i = 0; i < initialPoolSize; i++)
            CreateSFXSource();


        //cache sounds
        await LoadClipAsync("Sounds/SFX/Coin", sfxCache);
        await LoadClipAsync("Sounds/SFX/Complete", sfxCache);
        await LoadClipAsync("Sounds/SFX/Complete", sfxCache);
        await LoadClipAsync("Sounds/SFX/Pick", sfxCache);
        await LoadClipAsync("Sounds/SFX/Stack", sfxCache);
        await LoadClipAsync("Sounds/SFX/Win", sfxCache);
        await LoadClipAsync("Sounds/SFX/Lose", sfxCache);

        var sound = ConfigSave.GetSound();
        var music = ConfigSave.GetMusic();
        ActiveBgm(music);
        ActiveSound(sound);

       // var haptic = ConfigSave.GetVibrate();
        //HapticController.hapticsEnabled = haptic;
        await LoadClipAsync("Sounds/BGM/BGM", bgmCache);
        PlayBGMAsync("BGM").Forget();
    }

    private AudioSource CreateSFXSource()
    {
        var src = Instantiate(audioSourcePrefab, transform);
        src.loop = false;
        sfxPool.Add(src);
        return src;
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var src in sfxPool)
            if (!src.isPlaying) return src;

        return CreateSFXSource();
    }

    // ====== Async Play Methods ======

    public async UniTaskVoid PlayBGMAsync(string name, float volume = 1f)
    {
        var music = ConfigSave.GetMusic();
        AudioClip clip = await LoadClipAsync("Sounds/BGM/" + name, bgmCache);
        if (clip == null) return;

        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.volume = music ? volume : 0;
            bgmSource.Play();
        }
    }

    public void StopBGM() => bgmSource.Stop();

    public async UniTaskVoid PlaySFXAsync(string name, float volume = 1f)
    {
        var sound = ConfigSave.GetSound();
        if (!sound) return;
        AudioClip clip = await LoadClipAsync("Sounds/SFX/" + name, sfxCache);
        if (clip == null) return;

        var src = GetAvailableSFXSource();
        src.clip = clip;
        src.volume = volume;
        src.Play();
    }

    private async UniTask<AudioClip> LoadClipAsync(string path, Dictionary<string, AudioClip> cache)
    {
        if (cache.TryGetValue(path, out var cached))
            return cached;

        var request = Resources.LoadAsync<AudioClip>(path);
        await UniTask.WaitUntil(() => request.isDone);

        var clip = request.asset as AudioClip;
        if (clip != null)
            cache[path] = clip;
        else
            Debug.LogWarning($"[SoundManager] AudioClip not found at: {path}");

        return clip;
    }
}