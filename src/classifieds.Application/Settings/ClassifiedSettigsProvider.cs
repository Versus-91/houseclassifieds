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
                        SiteSettings.SmsId,
                        "AC86a79773a7497ca6cabafdc0b0f458b0"
                        ),
                   new SettingDefinition(
                        SiteSettings.SmsPassword,
                        "49c2a59e46f286f2852bca8dcae54c6c"
                        ),
                   new SettingDefinition(
                        SiteSettings.SmsNumber,
                        "+14242342502"
                        ),
                    new SettingDefinition(
                        SiteSettings.NumberOfAds,
                        "3"
                        ),
                   new SettingDefinition(
                        SiteSettings.ExpirationDays,
                        "30"
                        ),

                };
    }
}
