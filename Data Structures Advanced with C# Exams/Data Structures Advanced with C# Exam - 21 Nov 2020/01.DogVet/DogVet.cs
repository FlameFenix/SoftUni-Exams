namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById;
        private Dictionary<string, Owner> owners;
        private Dictionary<string, Dog> ownersWithDogs;

        public DogVet()
        {
            dogsById = new Dictionary<string, Dog>();
            owners = new Dictionary<string, Owner>();
        }

        public int Size => dogsById.Count;
        public void AddDog(Dog dog, Owner owner)
        {
            if (dogsById.ContainsKey(dog.Id) ||
                owner.DogsByName.ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }

            if (!owners.ContainsKey(owner.Id))
            {
                owners.Add(owner.Id, owner);
            }

            dogsById.Add(dog.Id, dog);

            dog.Owner = owner;
            owner.DogsByName.Add(dog.Name, dog);
        }

        public bool Contains(Dog dog)
        {
            if (dogsById.ContainsKey(dog.Id))
            {
                return true;
            }

            return false;
        }


        public Dog GetDog(string name, string ownerId)
        {
            if(!owners.ContainsKey(ownerId) || !owners[ownerId].DogsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var dog = owners[ownerId].DogsByName[name];

            return dog;
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            var dog = GetDog(name, ownerId);

            dogsById.Remove(dog.Id);
            owners[ownerId].DogsByName.Remove(name);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!owners.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return owners[ownerId].DogsByName.Values;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            var dogs = dogsById.Values.Where(x => x.Breed.Equals(breed));

            if(dogs.Count() == 0)
            {
                throw new ArgumentException();
            }

            return dogs;
        }

        public void Vaccinate(string name, string ownerId)
        {
            var dog = GetDog(name, ownerId);

            dog.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            var dog = GetDog(oldName, ownerId);

            dog.Name = newName;

            owners[ownerId].DogsByName.Remove(oldName);

            owners[ownerId].DogsByName.Add(newName, dog);

        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            var dogs = dogsById.Values.Where(x => x.Age == age);

            if(dogs.Count() == 0)
            {
                throw new ArgumentException();
            }

            return dogs;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        => dogsById.Values.Where(x => x.Age >= lo && x.Age <= hi);

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        => dogsById.Values.OrderBy(x => x.Age).ThenBy(x => x.Name).ThenBy(x => x.Owner.Name);

    }
}