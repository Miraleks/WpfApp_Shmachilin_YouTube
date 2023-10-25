using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp_Shmachilin_YouTube.Infrastructure.Commands;
using WpfApp_Shmachilin_YouTube.ViewModels.Base;

namespace WpfApp_Shmachilin_YouTube.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        # region Заголовок окна
        private string _Title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>

        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();
            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status : string - Статус программы

        private string _Status = "Ready";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region Команды

        #region CloseAplicationCommand
        public ICommand CloseAplicationCommand { get; }

        private bool CanCloseAplicationCommandExecute(object p) => true;

        private void OnCloseAplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        } 
        #endregion


        #endregion

        public MainWindowViewModel() 
        {
            #region Команды

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecute);

            #endregion

        }

    }
}
