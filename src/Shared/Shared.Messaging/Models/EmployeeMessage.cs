using System;

namespace Shared.Messaging.Models
{
    public class EmployeeMessage
    {
        public Guid EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Status { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public Guid? ManagerId { get; set; }
        public required string EventType { get; set; }
    }
}
