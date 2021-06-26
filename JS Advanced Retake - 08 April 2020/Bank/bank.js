class Bank {

    constructor(bankName) {
        this._bankName = bankName;
        this.allCustomers = [];
    }

    newCustomer(customer) {
        // {firstName, lastName, personalId}

        let currentCustomer = this.allCustomers.find(x => x.personalId == customer.personalId);

        if (currentCustomer == null || currentCustomer == undefined || currentCustomer == false) {
            this.allCustomers.push(customer);
            return customer;
        } else {
            throw new Error(`${currentCustomer.firstName} ${currentCustomer.lastName} is already our customer!`);
        }
    }

    depositMoney(personalId, amount) {
        let currentCustomer = this.allCustomers.find(x => x.personalId == personalId);
        
        if (currentCustomer == null || currentCustomer == undefined) {
            throw new Error('We have no customer with this ID!');
        }

        if(!currentCustomer.hasOwnProperty('totalMoney')) {
            currentCustomer.totalMoney = 0;
        }

        if(!currentCustomer.hasOwnProperty('transactions')) {
            currentCustomer.transactions = [];
        }

        currentCustomer.totalMoney = Number(currentCustomer.totalMoney) + Number(amount);
        currentCustomer.transactions.push(`${currentCustomer.firstName} ${currentCustomer.lastName} made deposit of ${amount}$!`);
        return `${currentCustomer.totalMoney}$`;
    }

    withdrawMoney(personalId, amount) {
        let currentCustomer = this.allCustomers.find(x => x.personalId == personalId);

        if (currentCustomer == null || currentCustomer == undefined) {
            throw new Error('We have no customer with this ID!');
        }

        if(!currentCustomer.hasOwnProperty('totalMoney')) {
            currentCustomer.totalMoney = 0;
        }

        if(!currentCustomer.hasOwnProperty('totalMoney')) {
            currentCustomer.totalMoney = 0;
        }

        if (currentCustomer.totalMoney < amount) {
            throw new Error(`${currentCustomer.firstName} ${currentCustomer.lastName} does not have enough money to withdraw that amount!`);
        }

        currentCustomer.totalMoney -= Number(amount);
        currentCustomer.transactions.push(`${currentCustomer.firstName} ${currentCustomer.lastName} withdrew ${amount}$!`);
        return `${currentCustomer.totalMoney}$`;
    }


    customerInfo(personalId) {

        let currentCustomer = this.allCustomers.find(x => x.personalId == personalId);

        if (currentCustomer == null || currentCustomer == undefined) {
            throw new Error('We have no customer with this ID!');
        }

        let output = '';
        output += `Bank name: ${this._bankName}\n`;
        output += `Customer name: ${currentCustomer.firstName} ${currentCustomer.lastName}\n`;
        output += `Customer ID: ${currentCustomer.personalId}\n`;
        output += `Total Money: ${currentCustomer.totalMoney}$\n`;
        output += `Transactions:\n`;
        currentCustomer.transactions = currentCustomer.transactions.sort((a,b) => b.localeCompare(a));
        for (let i = 0; i < currentCustomer.transactions.length; i++) {
            const element = currentCustomer.transactions[i];
            output += `${Number(currentCustomer.transactions.length - i)}. ` + `${currentCustomer.transactions[i]}\n`;
        }
        return output.trim();
    }

}

let bank = new Bank('SoftUni Bank');

console.log(bank.newCustomer({ firstName: 'Svetlin', lastName: 'Nakov', personalId: 6233267 }));

console.log(bank.newCustomer({ firstName: 'Mihaela', lastName: 'Mileva', personalId: 4151596 }));

bank.depositMoney(6233267, 250);
console.log(bank.depositMoney(6233267, 250));
bank.depositMoney(4151596, 555);

console.log(bank.withdrawMoney(6233267, 125));

console.log(bank.customerInfo(6233267));
console.log(bank.customerInfo(4151596));

