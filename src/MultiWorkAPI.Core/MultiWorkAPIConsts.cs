using MultiWorkAPI.Debugging;

namespace MultiWorkAPI
{
    public class MultiWorkAPIConsts
    {
        public const string LocalizationSourceName = "MultiWorkAPI";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "313ef89f9b9d4a1e857f9e390d80fd28";
    }
}
