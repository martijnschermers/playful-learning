using System.Security.Claims;
using Core.Domain;
using Core.DomainServices.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace Portal.Models;

public static class IdentitySeedData
{
    private const string OrganizerUser = "martijn@gmail.com";
    private const string OrganizerPassword = "Martijn123!";

    private const string ParticipantUser = "kees@gmail.com";
    private const string ParticipantPassword = "Kees123!";

    public static async Task SeedUsers(UserManager<IdentityUser> userManager, IUserRepository repository)
    {
        var organizer = await userManager.FindByEmailAsync(OrganizerUser);
        if (organizer == null) {
            var user = new User
            {
                Name = "Martijn",
                Address = new Address { City = "Dussen", Street = "Bredeweer", HouseNumber = 10 },
                Type = UserType.Organizer, Email = OrganizerUser, Gender = Gender.Male,
                BirthDate = new DateTime(2004, 03, 04)
            };

            repository.AddUser(user);

            organizer = new IdentityUser(OrganizerUser)
            {
                Email = OrganizerUser
            };
            await userManager.CreateAsync(organizer, OrganizerPassword);
            await userManager.AddClaimAsync(organizer, new Claim("UserType", UserType.Organizer.ToString()));
        }

        var participant = await userManager.FindByEmailAsync(ParticipantUser);
        if (participant == null) {
            var user = new User
            {
                Name = "Kees",
                Address = new Address { City = "Breda", Street = "Lovensdijkstraat", HouseNumber = 61 },
                Type = UserType.Participant, Email = ParticipantUser, Gender = Gender.Male,
                BirthDate = new DateTime(1990, 08, 21)
            };

            repository.AddUser(user);

            participant = new IdentityUser(ParticipantUser)
            {
                Email = ParticipantUser
            };
            await userManager.CreateAsync(participant, ParticipantPassword);
            await userManager.AddClaimAsync(participant, new Claim("UserType", UserType.Participant.ToString()));
        }
    }
}