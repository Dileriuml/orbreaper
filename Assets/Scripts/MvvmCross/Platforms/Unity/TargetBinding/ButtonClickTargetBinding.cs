using System;
using System.Windows.Input;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.WeakSubscription;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MvvmCross.Platforms.Unity.TargetBinding
{
    public class ButtonClickTargetBinding : MvxConvertingTargetBinding<Button, ICommand>
    {
        private readonly EventHandler<EventArgs> _canExecuteEventHandler;
        private readonly UnityAction clickListener;
        private ICommand _command;
        private IDisposable _canExecuteSubscription;
        
        public ButtonClickTargetBinding(Button target) : base(target)
        {
            _canExecuteEventHandler = OnCanExecuteChanged;
            clickListener = ViewOnClick;
            target.onClick.AddListener(clickListener);
        }
        
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(Button target, ICommand value)
        {
            _canExecuteSubscription?.Dispose();
            _canExecuteSubscription = null;

            _command = value;
            if (_command != null)
            {
                _canExecuteSubscription = _command.WeakSubscribe(_canExecuteEventHandler);
            }
            
            RefreshEnabledState();
        }

        private void ViewOnClick()
        {
            if (_command == null)
                return;

            if (!_command.CanExecute(null))
                return;

            _command.Execute(null);
        }
        
        private void RefreshEnabledState()
        {
            var view = Target;
            if (view == null)
                return;

            var shouldBeEnabled = false;
            if (_command != null)
            {
                shouldBeEnabled = _command.CanExecute(null);
            }
            
            view.enabled = shouldBeEnabled;
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            RefreshEnabledState();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (Target)
                {
                    Target.onClick.RemoveListener(clickListener);
                }

                _canExecuteSubscription?.Dispose();
                _canExecuteSubscription = null;
            }
            base.Dispose(isDisposing);
        }
    }
}