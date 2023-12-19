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
    public class ActionAddViewModel : INotifyPropertyChanged
    {
        private IActionsRepository _actionsRepository;
        private ITypeActionsRepository _typeActionsRepository;
        private IProductsRepository _productsRepository;
        private decimal _discountpercentage;
        private string _selectedTypeProduct;
        public string SelectedTypeProduct
        {
            get { return _selectedTypeProduct; }
            set
            {
                if (_selectedTypeProduct != value)
                {
                    _selectedTypeProduct = value;
                    var list = _selectedTypeProduct.Split(' ');
                    _selectedTypeProduct = list[1];
                    OnPropertyChanged(nameof(SelectedTypeProduct));
                    
                }
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

        public ActionAddViewModel(IActionsRepository actionsRepository, ITypeActionsRepository typeActionsRepository, IProductsRepository productsRepository)
        {
            this._actionsRepository = actionsRepository;
            this._typeActionsRepository = typeActionsRepository;
            _productsRepository = productsRepository;
            SubmitCommand = new SubmitAddCommand(this);
        }

        public void Submit()
        {
            var typeaction = _typeActionsRepository.GetList().SingleOrDefault(a => a.TypeActionName == "Future");
            var product = _productsRepository.GetList().SingleOrDefault(p => p.ProductName == _selectedTypeProduct);

            TblAction action = new TblAction
            {
                Product = product,
                ProductId = product.ProductId,
                DiscountPercentage = _discountpercentage,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                TypeAction = typeaction,
                TypeActionId = typeaction.TypeActionId,
            };

            _actionsRepository.Create(action);

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
