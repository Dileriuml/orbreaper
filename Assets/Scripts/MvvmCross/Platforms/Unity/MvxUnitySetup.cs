using MvvmCross.Base;
using MvvmCross.Binding;
using MvvmCross.IoC;
using Zenject;

namespace MvvmCross.Platforms.Unity
{
    public class MvxUnitySetup : IInitializable
    {
        public void Initialize()
        {
            var container = MvxIoCProvider.Initialize();
            Init(container);
        }
        
        private void Init(IMvxIoCProvider iocProvider)
        {
            InitializeBindingBuilder(iocProvider);
            RegisterMainThreadDispatcher(iocProvider);
        }

        private void RegisterMainThreadDispatcher(IMvxIoCProvider iocProvider)
        {
            var mainThreadDispatcher =
                iocProvider.ConstructAndRegisterSingleton<IMvxMainThreadAsyncDispatcher, MvxUnityMainThreadDispatcher>();
            iocProvider.RegisterSingleton<IMvxMainThreadDispatcher>(mainThreadDispatcher);
        }

        private void InitializeBindingBuilder(IMvxIoCProvider iocProvider)
        {
            var bindingBuilder = CreateBindingBuilder();
            RegisterBindingBuilderCallbacks(iocProvider);
            bindingBuilder.DoRegistration(iocProvider);
        }

        private void RegisterBindingBuilderCallbacks(IMvxIoCProvider iocProvider)
        {
            // iocProvider.CallbackWhenRegistered<IMvxValueConverterRegistry>(FillValueConverters);
            // iocProvider.CallbackWhenRegistered<IMvxTargetBindingFactoryRegistry>(FillTargetFactories);
            // iocProvider.CallbackWhenRegistered<IMvxBindingNameRegistry>(FillBindingNames);
        }

        private MvxBindingBuilder CreateBindingBuilder() => new MvxUnityBindingBuilder();
    }
}