using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using ActionManagerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for ProductBuy.xaml
    /// </summary>
    public partial class ActionChangeView : Window
    {
        public ActionChangeView(ActionChangeViewModel actionChangeViewModel)
        {
            DataContext = actionChangeViewModel;
            InitializeComponent();
            Loaded += ActionChange_Loaded;
        }

        private void ActionChange_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ActionChangeViewModel vm)
            {
                vm.Submited += () =>
                {
                    DialogResult = true;
                    this.Close();
                };
            }
        }
    }
}