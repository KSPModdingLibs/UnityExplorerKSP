namespace UnityExplorer
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class UEKSPLoader : MonoBehaviour
    {
        void Start()
        {
            ExplorerStandalone.CreateInstance();
            Destroy(gameObject);
        }
    }
}
