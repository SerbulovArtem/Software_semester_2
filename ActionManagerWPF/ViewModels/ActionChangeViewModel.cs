using ActionManager.BLL;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Commands;
using WPF.Utilities;
using WPF.Views;
using WPF.Commands;
using WPF.Interfaces;
using WPF.Utilities;
using System.Linq;

namespace WPF.ViewModels
{
    public class ActionChangeViewModel : INotifyPropertyChanged
    {
        private TblAction _action;
        private IActionsRepository _actionsRepository;
        private ITypeActionsRepository _typeActionsRepository;
        private decimal _discountpercentage;
        private string _selectedTypeAction;
        public string SelectedTypeAction
        {
            get { return _selectedTypeAction; }
            set
            {
                if (_selectedTypeAction != value)
                {
                    _selectedTypeAction = value;
                    var list = _selectedTypeAction.Split(' ');
                    _selectedTypeAction = list[1];
                    OnPropertyChanged(nameof(SelectedTypeAction));
                    
                }
            }
        }

        public TblAction Action
        {
            get { return _action; }
            set
            {
                _action = value;
                OnPropertyChanged(nameof(Action));
            }
        }

        public decimal DiscountPercentage
        {
            get { return _discountpercentage; }
            set
            {
                _discountpercentage = value;
                OnPropertyChanged(nameof(DiscountPercentage));
            }
        }

        public ICommand SubmitCommand { get; }

        public ActionChangeViewModel(TblAction action, IActionsRepository actionsRepository, ITypeActionsRepository typeActionsRepository)
        {
            _action = action;
            this._actionsRepository = actionsRepository;
            this._typeActionsRepository = typeActionsRepository;
            SubmitCommand = new SubmitChangeCommand(this);
        }

        public void Submit()
        {
            var typeaction = _typeActionsRepository.GetList().SingleOrDefault(a => a.TypeActionName == _selectedTypeAction);
            var action = _actionsRepository.GetList().Find(a => a.ActionId == Action.ActionId);

            action.DiscountPercentage = _discountpercentage;
            action.TypeActionId = typeaction.TypeActionId;
            action.TypeAction = typeaction;
            action.UpdateTime = DateTime.Now;

            _actionsRepository.Update(action);

            // Add your logic here for handling the submit action
            // You can access the entered quantity using Quantity
            // and other details using Product properties.
            // Your business logic goes here.
        }

        public Action Submited { get; set; }
        public bool IsValid
        {
            get
            {
                return _actionsRepository.CheckDiscountPercentage(DiscountPercentage);
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
