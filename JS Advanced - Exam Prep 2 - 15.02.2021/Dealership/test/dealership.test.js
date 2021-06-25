let dealership = require('../dealership.js');
let { assert } = require('chai');

describe("Tests …", function () {
    describe("newCarCost functionality", function () {

        it("newCarCost should return correct value if car model exist", function () {
            assert.equal(dealership.newCarCost('Audi A6 4K', 30000), 10000);
            assert.equal(dealership.newCarCost('Audi A6 4K', 30000.500), 10000.500);
            assert.equal(dealership.newCarCost('Audi A6 4K', -30000), -50000);
        });

        it("newCarCost should return correct value if car model does not exist", function () {
            assert.equal(dealership.newCarCost('Audi A6 4F', 30000), 30000);
            assert.equal(dealership.newCarCost('Audi A6 4F', 30000.500), 30000.500);
            assert.equal(dealership.newCarCost('Audi A6 4F', -30000), -30000);
        });
    });

    describe("carEquipment functionality", function () {
        it("should return correct value for car extras", function () {
            let arr = ['xenon', 'keyless'];
            assert.deepEqual(dealership.carEquipment(['xenon', 'electric windows', 'keyless', 'fog lights'], [0, 2]), arr)
        });

        it("it should return empty array when there are no indexes", function () {
            let arr = [];
            assert.deepEqual(dealership.carEquipment(['xenon', 'electric windows', 'keyless', 'fog lights'], []), arr)
        });

        it("it should return empty array when there are no extras", function () {
            let arr = [undefined, undefined, undefined];
            assert.deepEqual(dealership.carEquipment([], [2, 3, 4]), arr)
        });
    });

    describe("euroCategory functionality", function () {

        it("euroCategory return correct value for lowest euro category", function () {
            assert.equal(dealership.euroCategory(3), 'Your euro category is low, so there is no discount from the final price!');
        });

        it("euroCategory return correct value for lowest euro category with negative number", function () {
            assert.equal(dealership.euroCategory(-3), 'Your euro category is low, so there is no discount from the final price!');
        });

        it("euroCategory return correct value for lowest euro category with floating-point number", function () {
            assert.equal(dealership.euroCategory(2.5), 'Your euro category is low, so there is no discount from the final price!');
        });

        it("euroCategory return correct value for higher euro category", function () {
            assert.equal(dealership.euroCategory(5), `We have added 5% discount to the final price: 14250.`);
        });

        it("euroCategory return correct value for higher euro category corner", function () {
            assert.equal(dealership.euroCategory(4), `We have added 5% discount to the final price: 14250.`);
        });

    });

    // TODO: …
});
