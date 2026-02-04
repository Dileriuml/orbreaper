using System;
using MvvmCross.Binding;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Platforms.Unity.TargetBinding;
using TMPro;
using UnityEngine.UI;

namespace MvvmCross.Platforms.Unity
{
    public class MvxUnityBindingBuilder : MvxBindingBuilder
    {
        private readonly Action<IMvxTargetBindingFactoryRegistry> _fillRegistryAction;
        private readonly Action<IMvxValueConverterRegistry> _fillValueConvertersAction;
        private readonly Action<IMvxBindingNameRegistry> _fillBindingNamesAction;

        public MvxUnityBindingBuilder(Action<IMvxTargetBindingFactoryRegistry> fillRegistryAction = null,
            Action<IMvxValueConverterRegistry> fillValueConvertersAction = null,
            Action<IMvxBindingNameRegistry> fillBindingNamesAction = null)
        {
            _fillRegistryAction = fillRegistryAction;
            _fillValueConvertersAction = fillValueConvertersAction;
            _fillBindingNamesAction = fillBindingNamesAction;
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<Button>(BindingNames.Click, button => new ButtonClickTargetBinding(button));
            registry.RegisterCustomBindingFactory<Toggle>(BindingNames.IsOn, toggle => new ToggleTargetBinding(toggle));
            
            _fillRegistryAction?.Invoke(registry);
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            _fillValueConvertersAction?.Invoke(registry);
        }

        protected override void FillDefaultBindingNames(IMvxBindingNameRegistry registry)
        {
            base.FillDefaultBindingNames(registry);

            registry.AddOrOverwrite(typeof(Button), BindingNames.Click);
            registry.AddOrOverwrite(typeof(Toggle), BindingNames.IsOn);
            registry.AddOrOverwrite(typeof(TMP_Text), nameof(TMP_Text.text));
            // registry.AddOrOverwrite(typeof(UIButton), MvxIosPropertyBinding.UIControl_TouchUpInside);
            // registry.AddOrOverwrite(typeof(UIBarButtonItem), nameof(UIBarButtonItem.Clicked));
            // registry.AddOrOverwrite(typeof(UISearchBar), MvxIosPropertyBinding.UISearchBar_Text);
            // registry.AddOrOverwrite(typeof(UITextField), MvxIosPropertyBinding.UITextField_Text);
            // registry.AddOrOverwrite(typeof(UITextView), MvxIosPropertyBinding.UITextView_Text);
            // registry.AddOrOverwrite(typeof(UILabel), MvxIosPropertyBinding.UILabel_Text);
            // registry.AddOrOverwrite(typeof(MvxCollectionViewSource), nameof(MvxCollectionViewSource.ItemsSource));
            // registry.AddOrOverwrite(typeof(MvxTableViewSource), nameof(MvxTableViewSource.ItemsSource));
            // registry.AddOrOverwrite(typeof(UIImageView), nameof(UIImageView.Image));
            // registry.AddOrOverwrite(typeof(UIDatePicker), MvxIosPropertyBinding.UIDatePicker_Date);
            // registry.AddOrOverwrite(typeof(UISlider), MvxIosPropertyBinding.UISlider_Value);
            // registry.AddOrOverwrite(typeof(UISwitch), MvxIosPropertyBinding.UISwitch_On);
            // registry.AddOrOverwrite(typeof(UIProgressView), nameof(UIProgressView.Progress));
            // registry.AddOrOverwrite(typeof(UISegmentedControl),
            //     MvxIosPropertyBinding.UISegmentedControl_SelectedSegment);
            // registry.AddOrOverwrite(typeof(UIActivityIndicatorView),
            //     MvxIosPropertyBinding.UIActivityIndicatorView_Hidden);

            _fillBindingNamesAction?.Invoke(registry);
        }
    }
}