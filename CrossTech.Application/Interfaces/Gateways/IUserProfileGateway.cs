﻿using CrossTech.Domain.Entities;

namespace CrossTech.Application.Interfaces.Gateways
{
    public interface IUserProfileGateway : IBasicGateway<UserProfile>
    {
        UserProfile FindByUserId(int userId);
    }
}
