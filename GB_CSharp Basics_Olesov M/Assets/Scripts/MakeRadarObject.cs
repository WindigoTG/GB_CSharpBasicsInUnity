using UnityEngine;
using UnityEngine.UI;

namespace BallGame
{
    public sealed class MakeRadarObject : MonoBehaviour
    {
        [SerializeField] BonusType _bonusType;
        [SerializeField] private Image _ico;

        private void OnValidate()
        {
            string bonus = _bonusType.ToString();
            string path = "MiniMap/RadarObject" + bonus;
            _ico = Resources.Load<Image>(path);
        }

        private void OnDisable()
        {
            RadarController.RemoveRadarObject(gameObject);
        }

        private void OnEnable()
        {
            RadarController.RegisterRadarObject(gameObject, _ico);
        }
    }

    enum BonusType
    { 
        Necessary,
        Score,
        Speed,
        Invincibility,
        Slow,
        Trap,
        Ground
    }
}