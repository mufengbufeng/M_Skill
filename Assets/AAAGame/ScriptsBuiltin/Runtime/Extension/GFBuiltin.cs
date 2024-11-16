using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class GFBuiltin : MonoBehaviour
{
    public static GFBuiltin Instance { get; private set; }
    public static BaseComponent Base { get; private set; }
    public static ConfigComponent Config { get; private set; }
    public static DataNodeComponent DataNode { get; private set; }
    public static DataTableComponent DataTable { get; private set; }
    public static DebuggerComponent Debugger { get; private set; }
    public static DownloadComponent Download { get; private set; }
    public static EntityComponent Entity { get; private set; }
    public static EventComponent Event { get; private set; }
    public static FsmComponent Fsm { get; private set; }
    public static FileSystemComponent FileSystem { get; private set; }
    public static LocalizationComponent Localization { get; private set; }
    public static NetworkComponent Network { get; private set; }
    public static ProcedureComponent Procedure { get; private set; }
    public static ResourceComponent Resource { get; private set; }
    public static SceneComponent Scene { get; private set; }
    public static SettingComponent Setting { get; private set; }
    public static SoundComponent Sound { get; private set; }
    public static UIComponent UI { get; private set; }
    public static ObjectPoolComponent ObjectPool { get; private set; }
    public static WebRequestComponent WebRequest { get; private set; }
    public static BuiltinViewComponent BuiltinView { get; private set; }
    public static Camera UICamera { get; private set; }

    public static Canvas RootCanvas { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            var resCom = GameEntry.GetComponent<ResourceComponent>();
            if (resCom != null)
            {
                var resTp = resCom.GetType();
                var resourceMode = resTp.GetField("m_ResourceMode", BindingFlags.Instance | BindingFlags.NonPublic);
                resourceMode?.SetValue(resCom, AppSettings.Instance.ResourceMode);
                Log.Info($"------------Set ResourceMode:{AppSettings.Instance.ResourceMode}------------");
            }
        }
    }

    private void Start()
    {
        Base = GameEntry.GetComponent<BaseComponent>(); // 版本检查
        Config = GameEntry.GetComponent<ConfigComponent>();
        DataNode = GameEntry.GetComponent<DataNodeComponent>();
        DataTable = GameEntry.GetComponent<DataTableComponent>();
        Debugger = GameEntry.GetComponent<DebuggerComponent>();
        Download = GameEntry.GetComponent<DownloadComponent>();
        Entity = GameEntry.GetComponent<EntityComponent>();
        Event = GameEntry.GetComponent<EventComponent>();
        Fsm = GameEntry.GetComponent<FsmComponent>();
        Procedure = GameEntry.GetComponent<ProcedureComponent>();
        Localization = GameEntry.GetComponent<LocalizationComponent>();
        Network = GameEntry.GetComponent<NetworkComponent>();
        Resource = GameEntry.GetComponent<ResourceComponent>();
        FileSystem = GameEntry.GetComponent<FileSystemComponent>();
        Scene = GameEntry.GetComponent<SceneComponent>();
        Setting = GameEntry.GetComponent<SettingComponent>();
        Sound = GameEntry.GetComponent<SoundComponent>();
        UI = GameEntry.GetComponent<UIComponent>();
        ObjectPool = GameEntry.GetComponent<ObjectPoolComponent>();
        WebRequest = GameEntry.GetComponent<WebRequestComponent>();
        BuiltinView = GameEntry.GetComponent<BuiltinViewComponent>();

        RootCanvas = UI.GetComponentInChildren<Canvas>();
        UICamera = RootCanvas.worldCamera;

        UpdateCanvasScaler();
    }

    public void UpdateCanvasScaler()
    {
        CanvasScaler canvasScaler = RootCanvas.GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = AppSettings.Instance.DesignResolution;
        var designRatio = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        var canvasFitMode = Screen.width / (float)Screen.height > designRatio ? ScreenFitMode.Height : ScreenFitMode.Width;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = (int)canvasFitMode;
        Log.Info($"----------UI适配模式:{canvasFitMode}----------");
    }


    /// <summary>
    /// 退出或重启
    /// </summary>
    /// <param name="type"></param>
    public static void Shutdown(ShutdownType type)
    {
        GameEntry.Shutdown(type);
    }
}