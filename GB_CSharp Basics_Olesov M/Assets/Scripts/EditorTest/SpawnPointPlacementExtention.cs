#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BallGame
{
    [CustomEditor(typeof(SpawnPointPlacement))]
    public class SpawnPointPlacementExtention : UnityEditor.Editor
    {
        private SpawnPointPlacement _target;

        private void OnEnable()
        {
            _target = (SpawnPointPlacement)target;
        }
        private void OnSceneGUI()
        {
            if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
            {
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
                   SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

                if (Physics.Raycast(ray, out var hit) && hit.transform.CompareTag("Ground"))
                {
                    _target.InstantiateObj(hit.point);
                    SetObjectDirty(_target.gameObject);
                }
            }

            Selection.activeGameObject = FindObjectOfType<SpawnPointPlacement>().gameObject;
        }

        public void SetObjectDirty(GameObject obj)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }
        }

    }
}
#endif
