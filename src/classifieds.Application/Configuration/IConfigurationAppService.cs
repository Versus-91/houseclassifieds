using System.Threading.Tasks;
using classifieds.Configuration.Dto;

namespace classifieds.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
