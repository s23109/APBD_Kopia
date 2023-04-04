using LinqTutorials.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTutorials
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {   
            
            return 
            from emp in Emps 
            where emp.Job == "Backend programmer" 
            select emp;
            
            /*
            Albo przez lambde
            w sql-owej nie zrobi się wszystkiego, dlatego lepiej używać tego z lambdami 
            
            return Emps.Where(item => item.Job == "Backend programmer" ).OrderBy(item => item.Ename);
            
            */

            // z obiektów emp w emps
            // where
            // zwróć emp-y 

        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {         
            return Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
            .OrderByDescending(emp => emp.Ename);
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            return Emps.Max(emp => emp.Salary);
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            return Emps.Where(emp => emp.Salary == Emps.Max(e => e.Salary));
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {        
            //obiekt anonimowy , aliasy takie jak jak w emp   
            //w ramach selecta tworzy się nowy obiekt , potem zmienia obiekt z kolekcji w inną kolekcję

            return Emps.Select(emp => new {
                Nazwisko = emp.Ename,
                Praca = emp.Job
            });
            // {Nazwisko = "Nazwa" , Praca = "Praca"}
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public static IEnumerable<object> Task6()
        {         // kolejność kluczy ścisła
            return Emps.Join(Depts , emp => emp.Deptno , dept => dept.Deptno , (emp , dept) => new {
                emp.Ename,
                emp.Job,
                dept.Dname
                } );
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {       
            //bo kluczem jest praca w group by     
            return Emps.GroupBy(e => e.Job)
            .Select(e => new {
                Praca= e.Key,
                LiczbaPracownikow = e.Count()
            });
        }

        /// <summary>
        ///     Zwróć wartość "true" jeśli choć jeden
        ///     z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            // any - sprawdza czy kolekcja zawiera jakikolwiek rekord
            return Emps.Where(e => e.Job == "Backend programmer").Any();
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            return Emps.Where(e => e.Job == "Frontend programmer").OrderByDescending(e => e.HireDate).ToList()[0];
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {          
            return Emps.Select( e => new {
                e.Ename,
                e.Job,
                e.HireDate
            }).Union( new List<object> {
                new {
                    Ename = "Brak wartości",
                    Job = (string) null,
                    Hiredate = (DateTime?) null
                }
            });
            // typ object bo łączymy to z listą obiektów
            // kompilator nie wie jakiego typu jest null , więc trzeba castować
            // datetime to typ złożony , więc musi być nullowalny ( ? ) 
        }

        /// <summary>
        /// Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
        /// 1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
        /// 2. Chcemy zwrócić listę obiektów o następującej srukturze:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Wykorzystaj typy anonimowe
        /// </summary>

        // group by i where

        /*
         * Select Dname , Count(Ename) from Depts
         * Join Emp on ...
         * group by dname
         * where count(ename)>1
         * 
         */
        public static IEnumerable<object> Task11()
        {
            return Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new {
               dept.Dname,
               emp.Ename
            }).GroupBy(l => l.Dname)
            .Select(l => new {
                name = l.Key,
                numOfEmployees = l.Count()
            })
            .Where(e => e.numOfEmployees>1)
           ;
        }

        /// <summary>
        /// Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
        /// Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
        /// 
        /// Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
        /// Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
        /// </summary>

        // customowa metoda którą jesteśmy w stanie dodać do takiej klasy ( na dole jest jest deklaracja )
        //

        public static IEnumerable<Emp> Task12()
        {    //trzeba zrobić to GetEmpsWithSubordinates
            return Emps.GetEmpsWithSubordinates();
        }

        /// <summary>
        /// Poniższa metoda powinna zwracać pojedyczną liczbę int.
        /// Na wejściu przyjmujemy listę liczb całkowitych.
        /// Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
        /// Zakładamy, że zawsze będzie jedna taka liczba.
        /// Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>

        // tutaj praca nie na bazie, a parametrze w zadaniu

        // select ELEMENT , Count(ELEMENT) from arr
        // group by ELEMENT
        // where Count(ELEMENT)%2==1

        //typ zwrotu INT
        public static int Task13(int[] arr)
        {
            var  i =  from num in arr
                   group num by num into g
                   where g.Count() %2 == 1
                   select g.Key; 

            return i.ElementAt(0);
        }

        /// <summary>
        /// Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
        /// Posortuj rezultat po nazwie departament rosnąco.
        /// </summary>
        /// 

        /*
         * select * from Dept
         * where deptno in (
         * -- dla tych z 5 indeksami
         * select deptno , count(deptno) from emp 
         * group by deptno
         * where count(deptno) == 5 
         * )
         * UNION
         *  select * from Dept
         * where deptno not in (
         * -- dla tych bez indeksów
         * select distinct deptno from emp 
         * )
         * 
         */
        public static IEnumerable<Dept> Task14()
        {
            //działa dla 3, 
            var DeptWith5 = Emps.GroupBy(l => l.Deptno)
                .Select( l => new {l.Key , NumOfEmployees = l.Count()})
                .Where(l => l.NumOfEmployees == 5).Select(l => l.Key);

            var DeptWithNone = Depts.Where(x => !(Emps.Select(l => l.Deptno).Distinct()).Contains(x.Deptno)).Select(l => l.Deptno);

            return Depts.Where(x => (DeptWith5.Contains(x.Deptno)) || (DeptWithNone.Contains(x.Deptno)));
        }
    }

    public static class CustomExtensionMethods
    {
        //Put your extension methods here
        public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
        {
            /// <summary>
            /// Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
            /// Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
            /// 
            /// Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
            /// Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
            /// </summary>
            // wpisz wszystkich poza tymi , który nie są wpisani jako mgr nigdzie (nie jest niczyim mgr -> nie ma podwładnych)
            // skorelowane - wypisz te id-ki, które nie są nullami (nie jest nullem -> jest czyimś nadwładnym -> czyli on sam ma podwładnych

            return emps.Intersect(emps.Where(e => e.Mgr == e.Mgr).Select(e => e.Mgr));
        }

    }
}
