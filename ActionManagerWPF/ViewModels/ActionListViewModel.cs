using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ActionManager.BLL;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using WPF.Views;

namespace WPF.ViewModels
{
    public class ActionListViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }



        private IActionsRepository actionsRep;
        private ObservableCollection<TblAction> actions;
        public ICommand NavigateToUserDetailCommand { get; }



        public ObservableCollection<TblAction> ActionList
        { get { return actions; } 
            set {
                actions = value;
                OnPropertyChanged(nameof(ActionList));
            }
        }

        public ActionListViewModel(IActionsRepository actionsRep)
        {
            this.actionsRep = actionsRep;
            Update();
        }



        public void Update()
        {
            var actions = actionsRep.GetList();
            ActionList = new ObservableCollection<TblAction>(actions);
        }

        public void PrintPast()
        {
            var actions = actionsRep.GetPastList();
            ActionList = new ObservableCollection<TblAction>(actions);
        }

        public void PrintPresent()
        {
            var actions = actionsRep.GetPresentList();
            ActionList = new ObservableCollection<TblAction>(actions);
        }

        public void PrintFuture()
        {
            var actions = actionsRep.GetFutureList();
            ActionList = new ObservableCollection<TblAction>(actions);
        }

    }
}
