using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Music")]
    [SerializeField] AudioClip bossLevelMusic;
    [SerializeField] AudioClip levelOneMusic;

    [Header("Sound SFX")]
    [SerializeField] AudioClip gunSFX;
    [SerializeField] AudioClip shotgunSFX;
    [SerializeField] AudioClip coinPickSFX;
    [SerializeField] AudioClip gemPickSFX;
    [SerializeField] AudioClip healSFX;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Gun.OnShoot += HandleWeaponAudio;
        // WeaponSwitchingManager.OnWeaponSelectChange += SetItemType;
        Coin.OnCoinCollected += HandleCoinAudio;
        Gem.OnGemCollected += HandleGemAudio;
        Health.OnHealthAdd += HandleHealAudio;
    }

    void OnDisable()
    {
        Gun.OnShoot -= HandleWeaponAudio;
        Coin.OnCoinCollected -= HandleCoinAudio;
        Gem.OnGemCollected -= HandleGemAudio;
        Health.OnHealthAdd -= HandleHealAudio;
    }

    void HandleWeaponAudio(Gun sender)
    {
        if (sender.CurrentAmmo > 0)
        {
            switch (sender.WeaponType)
            {
                case WeaponSO.WeaponType.Pistol:
                case WeaponSO.WeaponType.Rifle:
                    audioSource.PlayOneShot(gunSFX);
                    break;
                case WeaponSO.WeaponType.Shotgun:
                    audioSource.PlayOneShot(shotgunSFX);
                    break;
            }
        }
    }

    public void ChangeLevelMusic(string levelName)
    {
        audioSource.Stop();
        switch (levelName)
        {
            case LevelsEnum.LevelOne:
            case LevelsEnum.LevelTwo:
            case LevelsEnum.LevelThree:
            case LevelsEnum.LevelFour:
                audioSource.clip = levelOneMusic;
                break;
            case LevelsEnum.BossLevel:
                audioSource.clip = bossLevelMusic;
                break;
        }
        audioSource.Play();
        audioSource.loop = true;
    }

    // TODO move the collectables sound effect to their own scriptable object, so it will be more maintainable and cleaner
    void HandleCoinAudio(Coin coin)
    {
        audioSource.PlayOneShot(coinPickSFX);
    }

    void HandleGemAudio()
    {
        audioSource.PlayOneShot(gemPickSFX);
    }

    void HandleHealAudio()
    {
        audioSource.PlayOneShot(healSFX);
    }
}
