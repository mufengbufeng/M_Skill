using System.Globalization;
using GameFramework.Fsm;
using GameFramework.Procedure;

public class LaunchProcedure : ProcedureBase
{
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        InitSettings();
        ChangeState(procedureOwner, GFBuiltin.Base.EditorResourceMode ? typeof(LoadHotfixDllProcedure) : typeof(UpdateResourcesProcedure));
    }

    private void InitSettings()
    {
        CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        GFBuiltin.Debugger.ActiveWindow = AppSettings.Instance.DebugMode;
        GFBuiltin.Debugger.WindowScale = 1.4f;
    }
}
