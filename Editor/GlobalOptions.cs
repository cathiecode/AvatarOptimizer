using Anatawa12.AvatarOptimizer.Processors.SkinnedMeshes;
using UnityEditor;

namespace Anatawa12.AvatarOptimizer;

[InitializeOnLoad]
internal static class GlobalOptions
{
    private const string ToggleMeshValidationSessionKey = "AvatarOptimizer.MeshValidationEnabled";
    private const string ToggleMeshValidationMenuName = "Tools/Avatar Optimizer/Mesh Invariant Validation";
    private const string ApplyTraceAndOptimizeOnPlaySessionKey = "AvatarOptimizer.ApplyOnPlay";
    private const string ApplyTraceAndOptimizeOnPlayMenuName = "Tools/Avatar Optimizer/Apply On Play(Trace and Optimize)";

    public static bool MeshValidationEnabled
    {
        get => MeshInfo2.MeshValidationEnabled;
        set
        {
            MeshInfo2.MeshValidationEnabled = value;
            SessionState.SetBool(ToggleMeshValidationSessionKey, value);
            Menu.SetChecked(ToggleMeshValidationMenuName, value);
        }
    }

    public static bool ApplyTraceAndOptimizeOnPlay
    {
        get => SessionState.GetBool(ApplyTraceAndOptimizeOnPlaySessionKey, false);
        set
        {
            SessionState.SetBool(ApplyTraceAndOptimizeOnPlaySessionKey, value);
            Menu.SetChecked(ApplyTraceAndOptimizeOnPlayMenuName, value);
        }
    }

    static GlobalOptions()
    {
        EditorApplication.delayCall += () =>
        {
            EditorApplication.delayCall += () =>
            {
                MeshValidationEnabled =
                    SessionState.GetBool(ToggleMeshValidationSessionKey, CheckForUpdate.Checker.IsBeta);
                ApplyTraceAndOptimizeOnPlay = SessionState.GetBool(ApplyTraceAndOptimizeOnPlaySessionKey, false);
            };
        };
    }

    [MenuItem(ToggleMeshValidationMenuName)]
    public static void ToggleMeshValidation()
    {
        MeshValidationEnabled = !MeshValidationEnabled;
    }

    [MenuItem(ApplyTraceAndOptimizeOnPlayMenuName)]
    public static void ToggleApplyOnPlay()
    {
        ApplyTraceAndOptimizeOnPlay = !ApplyTraceAndOptimizeOnPlay;
    }
}
