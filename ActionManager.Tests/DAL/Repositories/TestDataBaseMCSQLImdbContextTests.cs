using ActionManager.DAL.Repositories.Concreate;
using ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using ActionManager.DAL.Data;

namespace ActionManager.Tests.DAL.Repositories
{
    public class Tests
    {
        [Test]
        public void TestCreatedAction_ReturnsCreatedAction()
        {
            var context = new ActionManagerContext(0);
            var actionrep = new ActionManagerActionsRepository(context);

            var product = context.TblProducts.SingleOrDefault(p => p.ProductName == "Milk");
            var typeaction = context.TblTypeActions.Find(2);
            var actionCreated = new TblAction()
            {
                ProductId = product.ProductId,
                DiscountPercentage = Convert.ToDecimal(30.00),
                TypeActionId = 2,
                TypeAction = typeaction,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            actionrep.Create(actionCreated);

            var actualAction = context.TblActions.ToList()[context.TblActions.Count() - 1];

            Assert.That(actualAction, Is.EqualTo(actionCreated));
        }

        [Test]
        public void TestCreatedUser_ReturnsCreatedUser()
        {
            var context = new ActionManagerContext(0);
            var userrep = new ActionManagerUserRepository(context);

            string username = "Artemm";
            string password = "Serb";

            string salt = userrep.GenerateSalt(username, password);
            string hashpassword = userrep.EncryptPassword(password, salt);
            bool is_true = userrep.VerifyPassword(password + salt, hashpassword);

            TblUser userCreated = new TblUser
            {
                Username = username,
                Password = hashpassword,
                Salt = salt
            };

            userrep.Create(userCreated);

            var actualUser = context.TblUsers.ToList()[context.TblUsers.Count() - 1];

            userrep.Delete(userCreated);

            Assert.That(userCreated.Username, Is.EqualTo(username));
        }

        [Test]
        public void TestDeletedAction_ReturnsNone()
        {
            var context = new ActionManagerContext(0);
            var actionrep = new ActionManagerActionsRepository(context);

            var product = context.TblProducts.SingleOrDefault(p => p.ProductName == "Milk");
            var typeaction = context.TblTypeActions.Find(2);
            var actionCreated = new TblAction()
            {
                ProductId = product.ProductId,
                DiscountPercentage = Convert.ToDecimal(30.00),
                TypeActionId = 2,
                TypeAction = typeaction,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            actionrep.Create(actionCreated);

            actionrep.Delete(actionCreated);

            var actualAction = context.TblActions.Find(actionCreated.ActionId);


            Assert.IsTrue(actualAction == null);
        }

        [Test]
        public void TestDeletedUser_ReturnsNone()
        {
            var context = new ActionManagerContext(0);
            var userrep = new ActionManagerUserRepository(context);

            string username = "Artemm";
            string password = "Serb";

            string salt = userrep.GenerateSalt(username, password);
            string hashpassword = userrep.EncryptPassword(password, salt);
            bool is_true = userrep.VerifyPassword(password + salt, hashpassword);

            TblUser userCreated = new TblUser
            {
                Username = username,
                Password = hashpassword,
                Salt = salt
            };

            userrep.Create(userCreated);

            userrep.Delete(userCreated);

            var actualUser = context.TblUsers.Find(userCreated.Username);

            Assert.IsTrue(actualUser == null);
        }

        [Test]
        public void TestUpdatedAction_ReturnsUpdatedAction()
        {
            var context = new ActionManagerContext(0);
            var actionrep = new ActionManagerActionsRepository(context);

            var product = context.TblProducts.SingleOrDefault(p => p.ProductName == "Milk");
            var typeaction = context.TblTypeActions.Find(2);
            var actionCreated = new TblAction()
            {
                ProductId = product.ProductId,
                DiscountPercentage = Convert.ToDecimal(30.00),
                TypeActionId = 2,
                TypeAction = typeaction,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            actionrep.Create(actionCreated);

            actionrep.Update(actionCreated);
            var actionUpdated = context.TblActions.ToList().Last();

            Assert.That(actionCreated, Is.EqualTo(actionUpdated));
        }
    }
}