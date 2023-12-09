using VContainer;

namespace FrameworkUnity.OOP.VContainer.Installers
{
    public class SceneLifetimeScope : BaseGameInstallerVContainer
    {
        protected override void ConfigureSystems(IContainerBuilder builder)
        {
            ConfigureGameSystems(builder);
        }

        private void ConfigureGameSystems(IContainerBuilder builder)
        {
            
        }
    }
}
