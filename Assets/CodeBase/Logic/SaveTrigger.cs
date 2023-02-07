using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        private bool _isTriggered;

        private void Awake() => 
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isTriggered)
                return;

            _isTriggered = true;
            
            _saveLoadService.SaveProgress();
            Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) => _isTriggered = false;
    }
}