class Parking {
    constructor(capacity) {
        this.capacity = capacity;
        this.vehicles = [];
    }

    addCar( carModel, carNumber ) {
        if(this.capacity <= this.vehicles.length) {
            throw new Error("Not enough parking space.");
        }

        let car = {
            carModel,
            carNumber,
            payed: false,
        }
        let existingCar = this.vehicles.find((x) => x.carNumber == carNumber && x.carModel == carModel);

        if(!this.vehicles.includes(existingCar)) {
            this.vehicles.push(car);

            return `The ${carModel}, with a registration number ${carNumber}, parked.`;
        }
        
    }

    removeCar( carNumber ) {
        let car = this.vehicles.find((x) => x.carNumber == carNumber);
        if(car == undefined || car == null || car == false) {
            throw new Error("The car, you're looking for, is not found.");
        }

        if(car.payed == false) {
            throw new Error(`${carNumber} needs to pay before leaving the parking lot.`);
        }

        let index = this.vehicles.indexOf(car);
        this.vehicles.splice(index, 1);
        return `${carNumber} left the parking lot.`;
    }

    pay( carNumber ) {
        let car = this.vehicles.find((x) => x.carNumber == carNumber);
        if(car == undefined || car == null || car == false) {
            throw new Error(`${carNumber} is not in the parking lot.`);
        }

        if(car.payed == true) {
            throw new Error(`${carNumber}'s driver has already payed his ticket.`);
        }

        car.payed = true;
        return `${carNumber}'s driver successfully payed for his stay.`;
    }

    getStatistics(carNumber) {
        let output = '';
        if(carNumber == undefined || carNumber == null || carNumber == '') {
            output += `The Parking Lot has ${this.capacity - this.vehicles.length} empty spots left.`;

            for (const car of this.vehicles.sort((a,b) => a.carModel.localeCompare(b.carModel))) {
                output += `\n${car.carModel} == ${car.carNumber} - ${(car.payed ? 'Has payed ' : 'Not payed')}`;
            }

        } else {
            return `${car.carModel} == ${car.carNumber} - ${(car.payed ? 'Has payed ' : 'Not payed')}`;
        }

        return output;
    }
}

const parking = new Parking(12);

console.log(parking.addCar("Volvo t600", "TX3691CA"));
console.log(parking.addCar("Volvo t500", "TX3691CA"));
console.log(parking.addCar("Mazda RX-7", "TX3691CA"));
console.log(parking.addCar("Mazda RX-7", "TX3691CA"));
console.log(parking.getStatistics());

console.log(parking.pay("TX3691CA"));
console.log(parking.removeCar("TX3691CA"));
