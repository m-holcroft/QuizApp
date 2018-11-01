using System;
using Xamarin.Forms;

namespace QuizApp.Common.Behaviours
{
    /// <summary>
    /// Base class template for a behaviour to be attached to a UI control
    /// </summary>
    /// <param name="bindable"></param>
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        /// <summary>
        /// The object the behaviour will be associated with, typically a UI control
        /// </summary>
        public T AssociatedObject { get; private set; }


        /// <summary>
        /// Override the standard OnAttachedTo method. Takes the UI control as a parameter, assigns binding context and attaches the behaviour.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        /// <summary>
        /// Override the standard OnDetachingFrom method. Takes the UI control as a parameter, removes the behaviour from the object.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
