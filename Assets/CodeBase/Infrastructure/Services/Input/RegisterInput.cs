using Zenject;

namespace CodeBase.Infrastructure.Services.Input
{
    public class RegisterInput : MonoInstaller
    {
        public override void InstallBindings() => 
            BindInput();

        private void BindInput() => 
            Container.Bind<IInputService>().To<InputService>().AsSingle();
    }
}