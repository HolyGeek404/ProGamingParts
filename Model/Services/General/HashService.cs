using Model.Services.Interfaces;

namespace Model.Services.General;

public class HashService : IHashService
{
    private readonly Dictionary<char, string> _hashMap = new()
    {
         { 'a' , "xaJ:g" },
         { 'b' , "6F2%" },
         { 'c' , "G|E" },
         { 'd' , "jtQ" },
         { 'e' , "6p@" },
         { 'f' , "K/xjE" },
         { 'g' , ")86" },
         { 'h' , "tDx,U)" },
         { 'i' , ")x8UQ" },
         { 'j' , "j|qH:" },
         { 'k' , "Iq<g|" },
         { 'l' , "y[2" },
         { 'm' , "CZb" },
         { 'n' , "x#=" },
         { 'o' , "Wk8" },
         { 'p' , "W?|" },
         { 'q' , "-_Qi8" },
         { 'r' , "I|{OX<" },
         { 's' , "<8{YV)" },
         { 't' , "K#o+k" },
         { 'u' , "VaOY<9" },
         { 'v' , "g7$" },
         { 'w' , "eoe" },
         { 'x' , "i8p" },
         { 'y' , "G0M|" },
         { 'z' , "rT?S=]" },
         { '0' , "c>7" },
         { '1' , "qJYVYq" },
         { '2' , "YmX-" },
         { '3' , "4U@01]" },
         { '4' , "qbZS+" },
         { '5' , "KR_" },
         { '6' , "'iWN" },
         { '7' , "Q>wY" },
         { '8' , "][A" },
         { '9' , "5IbQY" },
         { '!' , "6/D" },
         { '@' , "d?=F" },
         { '#' , "NCk2" },
         { '$' , "v3-" },
         { '%' , "dW2l" },
         { '^' , "#2O5" },
         { '&' , "je=9X" },
         { '*' , "Kk.?" },
         { '(' , "o9H5M@" },
         { ')' , "i/_wV" },
         { '_' , "n+:a" },
         { '+' , "&+'W" },
         { '-' , ">)d<" },
         { '=' , "wD)" },
         { '[' , "7mF" },
         { ']' , "BUy:y" },
         { '{' , "A%9jc" },
         { '}' , "OZR" },
         { '|' , "li|" },
         { ';' , "4G.k6" },
         { ':' , "&(l-9C" },
         { '<' , "'%pC" },
         { '>' , "^i5" },
         { '/' , "=mf" },
         { '?' , "_K>%vM" }
    };

    public string Hash(string input)
    {
        return input.Aggregate("", (current, ch) => current + _hashMap[char.ToLower(ch)]);
    }

    public bool VerifyPassword(string passwordForm, string passwordHash)
    {
        var hashedPassword = Hash(passwordForm);
        return passwordHash.Equals(hashedPassword);
    }
}