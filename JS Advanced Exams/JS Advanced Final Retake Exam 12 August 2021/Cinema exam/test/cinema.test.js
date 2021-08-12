let cinema = require('../cinema.js');
let { assert } = require('chai');

describe("Tests …", function () {

    describe("ShowMovies functionality", function () {

        it("Return error while input is a empty array", function () {
            assert.equal(cinema.showMovies([]), 'There are currently no movies to show.');
        });

        it("Return correct answer", function () {
            let movies = [`King Kong`, "The Tomorrow War", 'Joker', "Spider Man"];
            let string = movies.join(', ')
            assert.equal(cinema.showMovies(movies), 'King Kong, The Tomorrow War, Joker, Spider Man');
            assert.equal(cinema.showMovies(movies), movies.join(', '));
            assert.deepEqual(cinema.showMovies(movies), string);
        });

    });

    describe("TicketPrice functionality", function () {

        it("Return error when invalid projection type 3", function () {
            assert.throw(() => cinema.ticketPrice(22.52), 'Invalid projection type.')
            assert.throw(() => cinema.ticketPrice(22), 'Invalid projection type.')
            assert.throws(() => cinema.ticketPrice(""), 'Invalid projection type.');
            assert.throws(() => cinema.ticketPrice('Spider-Man'), 'Invalid projection type.');
            assert.throws(() => cinema.ticketPrice([]), 'Invalid projection type.');
            assert.throws(() => cinema.ticketPrice({}), 'Invalid projection type.');
            assert.throws(() => cinema.ticketPrice(undefined), 'Invalid projection type.');
            assert.throws(() => cinema.ticketPrice(null), 'Invalid projection type.');
        });

        it("Return correct price for projection type", function () {
            assert.equal(cinema.ticketPrice("Premiere"), 12.00);
            assert.equal(cinema.ticketPrice(`Premiere`), 12.00);
            assert.equal(cinema.ticketPrice('Normal'), 7.50);
            assert.equal(cinema.ticketPrice("Discount"), 5.50);
        });

    });

    describe("swapSeatsInHall functionality", function () {

        it("Return error when seats numbers are negative", function () {
            assert.equal(cinema.swapSeatsInHall(-1, 15), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(1, -15), "Unsuccessful change of seats in the hall.")
        });

        it("Return error when seats numbers are integers 2", function () {
            assert.equal(cinema.swapSeatsInHall(15, 1.25), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(1.25, 15), "Unsuccessful change of seats in the hall.");
        });

        it("Return error when seats numbers are bigger than 20", function () {
            assert.equal(cinema.swapSeatsInHall(21, 5), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(4, 35), "Unsuccessful change of seats in the hall.");
        });

        it("Return correct answer when swap seats", function () {
            assert.equal(cinema.swapSeatsInHall(4, 15), "Successful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(20, 3), "Successful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(20, 5), "Successful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(17, 20), "Successful change of seats in the hall.");
        });

        it("Return wrong answer when swap seats without parameters", function () {
            assert.equal(cinema.swapSeatsInHall(), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(3), "Unsuccessful change of seats in the hall.")
        });

        it("Return wrong answer when swap seats with zero", function () {
            assert.equal(cinema.swapSeatsInHall(0, 5), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(10, 0), "Unsuccessful change of seats in the hall.")
        });

        it("Return wrong answer when swap seats with same numbers", function () {
            assert.equal(cinema.swapSeatsInHall(5, 5), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(11, 11), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall('5', 11), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall('5', '5'), "Unsuccessful change of seats in the hall.");
            assert.equal(cinema.swapSeatsInHall(undefined, NaN), "Unsuccessful change of seats in the hall.");
            
        });
    });

    // TODO: …
});