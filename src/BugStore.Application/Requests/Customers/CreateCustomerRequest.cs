using BugStore.Application.Responses.Customers;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace BugStore.Application.Requests.Customers
{
    public class CreateCustomerRequest : Notifiable<Notification>, IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "The name is required.")
                .IsEmail(Email, "Email", "The email is invalid.")
                .IsGreaterOrEqualsThan(BirthDate, new DateTime(1900, 1, 1), "BirthDate", "Invalid date of birth.")
            );
        }
    }
}
