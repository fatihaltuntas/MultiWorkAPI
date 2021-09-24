using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MultiWorkAPI.Localization
{
    public static class MultiWorkAPILocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(MultiWorkAPIConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MultiWorkAPILocalizationConfigurer).GetAssembly(),
                        "MultiWorkAPI.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
