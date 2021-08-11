let numberOperations = require('../03. Number Operations_Resources.js');
let { assert } = require('chai');

describe('testing functionality of numberOperations', () => {

    it('should work succsessfully', () => {
        assert.equal(numberOperations.powNumber(5), 5 * 5);
        assert.equal(numberOperations.powNumber(5.5), 5.5 * 5.5);
        assert.equal(numberOperations.powNumber(-3), -3 * -3);
        assert.equal(numberOperations.powNumber(-3.2), -3.2 * -3.2);
    })

    it('should throw error NaN', () => {
        assert.throw(() => numberOperations.numberChecker('yes'), 'The input is not a number!');
        assert.throw(() => numberOperations.numberChecker("hello"), 'The input is not a number!');
    })

    it('should check number range below 100', () => {
        assert.equal(numberOperations.numberChecker(92), 'The number is lower than 100!');
        assert.equal(numberOperations.numberChecker(-92), 'The number is lower than 100!');
        assert.equal(numberOperations.numberChecker(92.25), 'The number is lower than 100!');
    })

    it('should check number range above 100', () => {
        assert.equal(numberOperations.numberChecker(108), 'The number is greater or equal to 100!');
        assert.equal(numberOperations.numberChecker(108.25), 'The number is greater or equal to 100!');
    })

    it('should check number parsing', () => {
        assert.equal(numberOperations.numberChecker('195'), 'The number is greater or equal to 100!');
        assert.equal(numberOperations.numberChecker('195.25'), 'The number is greater or equal to 100!');
    })

    it('should sum two arrays', () => {
        let firstArr = [0, 1, 2, 3];
        let secondArr = [3, 2, 1, 0];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [3, 3, 3, 3]);
    })

    it('should sum two arrays with floating point', () => {
        let firstArr = [0.5, 1, 2.5, 3];
        let secondArr = [3, 2, 1.7, 0];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [3 + 0.5, 3, 2.5 + 1.7, 3]);
    })

    it('should sum two arrays with negative numbers', () => {
        let firstArr = [-0.5, 1, 2.5, 3];
        let secondArr = [3, 2, 1.7, 0];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [-0.5 + 3, 3, 2.5 + 1.7, 3]);
    })

    it('should sum two arrays part two', () => {
        let firstArr = [0, 1, 2, 3, 5];
        let secondArr = [3, 2, 1, 0];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [3, 3, 3, 3, 5]);
    })

    it('should sum two arrays part three', () => {
        let firstArr = [0, 1, 2, 3];
        let secondArr = [3, 2, 1, 0 , 5];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [3, 3, 3, 3, 5]);
    })

    it('should sum two arrays part four', () => {
        let firstArr = [0, 1, 2, 3];
        let secondArr = [3, 2, 1, 0 , 5, 6];
        let arraySummed = numberOperations.sumArrays(firstArr, secondArr);
        assert.deepEqual(arraySummed, [3, 3, 3, 3, 5, 6]);
    })
});