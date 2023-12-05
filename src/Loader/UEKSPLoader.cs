namespace UnityExplorer
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class UnityExplorerKSPLoader : MonoBehaviour
    {
        void Start()
        {
            ExplorerStandalone.CreateInstance();
            Destroy(gameObject);
        }
    }
}
