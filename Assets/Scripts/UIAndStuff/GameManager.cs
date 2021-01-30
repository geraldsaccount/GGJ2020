using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameObject loadingScreen;

    private void Awake() {
        instance = this;

        SceneManager.LoadSceneAsync((int) SceneIndexes.TitleScreen, LoadSceneMode.Additive);
    }

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame() {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TitleScreen));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int) SceneIndexes.Game, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadMainMenu() {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Game));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TitleScreen, LoadSceneMode.Additive));
        
        StartCoroutine(GetSceneLoadProgress());
    }

    public void RestartLevel() {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Game));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Game, LoadSceneMode.Additive));
        
        StartCoroutine(GetSceneLoadProgress());
    }
    
    private IEnumerator GetSceneLoadProgress() {
        for (int i = 0; i < scenesLoading.Count; i++) {
            while (!scenesLoading[i].isDone) {
                yield return null;
            }
        }
        
        loadingScreen.gameObject.SetActive(false);
    }
    
}
