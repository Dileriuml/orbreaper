using System;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using UnityEngine;

namespace MvvmCross.Platforms.Unity
{
    public class MvxMonoBehavior<TView, TViewModel> : MonoBehaviour, IMvxView<TViewModel>, IMvxBindingContextOwner
        where TView : MvxMonoBehavior<TView, TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        private bool isDesposing;

        public IMvxBindingContext BindingContext { get; set; }

        IMvxViewModel IMvxView.ViewModel
        {
            get => DataContext as IMvxViewModel;
            set => DataContext = value;
        }

        public TViewModel ViewModel
        {
            get => DataContext as TViewModel;
            set => DataContext = value;
        }

        [MvxSetToNullAfterBinding]
        public object DataContext
        {
            get => BindingContext.DataContext;
            set => BindingContext.DataContext = value;
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnStart()
        {
        }

        private void Start()
        {
            OnStart();
        }

        private void Awake()
        {
            this.CreateBindingContext();
            OnAwake();
            Bind();
        }

        private void OnDestroy()
        {
            if (isDesposing)
            {
                BindingContext.ClearAllBindings();
            }
        }
        
        private void Bind()
        {
            var bindingSet = this.CreateBindingSet<MvxMonoBehavior<TView, TViewModel>, TViewModel>();
            DoBinding(bindingSet);
            bindingSet.Apply();
        }

        protected virtual void DoBinding(MvxFluentBindingDescriptionSet<MvxMonoBehavior<TView, TViewModel>, TViewModel> bindingDescriptionSet)
        {
        }
    }
}