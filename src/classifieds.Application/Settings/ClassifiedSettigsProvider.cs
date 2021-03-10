using Abp.Configuration;
using classifieds.Settings.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Settings
{
    public class ClassifiedSettigsProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context) => new[]
                    {
                    new SettingDefinition(
                        SiteSettings.NumberOfAds,
                        "3"
                        ),
                   new SettingDefinition(
                        SiteSettings.ExpirationDays,
                        "30"
                        ),
                    new SettingDefinition(
                        SiteSettings.About,
                        ""
                        ),
                };
    }
}
