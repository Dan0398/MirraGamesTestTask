using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Dan398.App
{
    public sealed class AddressablesSceneLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference scene;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text label;

        private void Start()
        {
            _ = Addressables.InitializeAsync().Task;
        }

        public void Button_LoadClicked()
        {
            button.interactable = false;
            label.text = "Загрузка...";
            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                await Addressables.InitializeAsync().Task;
                await scene.LoadSceneAsync(LoadSceneMode.Single).Task;
            }
            catch (Exception exception)
            {
                Debug.LogError($"Scene loading failed: {exception}");
                label.text = "Ошибка загрузки";
                button.interactable = true;
            }
        }
    }
}