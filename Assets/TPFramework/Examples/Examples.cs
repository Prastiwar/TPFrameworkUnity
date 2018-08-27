using System;
using UnityEngine;
using System.Collections;
using TPFramework.Unity;
using TPFramework.Core;

public class Examples : MonoBehaviour
{
    public TPAchievementExample TPAchievementExample;
    public TPPersistenceExample TPPersistenceExample;
    public TPObjectPoolExample  TPObjectPoolExample;
    public TPAttributeExample   TPAttributeExample;
    public TPAudioPoolExample   TPAudioPoolExample;
    public TPInventoryExample   TPInventoryExample;
    public TPSettingsExample    TPSettingsExample;
    public TPTooltipExample     TPTooltipExample;
    public TPRandomExample      TPRandomExample;
    public TPFadeExample        TPFadeExample;
    public TPUIExample          TPUIExample;

    private readonly WaitForSeconds waitSecond = new WaitForSeconds(1);
    
    private void Awake()
    {
        DeactiveExamples();
    }

    public void ExampleTPAchievement()
    {
        DeactiveExamples();
        TPAchievementExample ex = TPAchievementExample;
        //ex.Achievement.Data.Points = 0;
        //ex.Achievement.Data.IsCompleted = false;

        ex.Achievement.OnComplete += () => {
            Debug.Log("Achievement completed!");
        };

        TPAchievementManager.OnNotifyActivation = CustomNotifyActive;

        for (int i = 0; i < ex.Achievement.Data.ReachPoints; i++)
        {
            ex.Achievement.AddPoints(1);
        }
    }

    public void ExampleTPPersistence()
    {
        DeactiveExamples();
        TPPersistenceExample.Scene.SetActive(true);
        TPPersistenceExample.LoadButton.onClick.AddListener(Load);
        TPPersistenceExample.SaveButton.onClick.AddListener(Save);
    }

    private void Load()
    {
#if NET_2_0 || NET_2_0_SUBSET
        object boxedExample = TPPersistenceExample;
        boxedExample = TPPersistantPrefs.Load(boxedExample);
        TPPersistenceExample = (TPPersistenceExample)boxedExample;
#else
        TPPersistenceExample = TPPersistantPrefs.Load(TPPersistenceExample);
#endif
        DrawLine();
        Debug.Log("Values Loaded");
        TPPersistenceExample.SomeString.ToLog("SomeString: ");
        TPPersistenceExample.SomeBool.ToLog("SomeBool: ");
        TPPersistenceExample.SomeInt.ToLog("SomeInt: ");
        TPPersistenceExample.SomeFloat.ToLog("SomeFloat: ");
        DrawLine();
    }

    private void Save()
    {
        TPPersistantPrefs.Save(TPPersistenceExample);
        DrawLine();
        Debug.Log("Values Saved");
        TPPersistenceExample.SomeString.ToLog("SomeString: ");
        TPPersistenceExample.SomeBool.ToLog("SomeBool: ");
        TPPersistenceExample.SomeInt.ToLog("SomeInt: ");
        TPPersistenceExample.SomeFloat.ToLog("SomeFloat: ");
        DrawLine();
    }


    public void ExampleTPObjectPool()
    {
        DeactiveExamples();
        TPObjectPoolExample ex = TPObjectPoolExample;

        TPObjectPool.CreatePool(ex.PoolKey, ex.Prefab, ex.PoolCount, 10);
        StartCoroutine(TPObjectPoolSpawnObjects(ex, 20));
    }


    public void ExampleTPAttribute()
    {
        DeactiveExamples();
        TPAttributeExample ex = TPAttributeExample;
        ex.Health.Recalculate(); // we need to call this since changes from editor doesnt call it
        object goldenArmor = new object(); // some Item
        ex.HealthIncreaser.Source = goldenArmor;
        DrawLine();
        Debug.Log("TPAttribute Health Base value: " + ex.Health.BaseValue);
        Debug.Log("TPAttribute Health Value before armor equip: " + ex.Health.Value);
        ex.Health.Modifiers.Add(ex.HealthIncreaser);
        Debug.Log("TPAttribute Health Value after armor equip: " + ex.Health.Value);
        DrawLine();
    }


    public void ExampleTPAudioPool()
    {
        DeactiveExamples();
        TPAudioPoolExample ex = TPAudioPoolExample;

        TPAudio.AddToPool("MyBundle", ex.AudioBundle);
        StartCoroutine(TPAudioPoolRepeatPlaying(5));
    }


    public void ExampleTPInventory()
    {
        DeactiveExamples();
        //TPInventoryExample ex = TPInventoryExample;

        throw new NotImplementedException();
    }


    public void ExampleTPSettings()
    {
        DeactiveExamples();
        TPSettingsExample ex = TPSettingsExample;
        ex.Scene.SetActive(true);

        TPSettings.SetAnisotropicToggler(ex.AniosotropicToggler);
        TPSettings.SetFullScreenToggler(ex.FullScreenToggler);
        TPSettings.SetVSyncToggler(ex.VsyncToggler);

        TPSettings.SetMusicToggler(ex.MusicToggler, ex.Mixer, ex.MusicFloat);
        TPSettings.SetSoundFXToggler(ex.SfxToggler, ex.Mixer, ex.SfxFloat);
        TPSettings.SetMusicVolumeSlider(ex.MusicSlider, ex.Mixer, ex.MusicFloat);
        TPSettings.SetSoundFXVolumeSlider(ex.SfxSlider, ex.Mixer, ex.SfxFloat);

        TPSettings.SetAntialiasingDropdown(ex.Antialiasing);
        TPSettings.SetResolutionDropdown(ex.Resolution);
        TPSettings.SetShadowQualityDropdown(ex.ShadowQuality);
        TPSettings.SetShadowResolutionDropdown(ex.ShadowResolution);
        TPSettings.SetTextureDropdown(ex.Texture);
        TPSettings.SetQualityDropdown(ex.Quality);
    }


    public void ExampleTPTooltip()
    {
        DeactiveExamples();
        TPTooltipExample ex = TPTooltipExample;
        ex.Scene.SetActive(true);
    }


    public void ExampleTPRandom()
    {
        DeactiveExamples();
        TPRandomExample ex = TPRandomExample;
        ex.Scene.SetActive(true);

        int elLength = ex.GameObjects.Length;
        ex.ProbabilityElements = new ProbabilityElementInt<GameObject>[elLength];
        int[] randomProbabilities = TPFramework.Core.TPRandom.RandomProbabilities(elLength);

        DrawLine();
        for (int i = 0; i < elLength; i++)
        {
            ex.ProbabilityElements[i] = new ProbabilityElementInt<GameObject>(ex.GameObjects[i], randomProbabilities[i]);
            Debug.Log("Random probability: " + ex.ProbabilityElements[i].Probability);
        }
        DrawLine();

        StartCoroutine(TPRandomToggleObject(15, ex));
    }


    public void ExampleTPFade()
    {
        DeactiveExamples();
        TPFadeExample ex = TPFadeExample;
        TPFadeExample.Scene.SetActive(true);
        ex.FadeInfo.TPFade = ex.AlphaFade;
        ex.FadeButton.onClick.AddListener(() => {
            TPFade.Fade(ex.FadeInfo);
        });
    }


    public void ExampleTPUI()
    {
        DeactiveExamples();
        TPUIExample ex = TPUIExample;
        TPUIExample.Scene.SetActive(true);

        ex.ModalWindow.Initialize();
        ex.WindowEnabled = false;
        ex.ModalWindow.OnShow = () => CustomModalWindowPop();
        ex.ModalWindow.OnHide = () => CustomModalWindowPop();

        ex.ToggleWindowBtn.onClick.AddListener(() => {
            ex.WindowEnabled = !ex.WindowEnabled;
            if (ex.WindowEnabled)
                ex.ModalWindow.Show();
            else
                ex.ModalWindow.Hide();
        });
    }


    /*------------------------------------------------------ Helpers to examples ------------------------------------------------------*/


    private IEnumerator TPRandomToggleObject(int repeat, TPRandomExample ex)
    {
        while (repeat >= 0)
        {
            GameObject selectedObject = TPFramework.Core.TPRandom.PickWithProbability(ex.ProbabilityElements);
            selectedObject.SetActive(!selectedObject.activeSelf);
            repeat--;
            yield return waitSecond;
        }
    }

    private IEnumerator TPAudioPoolRepeatPlaying(int repeat)
    {
        while (repeat >= 0)
        {
#if NET_2_0 || NET_2_0_SUBSET
            TPAudio.Play(this, "MyBundle", "door", () => {
                MessageWithLines("TPAudioPool Sound 'door' was played by MyBundle");
            });
#else
            TPAudio.Play("MyBundle", "door", () => {
                MessageWithLines("TPAudioPool Sound 'door' was played by MyBundle");
            });
#endif
            repeat--;
            yield return waitSecond;
        }
    }

    private IEnumerator TPObjectPoolSpawnObjects(TPObjectPoolExample ex, float last)
    {
        while (last >= 0)
        {
            TPObjectPool.ToggleActive(ex.PoolKey, TPObjectState.Deactive, TPFramework.Unity.TPRandom.InsideUnitSquare() * 5, true);
            last--;
            yield return waitSecond;
        }
        // ** Alternative: **
        //while (last >= 0)
        //{
        //    GameObject obj = TPObjectPool.PopObject(ex.PoolKey, TPObjectState.Deactive, true);
        //    obj.transform.position = GetRandomPosition();
        //    TPObjectPool.ToggleActive(ex.PoolKey, obj, true);
        //    last--;
        //    yield return waitSecond;
        //}
    }

    private void CustomNotifyActive(float evaluatedTime, Transform notify)
    {
        notify.localScale = notify.localScale.Equal(TPAnim.ReflectNormalizedCurveTime(evaluatedTime));
    }

    private void CustomModalWindowPop(/*float evaluatedTime, Transform window*/)
    {
        //window.localScale = window.localScale.Equal(TPAnim.ReflectNormalizedCurveTime(evaluatedTime));
    }

    private void MessageWithLines(string message)
    {
        DrawLine();
        Debug.Log(message);
        DrawLine();
    }

    private void DrawLine()
    {
        Debug.Log("-------------------------------------------------------------");
    }

    private void DeactiveExamples()
    {
        TPFadeExample.Scene.SetActive(false);
        TPRandomExample.Scene.SetActive(false);
        TPSettingsExample.Scene.SetActive(false);
        TPTooltipExample.Scene.SetActive(false);
        TPUIExample.Scene.SetActive(false);
        TPPersistenceExample.Scene.SetActive(false);
    }
}
