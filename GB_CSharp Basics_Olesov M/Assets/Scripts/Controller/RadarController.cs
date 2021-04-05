using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BallGame
{
    public sealed class RadarController : IExecuteable
    {
        private Transform _player; 
        private readonly float _mapScale = 2;
        public static List<RadarObject> RadObjects = new List<RadarObject>();
        private Transform _radar;

        public RadarController(Transform player, Transform radar)
        {
            _player = player;
            _radar = radar;
        }

        public static void RegisterRadarObject(GameObject o, Image i)
        {
            Image image = Object.Instantiate(i);
            RadObjects.Add(new RadarObject { Owner = o, Icon = image });
        }

        public static void RemoveRadarObject(GameObject o)
        {
            List<RadarObject> newList = new List<RadarObject>();
            foreach (RadarObject t in RadObjects)
            {
                if (t.Owner == o)
                {
                    Object.Destroy(t.Icon);
                    continue;
                }
                newList.Add(t);
            }
            RadObjects.RemoveRange(0, RadObjects.Count);
            RadObjects.AddRange(newList);
        }

        private void DrawRadarDots() // Синхронизирует значки на миникарте с реальными объектами
        {
            foreach (RadarObject radObject in RadObjects)
            {
                Vector3 radarPos = (radObject.Owner.transform.position -
                                    _player.position);
                float distToObject = Vector3.Distance(_player.position,
                                        radObject.Owner.transform.position) * _mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                radObject.Icon.transform.SetParent(_radar);
                radObject.Icon.transform.position = new Vector3(radarPos.x,
                                                       radarPos.z, 0) + _radar.position;
            }
        }

        public void Execute()
        {
            if (Time.frameCount % 2 == 0)
            {
                DrawRadarDots();
            }
        }
    }

    public sealed class RadarObject
    {
        public Image Icon;
        public GameObject Owner;
    }
}
