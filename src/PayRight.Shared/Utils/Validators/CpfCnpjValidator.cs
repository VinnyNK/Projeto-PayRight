namespace PayRight.Shared.Utils.Validators;

public class CpfCnpjValidator
{
    public static bool IsValid(string cpfCnpj) 
    { 
        if (string.IsNullOrEmpty(cpfCnpj))
            return false;
        return (IsCpf(cpfCnpj) || IsCnpj(cpfCnpj));
    }
    
    public static bool IsCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return false;
        
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");
        if (cpf.Length != 11 || !decimal.TryParse(cpf, out _))
            return false;

        for (var j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                return false;

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        var digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
     }

     public static bool IsCnpj(string cnpj)
     { 
         if (string.IsNullOrEmpty(cnpj))
             return false;
         
         var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
         var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

         cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
         if (cnpj.Length != 14 || !decimal.TryParse(cnpj, out _))
             return false;

         var tempCnpj = cnpj[..12];
         var soma = 0;

         for (var i = 0; i < 12; i++)
             soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

         var resto = (soma % 11);
         if (resto < 2)
             resto = 0;
         else
             resto = 11 - resto;

         var digito = resto.ToString();
         tempCnpj = tempCnpj + digito;
         soma = 0;
         for (var i = 0; i < 13; i++)
             soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

         resto = (soma % 11);
         if (resto < 2)
             resto = 0;
         else
             resto = 11 - resto;

         digito = digito + resto.ToString();

         return cnpj.EndsWith(digito);
     }
}