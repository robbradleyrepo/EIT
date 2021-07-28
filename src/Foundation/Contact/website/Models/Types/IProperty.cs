namespace LionTrust.Foundation.Contact.Models.Types
{
    using System;

    public interface IProperty<T>
    {
        Guid Id { get; }
        string Name { get; }
        T Value { get; set; }
        string Render(bool disableWebEditing = false);
    }
}
