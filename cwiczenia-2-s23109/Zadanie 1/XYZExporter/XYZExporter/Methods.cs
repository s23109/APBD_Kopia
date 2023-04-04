using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using XYZExporter.obj;

namespace XYZExporter
{
    public class Methods
    {
        string file_path_in;
        static string file_path_out;
        public Methods(string filePathIn , string filePathOut)
        {
            file_path_in = filePathIn;
            file_path_out = filePathOut;
        }
        public async Task<List<string>> ReadCSV()
        {
            try
            {
                if (!File.Exists(file_path_in)) throw new Exception($"Plik {file_path_in} nie istnieje");
                var linie = await File.ReadAllLinesAsync(file_path_in);
                return new List<string>(linie);
            }
            catch (FileNotFoundException e)
            {
                addToLog(e.Message);
                throw;
            }
            catch (Exception e)
            {
                addToLog("Błąd podczas odczytu");
                throw;
            }
            
        }
        public static Student Nowy_student(string linia)
       {
           var dane = linia.Split(",");

           if (dane.Length != 9)
           {
               throw new Exception($"Zła ilość danych w wierszu {linia}");
           }

           foreach (var item in dane)
           {
               if (item.Length < 1)
               {
                   throw new Exception($"Puste pola w wierszu {linia}");
               }
           }

           return new Student
           {
               Imie = dane[0],
               Nazwisko = dane[1],
           //    Kierunek = dane[2],
          //     TrybStudiow = dane[3],
                Studies = new Studies {Nazwa = dane[2] , Tryb = dane[3]},
               Email = dane[6],
               NrStudenta = dane[4],
               DataUrodzenia = dane[5],
               ImieMatki = dane[7],
               ImieOjca = dane[8]
               
           };


       }
        public Task dummy_test()
        {
            addToLog("test");
            return null;
        }

        public static void addToLog(string msg)
        {
            var data = DateTime.Now.ToString("dd:MM:yyyy-HH:mm:ss:fffffff");
            using StreamWriter stream = new StreamWriter(file_path_out + @"\log.txt" , true );
            stream.WriteLine(data + " " + msg);
        }

        public List<ActiveStudies> create_activeStudies(List<Student> students)
        {
            var temp = new Dictionary<string , ActiveStudies>();

            foreach (var item in students)
            {
                if (temp.ContainsKey(item.Studies.Nazwa))
                {
                    //powiększ
                    temp[item.Studies.Nazwa].ilosc++;
                }
                else
                {
                    var nowy = new ActiveStudies {nazwa = item.Studies.Nazwa, ilosc = 1};
                    temp.Add(item.Studies.Nazwa,nowy);

                }
            }


            return temp.Values.ToList();
        }
        
        
    }
}