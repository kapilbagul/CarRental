using System.Collections.Generic;
using System.ComponentModel;
using Core.Common.Utils;
using System.Linq.Expressions;
using System;
using System.ComponentModel.Composition.Hosting;

namespace Core.Common
{
    public abstract class ObjectBase : INotifyPropertyChanged
    {
        public static CompositionContainer Container { get; set; }

        private event PropertyChangedEventHandler _PropertyChanged;
        List<PropertyChangedEventHandler> _PropertyChangedSubscribers 
            = new List<PropertyChangedEventHandler>();

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (!_PropertyChangedSubscribers.Contains(value))
                {
                    _PropertyChanged += value;
                    _PropertyChangedSubscribers.Add(value);
                }

            }
            remove
            {
                if (!_PropertyChangedSubscribers.Contains(value))
                {
                    _PropertyChanged -= value;
                    _PropertyChangedSubscribers.Add(value);
                }

            }
        }

        protected virtual void OnProprtyChanged(string propertyName)
        {
           OnProprtyChanged(propertyName,true);
        }
        protected virtual void OnProprtyChanged(string propertyName, bool makeDirty)
        {
            if (_PropertyChanged != null)
            {
                _PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            if (makeDirty)
                _isDirty = true;
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnProprtyChanged(propertyName);
        }
        
        bool _isDirty;

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                _isDirty = value;
            }
        }
    }
}
