// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

bool ISVowel(char alpha)
{
    if(alpha == 'a' ||  alpha == 'e' ||  alpha == 'i' ||  alpha == 'o' ||  alpha == 'u')
    {
        return true; 
    }

    return false;
}


String str = "MeghaNisha";
//String dup = new String(str.Distinct().ToArray());
//Console.WriteLine(dup);


//String up = new string(str.ToUpper());
//Console.WriteLine(up);
//String down = new string(str.ToLower());
//Console.WriteLine(down);

//char ch = 'a';
//Console.WriteLine(Char.ToUpper(ch));


String rev = new String(str.Reverse().ToArray());



Console.WriteLine(rev);

string res = "";

for (int i = 0; i<rev.Length; i++)
{
    if (ISVowel(rev[i]))
    {
        res+= (char)(rev[i]+1);
    }
    else
    {
        res += rev[i];
    }
}
int k = 2;


int len = res.Length;

string final = res.Substring(len - k) + res.Substring(0, len - k); 

Console.WriteLine(final);
