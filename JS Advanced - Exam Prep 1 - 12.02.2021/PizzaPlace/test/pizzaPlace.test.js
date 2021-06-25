let pizzUni = require('../pizzaPlace.js');
let { assert } = require('chai');

describe("testing pizzaUni functionality", function () {
   describe("make an order functionality", function () {

      it("should return correct result when making an order", function () {
         let pizza = pizzUni.makeAnOrder({ orderedPizza: 'Margarita', orderedDrink: 'Cola' })
         assert.equal(pizza, `You just ordered Margarita` + ` and Cola.`);
      });

      it("should throw error when making an order", function () {

         assert.throw(() => pizzUni.makeAnOrder({ orderedPizza: undefined, orderedDrink: 'Cola' }), 'You must order at least 1 Pizza to finish the order.');
      });

   });

   describe("getRemainingWork functionality", function () {
      it("should return correct result when cheking for pizza status", function () {
         let pizzasWaiting = pizzUni.getRemainingWork([{ pizzaName: 'Neapolitan Pizza', status: 'preparing' }, { pizzaName: 'Sicilian Pizza', status: 'ready' }, { pizzaName: 'Detroit Pizza', status: 'preparing' }]);
         assert.equal(pizzasWaiting, `The following pizzas are still preparing: Neapolitan Pizza, Detroit Pizza.`);
      });

      it("should return correct result when cheking for pizza status when they are ready", function () {
         let pizzasWaiting = pizzUni.getRemainingWork([{ pizzaName: 'Neapolitan Pizza', status: 'ready' }, { pizzaName: 'Detroit Pizza', status: 'ready' }]);
         assert.equal(pizzasWaiting, 'All orders are complete!');
      });

   });

   describe("orderType functionality", function () {

      it("should return correct result when selecting order type", function () {

         assert.equal(pizzUni.orderType((50,40) , 'Carry Out'), (50,40) - (50, 40 * 0.1));
      });

      it("should return correct result when selecting order type (delivery)", function () {

         assert.equal(pizzUni.orderType((50,40) , 'Delivery'), (50,40));
      });

   });


});
