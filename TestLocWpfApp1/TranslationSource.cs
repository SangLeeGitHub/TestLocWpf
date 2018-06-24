using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;

namespace TestLocWpfApp1
{ 
    class TranslationSource : INotifyPropertyChanged
    {
        private const char KeySeparator = '_';
        private static readonly TranslationSource instance = new TranslationSource();

        public static TranslationSource Instance
        {
            get { return instance; }
        }

        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture = null;

        public string this[string key]
        {
            
            get
            {
                if (key.IndexOf(KeySeparator) == -1)
                {
                    var res = this.resManager.GetString(key, this.currentCulture);
                    return !string.IsNullOrWhiteSpace(res) ? res : $"[{key}]";
                }
                else
                {
                    var keys = key.Split(KeySeparator);
                    var results = new List<string>();
                    foreach (var k in keys)
                    {
                        var res = this.resManager.GetString(k, this.currentCulture);
                        results.Add(!string.IsNullOrWhiteSpace(res) ? res : $"[{k}]");
                    }
                    return string.Join(" ", results);
                }
            }
        }

        public CultureInfo CurrentCulture
        {
            get { return this.currentCulture; }
            set
            {
                if (this.currentCulture != value)
                {
                    this.currentCulture = value;
                    var @event = this.PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}