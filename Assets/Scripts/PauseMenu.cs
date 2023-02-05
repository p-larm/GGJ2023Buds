using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    public Slider _musicSlider, _sfxSlider;
    private bool paused = false;

    private void Awake() {
        container.SetActive(false);
    }

    public void TogglePauseMenu(InputAction.CallbackContext context){
        if(context.performed) {
            paused = !paused;
            if(paused) {
                container.SetActive(true);
            } else {
                container.SetActive(false);
            }
        }
    }

    public void ToggleMusic() {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX() {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume() {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume() {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void Quit() {
        Application.Quit();
    }
}
