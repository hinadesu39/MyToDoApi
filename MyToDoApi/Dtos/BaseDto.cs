using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyToDoApi.Dtos
{
    public class BaseDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        /// <summary>
        /// 通知更新
        /// </summary>
        /// <param name="propName"></param>
        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
