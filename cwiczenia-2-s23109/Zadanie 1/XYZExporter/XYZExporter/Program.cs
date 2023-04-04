using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using XYZExporter.obj;
using static XYZExporter.Methods;
using Formatting = Newtonsoft.Json.Formatting;

namespace XYZExporter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // PARAMETRY PLIKU:
            // ADRES_CSV(input) ADRES_DOCELOWY FORMAT_DANYCH  
            //Obsługiwane formaty : JSON , XML 
            //Dane testowe zakomentowane , na podstawie ich przykładowe output-y
            if (args.Length != 3)
            {
                throw new Exception("Wrong amount of arguments");
                
            }
            var csv_filename = args[0] ;
            // csv_filename = "D:\\Programowanie\\Cs + .Net\\cwiczenia-2-s23109\\Zadanie 1\\dane.csv"
            var folder = args[1];
            //  folder = = "D:\\Programowanie\\Cs + .Net\\cwiczenia-2-s23109\\Zadanie 1";
            var format = args[2];
            format = folder.ToUpper();
            //format = "XML";
            
            Methods t = new Methods(csv_filename,folder);
            
            List<string> linie;
            try
            {
             linie = await t.ReadCSV();
            }
            catch (Exception e)
            {
                
                throw;
            }
            
            var students = new List<Student>(); //lista z duplikatami ? 
            
            

            foreach (var item in linie)
            {
                try
                {
                    var nowy = Nowy_student(item);
                    Boolean czy_juz_jest = false;
                    foreach (var w_bazie in students )
                    {
                        if (nowy.is_the_same(w_bazie))
                        {
                            czy_juz_jest = true;
                        }
                    }

                    if (!czy_juz_jest)
                    {
                        students.Add(Nowy_student(item));
                    }
                    else
                    {
                        addToLog($"Próba dodania duplikatu studenta : {nowy.Imie} {nowy.Nazwisko} {nowy.NrStudenta}");
                    }
                   
                }
                catch (Exception e)
                {
                    addToLog(e.Message);
                }
            }

            Uniwersytet uniwersytet = new Uniwersytet
            {
                Autor = "Dawid Kachniarz",
                Studenci = students,
                CreatedAt = DateTime.Now.ToString("dd/MM/yyyy"),
                ActiveStudiesList = t.create_activeStudies(students)
            };

            switch (format)
            {
                case "JSON":
                {
                    var json_serial = JsonConvert.SerializeObject(uniwersytet, Formatting.Indented);
                    File.WriteAllText(folder+"\\data.json" , json_serial);
                    break;
                }
                case "XML":
                {
                    var xml_serial = new XmlSerializer(typeof(Uniwersytet));
                    using (var writer = new StreamWriter(folder + "\\dane.xml"))
                    {
                        xml_serial.Serialize(writer,uniwersytet);
                    }
                    break;
                }
                default:
                {
                    addToLog("Unsupported output file type");
                    break;
                }
            }


        }
    }

   
}
