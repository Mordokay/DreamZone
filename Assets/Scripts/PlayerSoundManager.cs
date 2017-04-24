using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {

    public AudioSource pickSound;
    public AudioSource shootSound;
    public AudioSource unlockTileSound;

    public void PlayPickSound()
    {
        pickSound.Play();
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void PlayUnlockTileSound()
    {
        unlockTileSound.Play();
    }
}
