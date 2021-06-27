let { HolidayPackage } = require('../HolidayPackage.js');
let { assert } = require('chai');
const { boolean } = require('yargs');

describe("Testing HolidayPackage functionality", function () {
    let holiday = '';
    this.beforeEach(() => {
        holiday = new HolidayPackage('Czech Republic', 'Summer');
    });

    it("should create new instance of HolidayPackage (testing constructor and properties)", function () {
        assert.equal(holiday.season, 'Summer');
        assert.equal(holiday.destination, 'Czech Republic');
        assert.equal(holiday.vacationers.length, 0);
        assert.equal(holiday.insuranceIncluded, false);
    });

    it("testing showVacationers functionality when there are no vacationers", function () {
        assert.equal(holiday.showVacationers(), "No vacationers are added yet");
    });

    it("testing showVacationers functionality when vacationers are added", function () {
        holiday.addVacationer("Gosho Goshev");
        holiday.addVacationer("Pesho Stoqnov");
        assert.equal(holiday.showVacationers(), "Vacationers:\n" + "Gosho Goshev\n" + "Pesho Stoqnov");
    });

    it("testing AddVacationers functionality when vacationers are added", function () {
        holiday.addVacationer("Gosho Goshev");
        assert.equal(holiday.vacationers.length, 1);
    });

    it("testing AddVacationers functionality when vacationers are added with wrong input", function () {
        assert.throw(() => holiday.addVacationer("Gosho"), "Name must consist of first name and last name");
    });

    it("testing AddVacationers functionality when vacationers are added with wrong input second phase", function () {
        assert.throw(() => holiday.addVacationer(3456), "Vacationer name must be a non-empty string");
        assert.throw(() => holiday.addVacationer(' '), "Vacationer name must be a non-empty string");
    });

    it("testing insuranceIncluded functionality with wrong input", function () {
        assert.throw(() => holiday.insuranceIncluded = 'hello', "Insurance status must be a boolean");
    });

    it("testing insuranceIncluded functionality getter value", function () {
        assert.equal(holiday.insuranceIncluded, false);
    });

    it("testing generateHolidayPackage functionality throw error for missing vacationers", function () {
        assert.throw(() => holiday.generateHolidayPackage(), "There must be at least 1 vacationer added");
    });

    it("testing generateHolidayPackage functionality work correctly for season summer", function () {
        holiday.addVacationer("Gosho Goshev");
        holiday.addVacationer("Stoqn Petrov");
        holiday.addVacationer("Misho Georgiev");

        let expectedResult =
            "Holiday Package Generated\n" +
            "Destination: " + holiday.destination + "\n" +
            holiday.showVacationers() + "\n" +
            "Price: " + 1400;

        assert.equal(holiday.generateHolidayPackage(), expectedResult);
    });

});

