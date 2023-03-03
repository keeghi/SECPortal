using System;

namespace SecPortal.Commons.ViewModels
{
    public class KeyValuePair<TKey>
    {
        public TKey Id { get; set; }
        public string Value { get; set; }

        public KeyValuePair(TKey id, string value)
        {
            Id = id;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
