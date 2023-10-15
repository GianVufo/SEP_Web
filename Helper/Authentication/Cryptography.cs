namespace SEP_Web.Helper.Authentication;
public static class Cryptography
{
    // A Classe Cryptography utiliza um algoritmo baseado em hash da biblioteca Bcrypt para criptografar senhas de forma segura gerando hashes únicas e saltos para a validação futura de uma senha que esteja criptografada pela classe;
    
    public static string EncryptPassword(string pass)
    {
        // EncryptPassword recebe por parâmetro uma string que será a senha que deve ser transformada em hash;

        return BCrypt.Net.BCrypt.HashPassword(pass); // HashPassword utiliza o esquema OpenBSD BCrypt e geração de salto utilizando o método BCrypt.Net.BCrypt.GenerateSalt() para gerar o hash da senha; 
    }

    public static bool VerifyPasswordEncrypted(string password, string hashedPassword)
    {
        // VerifyPasswordEncrypted recebe por parâmetro duas strings que correspondem respectivamente a uma senha dada como entrada por uma entidade, e um hash recuperado para compará-las;
        
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword); // O método Verify retorna true ou false ao fazer uma comparação dada com base no match da senha informada e o "salt" gerado pela hash recuperada;
    }
}