using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextNovelReader.Models;

public abstract class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetProperty<T>(ref T member, T value, [CallerMemberName]string? propertyName = null)
        where T : IComparable<T>
    {
        if (member.CompareTo(value) == 0)
            return;
        member = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
    }

    public void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
    }
}
