using ActionManager.BLL;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using ActionManagerWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Unity;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class ActionListView : Window
    {
        ActionListViewModel actionListViewModel;
        CollectionViewSource actionCollection;
        public ActionListView(ActionListViewModel vm)
        {
            actionListViewModel = vm;
            DataContext = vm;
            InitializeComponent();

            actionCollection = (CollectionViewSource)(Resources["ActionCollection"]);
        }

        public void AddAction_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity
                ITypeActionsRepository typeActionsRepository = ((App)Application.Current).Container.Resolve<ITypeActionsRepository>();
                IProductsRepository productsRepository = ((App)Application.Current).Container.Resolve<IProductsRepository>();


                var actionChangeViewModel = new ActionAddViewModel(actionRepository, typeActionsRepository, productsRepository);
                var actionChangeWindow = new ActionAddView(actionChangeViewModel);
                actionChangeWindow.ShowDialog();
                actionListViewModel.Update();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dataGrid = FindParent<DataGrid>(button);
                if (dataGrid != null)
                {
                    var selectedProduct = dataGrid.SelectedItem as TblAction;

                    // Now 'selectedProduct' holds the object in the selected row.

                    if (selectedProduct != null)
                    {
                        IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity
                        ITypeActionsRepository typeActionsRepository = ((App) Application.Current).Container.Resolve<ITypeActionsRepository>();

                        var actionChangeViewModel = new ActionChangeViewModel(selectedProduct, actionRepository, typeActionsRepository);
                        var actionChangeWindow = new ActionChangeView(actionChangeViewModel);
                        actionChangeWindow.ShowDialog();
                        actionListViewModel.Update();
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dataGrid = FindParent<DataGrid>(button);
                if (dataGrid != null)
                {
                    var selectedProduct = dataGrid.SelectedItem as TblAction;

                    // Now 'selectedProduct' holds the object in the selected row.

                    if (selectedProduct != null)
                    {
                        IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity

                        actionRepository.Delete(selectedProduct);

                        actionListViewModel.Update();
                    }

                }
            }
        }

        public void PrintAllButton_Click(object sender, RoutedEventArgs e)
        {
            IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>();

            actionListViewModel.Update();
        }

        public void PrintPastButton_Click(object sender, RoutedEventArgs e)
        {
            IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>();

            actionListViewModel.PrintPast();
        }

        public void PrintPresentButton_Click(object sender, RoutedEventArgs e)
        {
            IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>();

            actionListViewModel.PrintPresent();
        }

        public void PrintFutureButton_Click(object sender, RoutedEventArgs e)
        {
            IActionsRepository actionRepository = ((App)Application.Current).Container.Resolve<IActionsRepository>();

            actionListViewModel.PrintFuture();
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (true)
            {
                // Get parent item
                DependencyObject parentObject = VisualTreeHelper.GetParent(child);

                // We've reached the end of the tree
                if (parentObject == null) return null;

                // Check if the parent matches the type we're looking for
                if (parentObject is T parent)
                    return parent;

                child = parentObject;
            }
        }
    }
}
