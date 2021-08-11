class ChristmasDinner {
    constructor(budget) {
        this.budget = budget;
        this.dishes = [];
        this.products = [];
        this.guests = [];
    }

    get budget() {
        return this._budget;
    }
    set budget(value) {

        if (Number(value) < 0) {
            throw new Error("The budget cannot be a negative number");
        }
        this._budget = value;
    }

    shopping(products) {
        let type = products[0];
        let price = Number(products[1]);

        if (this.budget - price < 0) {
            throw new Error("Not enough money to buy this product");
        }
        this.products.push(type);
        this.budget -= price;

        return `You have successfully bought ${type}!`
    }

    recipes(input) {
        let recipeName = input.recipeName;
        let recipeProducts = input.productsList;

        for (const product of recipeProducts) {
            if (this.products.includes(product)) {
                // let removeIdx = this.products.indexOf(product);
                // this.products.splice(removeIdx, 1);
            } else {
                throw new Error("We do not have this product");
            }
        }
        this.dishes.push(input);
        return `${recipeName} has been successfully cooked!`;
    }

    inviteGuests(name, dish) {

        let dinner = this.dishes.find((x) => x.recipeName == dish);
        let guest = this.guests.find((x) => x == name);

        if (dinner == undefined) {
            throw new Error("We do not have this dish");
        }

        if (guest != undefined) {
            throw new Error("This guest has already been invited");
        }

        let newGuest = {
            guestName: name,
            dish: dinner,
        }

        this.guests.push(newGuest);
        return `You have successfully invited ${name}!`;
    }

    showAttendance() {
        let output = '';
        for (let i = 0; i < this.guests.length; i++) {
            const guest = this.guests[i];
            if (i == this.guests.length - 1) {
                output += `${guest.guestName} will eat ${guest.dish.recipeName}, which consists of ${guest.dish.productsList.join(', ')}`;
            } else {
                output += `${guest.guestName} will eat ${guest.dish.recipeName}, which consists of ${guest.dish.productsList.join(', ')}` + '\n';
            }

        }
        return output;
    }
}
let dinner = new ChristmasDinner(300);

dinner.shopping(['Salt', 1]);
dinner.shopping(['Beans', 3]);
dinner.shopping(['Cabbage', 4]);
dinner.shopping(['Rice', 2]);
dinner.shopping(['Savory', 1]);
dinner.shopping(['Peppers', 1]);
dinner.shopping(['Fruits', 40]);
dinner.shopping(['Honey', 10]);

dinner.recipes({
    recipeName: 'Oshav',
    productsList: ['Fruits', 'Honey']
});
dinner.recipes({
    recipeName: 'Folded cabbage leaves filled with rice',
    productsList: ['Cabbage', 'Rice', 'Salt', 'Savory']
});
dinner.recipes({
    recipeName: 'Peppers filled with beans',
    productsList: ['Beans', 'Peppers', 'Salt']
});

dinner.inviteGuests('Ivan', 'Oshav');
dinner.inviteGuests('Petar', 'Folded cabbage leaves filled with rice');
dinner.inviteGuests('Georgi', 'Peppers filled with beans');

console.log(dinner.showAttendance());
