const { assert } = require("chai");
let { Repository } = require("../solution.js");

describe("Testing functionality of repository …", function () {
    describe("testing constructor, propertiest and etc", function () {
        let properties = '';
        let repository = '';
        let entity = '';
        this.beforeEach(() => {
            properties = {
                name: "string",
                age: "number",
                birthday: "object"
            };

            repository = new Repository(properties);

            entity = {
                name: 'Gosho',
                age: 22,
                birthday: new Date(1998, 0, 7)
            };
        })
        it("should create new instance of repository", function () {

            assert.equal(properties, repository.props);
            assert.deepEqual(repository.data, new Map());
            assert.equal(repository.nextId(), 0);
            assert.equal(repository.count, 0);
        });

        it("should add new entity to the repository", function () {

            repository.add(entity);

            assert.equal(repository.count, 1);
            assert.deepEqual(repository.data.get(0), entity);
            assert.include(repository.data, entity);

        });

        it("should throw error while entity data is null", function () {
            entity = {
                age: null,
                birthday: null,
            };

            secondEntity = {
                name: 'Pesho',
                birthday: new Date(1998, 0, 7),
            };

            thirdEntity = {
                name: 'Pesho',
                age: 21,
            };

            assert.throw(() => repository.add(entity), `Property name is missing from the entity!`);
            assert.throw(() => repository.add(secondEntity), `Property age is missing from the entity!`);
            assert.throw(() => repository.add(thirdEntity), `Property birthday is missing from the entity!`);
        });

        it("should throw error while entity data is ", function () {
            entity = {
                name: null,
                age: null,
                birthday: null,
            };

            secondEntity = {
                name: 'Pesho',
                age: null,
                birthday: new Date(1998, 0, 7),
            };

            assert.throw(() => repository.add(entity), `Property name is not of correct type!`);
            assert.throw(() => repository.add(secondEntity), `Property age is not of correct type!`);
        });

        it("should get entity from the repository by id", function () {

            repository.add(entity);

            assert.equal(repository.getId(0), entity);
        });

        it("should throw error for missing entity from the repository by id", function () {

            assert.throw(() => repository.getId(0), `Entity with id: 0 does not exist!`);
        });

        it("should throw error for missing entity from the repository by id when update", function () {

            assert.throw(() => repository.update(0, entity), `Entity with id: 0 does not exist!`);
        });

        it("should return correct value for updating entity with the given id", function () {

            newEntity = {
                name: 'Pesho',
                age: 21,
                birthday: new Date(1938, 0, 7)
            };

            repository.add(entity);
            repository.update(0, newEntity)
            assert.deepEqual(repository.getId(0), newEntity);
        });

        it("should return correct value for deleting entity with the given id", function () {

            newEntity = {
                name: 'Pesho',
                age: 21,
                birthday: new Date(1938, 0, 7)
            };

            repository.add(entity);
            repository.add(newEntity)
            assert.equal(repository.count, 2);
            repository.del(1);
            assert.equal(repository.count, 1);
        });

        it("should throw error while deleting entity with the given id", function () {
            
            newEntity = {
                name: 'Pesho',
                age: 21,
                birthday: new Date(1938, 0, 7)
            };

            repository.add(entity);
            repository.add(newEntity);
            assert.throw(() => repository.del(3), `Entity with id: 3 does not exist!`);
        });
    });
});
