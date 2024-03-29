﻿using System;
using System.Windows;

namespace PasswordManager {
    /// <summary>
    /// A base attached property to replace the usual way to create AttachedProperties
    /// </summary>
    /// <typeparam name="Parent">Parent class to be the attached property</typeparam>
    /// <typeparam name="Property">The type of this property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property> where Parent : BaseAttachedProperty<Parent, Property>, new() {

        #region Public Events

        /// <summary>
        /// Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// Fired when the value changes, even when the value is the same
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        #endregion

        #region Public Properties

        /// <summary>
        /// Singleton of the Parent
        /// </summary>
        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region Attached Property Definitions

        /// <summary>
        /// Attached Property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", 
            typeof(Property),
            typeof(BaseAttachedProperty<Parent, Property>),
            new PropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// The callback event when the ValueProperty is changed, even if it is the same value
        /// </summary>
        /// <param name="d">UI Element that had its property changed</param>
        /// <param name="value">The arguments for the event</param>
        /// <returns></returns>
        private static object OnValuePropertyUpdated(DependencyObject d, object value) {
            // call listeners
            Instance.OnValueUpdated(d, value);
            Instance.ValueUpdated(d, value);

            // return the value
            return value;
        }

        /// <summary>
        /// Callback event when the value is changed
        /// </summary>
        /// <param name="d">UI Element that had its property changed</param>
        /// <param name="e">Arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            // Call the parent function
            Instance.OnValueChanged(d, e);
            
            // Call event listener
            Instance.ValueChanged(d, e);

        }

        /// <summary>
        /// Getter for the Attached Property
        /// </summary>
        /// <param name="d">Element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the Attached Property
        /// </summary>
        /// <param name="d">Element to get the property from</param>
        /// <param name="value">The value to set</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods
        /// <summary>
        /// The method that is called when any AttachedProperty of this type is changed
        /// </summary>
        /// <param name="sender">UI element that this property is changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// The method that is called when any AttachedProperty of this type is changed, even if the value is the same
        /// </summary>
        /// <param name="sender">UI element that this property is changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }
        #endregion
    }
}
