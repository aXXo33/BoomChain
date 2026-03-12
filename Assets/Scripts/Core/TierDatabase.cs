using UnityEngine;

[CreateAssetMenu(fileName = "TierDatabase", menuName = "GravityMerge/TierDatabase")]
public class TierDatabase : ScriptableObject
{
    public static TierDatabase Instance { get; private set; }
    public static readonly string[] TierNames = { "Spark","Ember","Flame","Comet","Nova","Star","Nebula","Pulsar","Quasar","Supernova","Galaxy","Universe" };
    public static readonly int[] ScoreValues = { 1,3,9,27,81,243,729,2187,6561,19683,59049,177147 };
    public static readonly float[] MassValues = { 0.1f,0.2f,0.4f,0.6f,0.8f,1.0f,1.5f,2.0f,3.0f,5.0f,7.0f,10.0f };
    public static readonly float[] ScaleValues = { 0.3f,0.4f,0.5f,0.6f,0.7f,0.8f,0.95f,1.1f,1.3f,1.5f,1.7f,2.0f };
    public void Initialize() { Instance = this; }
}