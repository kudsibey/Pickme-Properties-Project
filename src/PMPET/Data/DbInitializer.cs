using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMPET.Data;

namespace PMPET.Models
{
    public static class DbInitializer
    {
        public static void Initialize(PMPETContext context)
        {
            context.Database.EnsureCreated();

            //// Look for any students.
            //if (context.Persons.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var persons = new Person[]
            //{
            //new Person{FirstMidName="Carson",LastName="Alexander",JoinDate=DateTime.Parse("2005-09-01"),AddressLine1="17 Birdbrook Road",
            //    AddressLine2 ="Kidbrooke",Town="London",PostCode="SE39QA",MemberType="Buyer"},
            //new Person{ FirstMidName = "Ali", LastName = "Kali", JoinDate = DateTime.Parse("2005-09-01"), AddressLine1 = "17 Birdbrook Road",
            //    AddressLine2 = "Kidbrooke", Town = "London", PostCode = "SE39QA", MemberType = "Buyer" } };
            //foreach (Person p in persons)
            //{
            //    context.Persons.Add(p);
            //}
      

        }
    }
}
