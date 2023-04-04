using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cwiczenia_1_s23109
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            /*     Console.WriteLine("Hello World!");
             int? moj_int = 3;
             moj_int = null;
             //int moj_int2 = null;
     
             string zmienna = "string";
             void Moja_fun (int liczba){
                 Console.WriteLine(liczba);
             }
             var lista = new List<string>();
             var set = new Hashset<string>();
     
             class Tree {
                 public int MyProperty { get; set; }
     
     
                 public Tree (int myprop){
                     this.MyProperty = myprop;
                 }
     
             }
     
     
             var Tree2 = new Tree(3) {MyProperty = 10};
             */
           // Poprzednia wersja
       // try 
       // {
       //     var httpClient = new HttpClient();
       //     var response = await httpClient.GetAsync(args[0]);
       //     Console.WriteLine("Checking : " + args[0] );
       //     var maile = new HashSet<String>();
       //     var content = await response.Content.ReadAsStringAsync();
       //     var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.+]+");
       //     
       //     // Wypisanie danych strony (kodowanie itp)
       //     Console.WriteLine("Site info:\n" + response);
       //     
       //     foreach (Match match in regex.Matches(content))
       //     {
       //         if (!maile.Contains(match.ToString()))
       //         {
       //             //jeśli jeszcze nie dodano takiego maila, to dodaj
       //             maile.Add(match.ToString());
       //         }
       //     }
       //     
       //     Console.WriteLine("Maile:");
       //     foreach (var mail in maile)
       //     {
       //         Console.WriteLine(mail.ToString() + "\n");
       //     }
       //
       // }
       // catch (Exception e)
       // {
       //     //Jakaś obsługa błędów
       //     throw new Exception("Wystąpił błąd przy przetwarzaniu, aborting");
       // }

       if (args.Length != 1)
       {
           throw new ArgumentException("Podana zła ilość argumentów");
       }

       var urlStrony = args[0];
       var urlRegex = new Regex(@"^(http|http(s)?://)?([\w-]+.)+[\w-]+[.com|.in|.org]+([?%&=]*)?");
       var urlMatch = urlRegex.Matches(urlStrony);

       if (urlMatch.Count != 1)
       {
           throw new ArgumentException("Nie poprawny format argumentu wejścia");
       }
       
       var httpClient = new HttpClient();

       try
       {
           var response = await httpClient.GetAsync(urlStrony);
           var status_code = response.StatusCode;
           
            //jak co innego jak ok , to wywal błąd
           if (status_code != HttpStatusCode.OK)
           {
               
               throw new Exception("Zły kod statusu strony");
           }
           
           var siteContent = await response.Content.ReadAsStreamAsync();
           var emailRegex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
           var maileUnprocessed = emailRegex.Matches(siteContent.ToString()); //bo błąd ?
           
           //hashset - nie pozwala na duplikaty
           HashSet<String> maileProcessed = new HashSet<string>();

           if (maileUnprocessed.Count == 0)
           {
               throw new Exception("Brak maili na stronie");
           }

           foreach (var item in maileUnprocessed)
           {
               maileProcessed.Add(item as string);
           }

           foreach (var item in maileProcessed)
           {
               Console.WriteLine(item);
           }

       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           throw;
       }
       finally
       {
           httpClient.Dispose();
       }
       
        }


        
    }
}
