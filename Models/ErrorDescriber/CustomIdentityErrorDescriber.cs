using Microsoft.AspNetCore.Identity;

namespace ogrencievin.Models.ErrorDescriber;

public class CustomIdentityErrorDescriber:IdentityErrorDescriber
{
    

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError{Code="PasswordTooShort",Description=$"Şifre Çok Kısa En Az {length} karakter içermeli"};
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError{Code="PasswordRequiresDigit",Description=$"Şifre Sayı İçermeli"};
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError{Code="PasswordRequiresLower",Description=$"Şifre Küçük Karakter İçermeli"};
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError{Code="PasswordRequiresUpper",Description=$"Şifre Büyük Karakter İçermeli"};
    }
    
}