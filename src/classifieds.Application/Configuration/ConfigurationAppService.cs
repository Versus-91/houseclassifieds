﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using classifieds.Configuration.Dto;

namespace classifieds.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : classifiedsAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
