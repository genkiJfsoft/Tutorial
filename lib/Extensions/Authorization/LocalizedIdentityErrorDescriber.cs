using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace ExpenseTracker.Extensions.Authorization;
public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    private readonly IStringLocalizer<Resources.Strings> _localizer;

    public LocalizedIdentityErrorDescriber(IStringLocalizer<Resources.Strings> localizer)
    {
        _localizer = localizer;
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = _localizer["PasswordMustBeAtLeastCharacters", length]
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = _localizer["PasswordRequiresNonAlphanumeric"]
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = _localizer["PasswordRequiresDigit"]
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = _localizer["PasswordRequiresLower"]
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = _localizer["PasswordRequiresUpper"]
        };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = nameof(DefaultError),
            Description = _localizer["An unknown error occurred."]
        };
    }
}
