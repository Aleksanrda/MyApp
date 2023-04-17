using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyApp.Models;

namespace MyApp.Data
{
    public class DataContext
    {
        public DataContext()
        {
            Users = new List<User>
            {
                new User
                {
                    Id = 1, Forename = "Grant", Surname = "Cooper", IsActive = true,
                    DateOfBirth = new DateTime(1990, 5, 12)
                },
                new User
                {
                    Id = 2, Forename = "Tom", Surname = "Gathercole", IsActive = true,
                    DateOfBirth = new DateTime(1999, 4, 24)
                },
                new User
                {
                    Id = 3, Forename = "Mark", Surname = "Edmondson", IsActive = true,
                    DateOfBirth = new DateTime(2000, 3, 31)
                },
                new User
                {
                    Id = 4, Forename = "Graham", Surname = "Clark", IsActive = true,
                    DateOfBirth = new DateTime(1995, 2, 1)
                },
                new User
                {
                    Id = 5, Forename = "Nick", Surname = "Lewis", IsActive = false,
                    DateOfBirth = new DateTime(1991, 2, 11)
                },
                new User
                {
                    Id = 6, Forename = "James", Surname = "Young", IsActive = false,
                    DateOfBirth = new DateTime(1993, 12, 9)
                },
            };
        }

        private List<User> Users { get; set; }


        public List<TEntity> Set<TEntity>() where TEntity : class
        {
            var propertyInfo = PropertyInfos.FirstOrDefault(p => p.PropertyType == typeof(List<TEntity>));


            if (propertyInfo != null)
            {
                // Get the List<T> from 'this' Context instance
                var x = propertyInfo.GetValue(this, null) as List<TEntity>;

                return x;
            }

            throw new Exception("Type collection not found");
        }


        private IEnumerable<PropertyInfo> _propertyInfos;

        private IEnumerable<PropertyInfo> PropertyInfos
        {
            get
            {
                return _propertyInfos ??
                       (_propertyInfos = GetType()
                           .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                           .Where(p => p.PropertyType.IsGenericType &&
                                       p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)));
            }
        }
    }
}