using ActionManager.DAL.Data;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ActionManager.DAL.Repositories.Concreate.DataBaseMCSQLActionManager;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;


namespace ActionManager.Admin.UI
{
    class Menu
    {
        private ImdbContext _context;
        private ActionManagerActionsRepository _actionsRepository;
        private string username;
        private string password;

        public Menu()
        {
            _context = new ImdbContext(1);
            _actionsRepository = new ActionManagerActionsRepository(_context);

            while (Authentication()) { }
        }

        public void Demo()
        {
            while (DemoOnce()) { }
        }

        private bool DemoOnce()
        {
            Console.WriteLine("Select option:\n1. - Print All Dicsounts.\n2. - Print Only Past.\n3. - Print Only Present." +
                "\n4. - Print Only Fututre.\n5. - Crate New Action.\n6. - Change Action.\n7. - Delete Action.\n0. - Login Menu.\n-1 - Exit");
            string userInput = Console.ReadLine();

            try
            {
                switch (userInput)
                {
                    case "1":
                        PrintAllActions();
                        return true;
                    case "2":
                        PrintPastActions();
                        return true;
                    case "3":
                        PrintPresentActions();
                        return true;
                    case "4":
                        PrintFututreActions();
                        return true;
                    case "5":
                        CreateAction();
                        return true;
                    case "6":
                        UpdateAction();
                        return true;
                    case "7":
                        DeleteAction();
                        return true;
                    case "0":
                        while (Authentication()) { }
                        return true;
                    case "-1":
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured, check your input data");
                Console.WriteLine(ex);
                return true;
            }
        }

        public bool Authentication()
        {
            username = "";
            password = "";
            Console.WriteLine("Select option:\n1. - Login.\n-1. - Exit.");
            string userInput = Console.ReadLine();
            try
            {
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("~~~~~Enter Username and Password~~~~~");

                        var userInputList = Console.ReadLine()!.Split(' ');
                        username = userInputList[0];
                        password = userInputList[1];
                        return IsAuthenticated(username, password);
                    case "-1":
                        Console.WriteLine("~~~~~Access terminated~~~~~");
                        Environment.Exit(0);
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured:");
                Console.WriteLine(ex);
                return true;
            }
        }

        public bool IsAuthenticated(string username, string password)
        {
            foreach (var user in _context.TblUsers)
            {
                if (username == user.Username)
                {
                    if (password == user.Password)
                    {
                        Console.WriteLine($"~~~~~Access granted~~~~~" +
                            $"\n~~~~~Welcome {username}~~~~~");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("~~~~~Access denied~~~~~" +
                            "\n~~~~~Wrong Username or Password~~~~~");
                        return true;
                    }
                }
            }
            Console.WriteLine("~~~~~Access denied~~~~~" +
                "\n~~~~~Wrong Username or Password~~~~~");
            return true;
        }

        public void PrintAllActions()
        {
            foreach (var action in _actionsRepository.GetDbSet().Include(a => a.Product).Include(t => t.TypeAction))
            {
                Console.WriteLine($"Action ID: {action.ActionId}, Product Name: {action.Product.ProductName}, Discount Percentage: {action.DiscountPercentage}, Type Action Name: {action.TypeAction.TypeActionName}\n");
            }
        }

        public void PrintPastActions()
        {
            foreach (var action in _actionsRepository.GetDbSet().Include(a => a.Product).Include(t => t.TypeAction))
            {
                if (action.TypeActionId == 1) {
                    Console.WriteLine($"Action ID: {action.ActionId}, Product Name: {action.Product.ProductName}, Discount Percentage: {action.DiscountPercentage}\n");
                }
            }
        }

        public void PrintPresentActions()
        {
            foreach (var action in _actionsRepository.GetDbSet().Include(a => a.Product).Include(t => t.TypeAction))
            {
                if (action.TypeActionId == 2)
                {
                    Console.WriteLine($"Action ID: {action.ActionId}, Product Name: {action.Product.ProductName}, Discount Percentage: {action.DiscountPercentage}\n");
                }
            }
        }

        public void PrintFututreActions()
        {
            foreach (var action in _actionsRepository.GetDbSet().Include(a => a.Product).Include(t => t.TypeAction))
            {
                if (action.TypeActionId == 3)
                {
                    Console.WriteLine($"Action ID: {action.ActionId}, Product Name: {action.Product.ProductName}, Discount Percentage: {action.DiscountPercentage}\n");
                }
            }
        }

        public void CreateAction()
        {
            Console.WriteLine("Enter Product Name and Discount Percentage:");
            var input = Console.ReadLine()!.Split(' ');
            string productName = input[0];
            decimal discountPercentage = Convert.ToDecimal(input[1]);

            var product = _context.TblProducts.SingleOrDefault(p => p.ProductName == productName);
            var typeaction = _context.TblTypeActions.Find(3);
            if (product != null)
            {
                var action = new TblAction()
                {
                    ProductId = product.ProductId,
                    DiscountPercentage = discountPercentage,
                    TypeActionId = 3,
                    TypeAction = typeaction,
                    InsertTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                _actionsRepository.Create(action);
            }
            else
            {
                Console.WriteLine("This product name doesn't exist, try again");
                return;
            }
            Console.WriteLine("Action Added");
        }

        public void UpdateAction()
        {
            Console.WriteLine("Enter Action Id, Discount Percentage and New Type Action Id:");
            var input = Console.ReadLine()!.Split(' ');
            int actionId = Convert.ToInt32(input[0]);
            decimal discountPercentage = Convert.ToDecimal(input[1]);
            int typeactionid = Convert.ToInt32(input[2]);

            var typeaction = _context.TblTypeActions.Find(typeactionid);
            var action = _context.TblActions.Find(actionId);

            if (action != null)
            {
                action.DiscountPercentage = discountPercentage;
                action.TypeActionId = typeactionid;
                action.TypeAction = typeaction;
                action.UpdateTime = DateTime.Now;

                _actionsRepository.Update(action);
            }
            else
            {
                Console.WriteLine("This product name doesn't exist, try again");
                return;
            }
            Console.WriteLine("Action Updated");
        }

        public void DeleteAction()
        {
            Console.WriteLine("Enter Action Id");
            var actionId = Convert.ToInt32(Console.ReadLine());
            var action = _context.TblActions.Find(actionId);
            _actionsRepository.Delete(action);
            Console.WriteLine("Action Deleted");
        }
    }
}