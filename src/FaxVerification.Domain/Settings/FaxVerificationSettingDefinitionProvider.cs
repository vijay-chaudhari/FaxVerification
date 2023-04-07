using Volo.Abp.Settings;

namespace FaxVerification.Settings;

public class FaxVerificationSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FaxVerificationSettings.MySetting1));
    }
}
