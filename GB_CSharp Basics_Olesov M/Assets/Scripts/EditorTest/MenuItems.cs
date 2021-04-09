using UnityEditor;

namespace BallGame
{
    public class MenuItems
    {
        [MenuItem("Custom tools/SpawnPoint placement %&#p")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(SpawnPointPlacementWindow), false, "SpawnPoint placement");
        }
    }
}
