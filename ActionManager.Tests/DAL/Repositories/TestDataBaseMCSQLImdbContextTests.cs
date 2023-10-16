using ActionManager.DAL.Repositories.Concreate.DataBaseMCSQL;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace ActionManager.Tests.DAL.Repositories
{
    public class Tests
    {
        [Test]
        public void TestCreatedAction_ReturnsCreatedAction()
        {
            var context = new ImdbContext(0);

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

            context.CreateTblAction(actionCreated);

            var actualAction = context.TblActions.ToList()[context.TblActions.Count() - 1];


            Assert.That(actualAction, Is.EqualTo(actionCreated));
        }

        [Test]
        public void TestDeletedAction_ReturnsNone()
        {
            var context = new ImdbContext(0);

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

            context.CreateTblAction(actionCreated);

            context.DeleteTblAction(actionCreated);

            var actualAction = context.TblActions.Find(actionCreated.ActionId);


            Assert.IsTrue(actualAction == null);
        }

        [Test]
        public void TestUpdatedAction_ReturnsUpdatedAction()
        {
            var context = new ImdbContext(0);

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

            context.CreateTblAction(actionCreated);

            context.UpdateTblAction(actionCreated.ActionId, Convert.ToDecimal(15.00), actionCreated.TypeActionId);
            var actionUpdated = context.TblActions.ToList().Last();

            Assert.That(actionCreated, Is.EqualTo(actionUpdated));
        }
    }
}