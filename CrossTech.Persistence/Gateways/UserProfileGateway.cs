using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using CrossTech.Domain.Enumerations;
using CrossTech.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище записей пользовательской информации
    /// </summary>
    public class UserProfileGateway : BasicGateway<UserProfile>, IUserProfileGateway
    {
        protected override IList<UserProfile> InitializeCollection()
        {
            return new List<UserProfile>
            {
                new UserProfile { Id = 1, UserId = 1, FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович", Phone = Phone.For("+7 (914) 577-12-30"), Birthdate = new DateTime(1994, 4, 17), Gender = ProfileGender.Male },
                new UserProfile { Id = 2, UserId = 2, FirstName = "Петр", LastName = "Петров", MiddleName = "Петрович", Phone = Phone.For("+7 (914) 577-12-30"), Birthdate = new DateTime(1981, 1, 21), Gender = ProfileGender.Female },
                new UserProfile { Id = 3, UserId = 3, FirstName = "Семен", LastName = "Семенов", MiddleName = "Семенович", Phone = Phone.For("+7 (914) 577-12-30"), Birthdate = new DateTime(1958, 7, 30), Gender = ProfileGender.Male }
            };
        }

        public UserProfile FindByUserId(int userId)
        {
            return Find(p => p.UserId == userId).First();
        }

        /// <summary>
        /// Заполняем ID новой сущности и помещаем её в список
        /// </summary>
        /// <param name="newProfile"></param>
        public override void Add(UserProfile newProfile)
        {
            UserProfile lastProfile = _entities.Last();
            newProfile.Id = lastProfile.Id + 1;
            base.Add(newProfile);
        }
    }
}
