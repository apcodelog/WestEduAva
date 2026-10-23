using System;
using Core.Interfaces;
using Core.Models.ConsoleHelpers;

namespace Core.Models.Users;

public abstract class UserBase : IDataObject
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public Address? Address { get; set; }

    public virtual void SetUserInfo()
    {
        FirstName = ConsoleInputHelper.GetString("Skriv förnamn");
        LastName = ConsoleInputHelper.GetString("Skriv efternamn");
        Phone = ConsoleInputHelper.GetString("Skriv telefonnr.");
        Email = ConsoleInputHelper.GetString("Skriv e-post");
        Address = ConsoleInputHelper.GetAddress();
    }

    public virtual List<string> GetInfoCells()
    {
        return [
            $"{FirstName} {LastName}",
            $"Telefonnr.: {Phone}",
            $"E-post: {Email}",
            $"Adress: {Address}"
        ];
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} Telefonnr.: {Phone} E-post: {Email} Adress: {Address}";
    }
}
