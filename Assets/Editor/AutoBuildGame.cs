using SGGames.Scripts.Data;
using UnityEditor;

public class AutoBuildGame
{
    [MenuItem("Build/PC Build x64")]
    public static void AutoBuild()
    {
        var buildVersionAsset = AssetDatabase.LoadAssetAtPath<BuildVersion>("Assets/_Data/BuildVersion.asset");
        buildVersionAsset.IncreaseBuildNumber(increaseMajor:false,increaseMinor:true);
        AssetDatabase.SaveAssets();
        
        PlayerSettings.bundleVersion = buildVersionAsset.GetCurrentBuildNumber();
        PlayerSettings.companyName = "SG Games";
        PlayerSettings.productName = "Final Roguelike";
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = buildVersionAsset.BuildPath,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneWindows64,
            subtarget = 0,
            options = BuildOptions.None,
        };
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
