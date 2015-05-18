using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Checkpoint01.Classes;
using Checkpoint01.Interfaces;
using Checkpoint01.Types;

namespace Checkpoint01
{


    internal class Program
    {
        // Точность вычислений при сравнении
        public const double Precision = 0.0001;
        // Максимальное кол-во конфет в наборе
        public const int MaxCandyAmountInSet = 20;
        // Максимальный вес набора конфет
        public const int MaxCandySetWeight = 100;
        // Кол-во наборов
        public const int CandySetAmount = 200;
        // Вес подарка
        public const int XmasGiftWeight = 1000;

        public static string[] CandyData = { "Конфеты", "candy" };
        public static string[] CandySetData = { "Наборы", "candyset" };

        public static string AppPath;
        public static string[] CandyNames = {"Грильяж", "Балтика","Белочка","Вечерний звон","Золотая нива","Кара-Кум","Красная","Маска","Ну-ка, отними!","Петушок – золотой гребешок", "Гулливер","Красная шапочка","Мишка косолапый","Мишка на Севере","Садко","Шоколадный крем","Буревестник","Вихрь", "Клубничные", "Ласточка", "Пилот",
                                               "Радий","Ромашка","Цитрон","Южные орехи","Южная ночь","Сливовые листья","Смородинка","Цитрусовые","Басни Крылова","Космические","Красная Москва","Трюфель","Трюфель Экстра", "Птичье молоко","Стратосфера","Суфле","Юбилейные"};

        // рассматриваем Новогодний подарок как совокупность наборов конфет CandySet. Т.е. даже если 1 конфета - то мы ее помещаем в набор с кол-ом 1.
        static void Main(string[] args)
        {
            AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            /*
            // создаем конфеты и наборы
             CreateCandy(false);
             CreateCandySets(true);
             return;
            // */
            ArrayList candyList = GetCandyList(); // получаем список конфет
            ArrayList candySetList = FillCandySet(true); // набор наборов конфет :)

            // Найдем просроченные наборы, т.е. у кого срок хранения вышел на текущую дату
            Console.WriteLine("\nПросроченные наборы");
            foreach (var c in candySetList.ToArray().Where(x => (x as CandySet).ExpirationDate < DateTime.Now))
            {
                Console.WriteLine("Набор:{0} срок годности истек: {1}",(c as CandySet).CandySetName, (c as CandySet).ExpirationDate);
                var res = (from x in (c as CandySet)
                           where (x.Candy.ExpirationDate < DateTime.Now)
                          select (x as CandyForSet).Candy);
                if (res.Any())
                {
                    Console.WriteLine("Во всем виноваты:");
                    foreach (var cc in res)
                    {
                        Console.WriteLine("Конфетка:{0} со сроком:{1}", cc.CandyName, cc.ExpirationDate);
                    }
                }
            }

            XmasGift XmasGift = new XmasGift();
            Random r = new Random();
            // все наборы с хорошим сроком годности и в которых больше 1 конфеты
            var validCandySets = (from x in candySetList.ToArray()
                where ((x as CandySet).ExpirationDate > DateTime.Now && (x as CandySet).Count() > 1)
                select (x as CandySet));
            var candySets = validCandySets as CandySet[] ?? validCandySets.ToArray();
            int minCandySetWeight = candySets.Min(x => (int)x.Weight);
            int maxCandySetWeight = candySets.Max(x => (int)x.Weight);
            // наполняем подарок, пока не оставим место для одного набора
            while (XmasGiftWeight - XmasGift.Weigh > maxCandySetWeight)
            {
                XmasGift.Add(candySets.ToArray()[r.Next(candySets.Count())]);
            }
            // нужно добавить один набор, тогда у нас будет подарок приближен к Макс весу подарка
            //XmasGift.Add(candySets.ToArray()[r.Next(candySets.Count())]);
            // если осталось место для еще одного набора, то дополним его
            if (XmasGiftWeight - XmasGift.Weigh > minCandySetWeight)
            {
                int difference = XmasGiftWeight - XmasGift.Weigh;
                int minimalValue = difference;
                CandySet cs = null;
                foreach (var c in candySets)
                {
                   if  ((difference - c.Weight > 0) && (difference - c.Weight < minimalValue))
                   {
                       cs = c;
                       minimalValue = difference - (int)c.Weight;
                   }
                }
                // если нашли наиболее близкий набор для наполнения
                if (cs != null)
                {
                    XmasGift.Add(cs);
                }
            }
            // нужно подарок добить конфетами до XmasGiftWeight (1000)
            Console.WriteLine("\nНабор без сортировки");

            foreach (var cs in XmasGift)
            {
                Console.WriteLine("Набор: {0} sugar={1} вec={2}", (cs as CandySet).CandySetName, (cs as CandySet).Sugar, (cs as CandySet).Weight);
            }
            XmasGift.SortBySugar();
            Console.WriteLine("\nНаборы после сортировки сахару");
            foreach (var cs in XmasGift)
            {
                Console.WriteLine("Набор: {0} sugar={1} вec={2}", (cs as CandySet).CandySetName, (cs as CandySet).Sugar, (cs as CandySet).Weight);
            }

            Console.WriteLine("Самые сладкие конфеты:");
            foreach (var c in candyList.ToArray().Where(x => (x as Candy).Sugar == candyList.ToArray().Max(y=>(y as Candy).Sugar)))
            {
                Console.WriteLine("Конфета: {0} sugar={1} вec={2}", (c as Candy).CandyName, (c as Candy).Sugar, (c as Candy).Weight);
            }

            // вывести все конфеты в наборе
            List<CandyForSet> candyForSetList = XmasGift.Cast<CandySet>().SelectMany(c => c).ToList();
            List<CandyForSet> candyForSetSummuryList = new List<CandyForSet>();
            foreach (var cs in candyForSetList)
            {
                if (candyForSetSummuryList.Where(x => x.Candy.CandyName == cs.Candy.CandyName).Any())
                {
                    // если такая конфета есть - складываем с существующей
                    candyForSetSummuryList.Single(x => x.Candy.CandyName == cs.Candy.CandyName).Amount += cs.Amount;
                }
                else
                { 
                    // добавляем в набор
                    candyForSetSummuryList.Add(cs);    
                }
            }
            candyForSetSummuryList.Sort(new CandyForSetComparerByCandyName()); // сортируем по имени конфет
            Console.WriteLine("\nВ подарке имеются следующие конфеты:");
            foreach (var c in candyForSetSummuryList)
            {
                Console.WriteLine("Конфета: {0},  кол-во: {1}", c.Candy.CandyName, c.Amount);   
            }
            Console.WriteLine("\nПодарок весит:{0}", XmasGift.Weigh);

            Console.ReadKey();
        }

        public static ArrayList GetCandyList()
        {
            var result = new ArrayList();
            for (int i = 0; i < CandyNames.Count(); i++)
            {
                Candy candy = Candy.LoadFromFile(CandyNames[i]);
                result.Add(candy); // сохраняем все конфеты в списке, чтобы потом было удобнее работать
            }
            return result;
        }

        public static ArrayList FillCandySet(bool showLog = false)
        {
            ArrayList resultSet = new ArrayList();

            for (int i = 0; i < CandySetAmount; i++)
            {
                try
                {
                    CandySet candySet = CandySet.LoadFromFile(string.Format("{0}{1:D3}", CandySetData[0], i));
                    candySet.CandySetName = string.Format("{0}{1:D3}", CandySetData[0], i);

                    resultSet.Add(candySet);
                    if (showLog)
                    {
                        Console.WriteLine("{0}{1:D3} состоит:", CandySetData[0], i);
                        foreach (var c in candySet)
                        {
                            Console.WriteLine("Конфета: {0}  количество:{1}", ((CandyForSet)c).Candy.CandyName, ((CandyForSet)c).Amount);
                        }
                        Console.WriteLine("Вес набора:{0}", candySet.Weight);
                        Console.WriteLine("Энергетическая ценность:{0}", candySet.FoodValue);
                        Console.WriteLine("Годен до:{0}", candySet.ExpirationDate);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось загрузить: {0}{1:D3}", CandySetData[0], i);
                    Console.WriteLine("Ошибка: {0}", e.Message);
                }
            }

            return resultSet;
        }

        public static List<CandySet> FillCandySetByCandy()
        {
            return null;
        }

        public static void CreateCandy(bool overrideCandy = false)
        {  // Создаем конфеты
            foreach (var c in CandyNames)
            {
                if ((overrideCandy) || (!File.Exists(Path.Combine(AppPath, CandyData[0], Path.ChangeExtension(c, CandyData[1])))))
                    Candy.SaveToFile(c);  // создаем конфеты, если их нет (по названию файла)
            }            
        }

        public static void CreateCandySets(bool overrideCandySet = false) // создаем наборы конфет
        {
            // создаем CandySetAmount(200) наборов конфет.
            // т.е. в наборе конфета1-количество, ..., конфетаN-количество
            // считаем, что кол-во конфет в наборе не должно превышать MaxCandyAmountInSet(20) и вес 100г.
            Random r = new Random();
            ArrayList candyList = GetCandyList();
            for (int i = 0; i < CandySetAmount; i++)
            {
                // делаем первых CandyName.Count наборов c 1 конфетой, чтобы потом можно было докладывать конфетами до определенного веса
                CandySet CandySet = new CandySet(string.Format("{0}{1:D3}", CandySetData[0], i));

                if (i < CandyNames.Count())
                {
                    CandyForSet candyForSet = new CandyForSet() { Amount = 1, Candy = (Candy)candyList[i] };
                    CandySet.Add(candyForSet);
                }
                else
                {
                    // находим минимальный вес конфеты
                    //double minCandyWeight = 1000;
                    //foreach (var c in candyList)
                    //{
                    //    minCandyWeight = ((c as Candy).Weight < minCandyWeight && (c as Candy).Weight > 0) ? (c as Candy).Weight : minCandyWeight;
                    //}
                    //Console.WriteLine(minCandyWeight);
                    //Console.WriteLine(candyList.ToArray().Min(x => (x as Candy).Weight));
                    //Console.WriteLine(candyList.Cast<object>().Aggregate<object, double>(1000, (current, c) => ((c as Candy).Weight < current && (c as Candy).Weight > 0) ? (c as Candy).Weight : current));
                    //double minCandyWeight = candyList.Cast<object>().Aggregate<object, double>(1000, (current, c) => ((c as Candy).Weight < current && (c as Candy).Weight > 0) ? (c as Candy).Weight : current);
                    double minCandyWeight = candyList.ToArray().Min(x => (x as Candy).Weight);

                    //max weight 100 - набор в MaxCandySetWeight (100)г
                    int restWeight = MaxCandySetWeight;
                    Candy candy;
                    do
                    {
                        do
                        {
                            int candyIndex = r.Next(CandyNames.Count() - 1);
                            candy = (Candy)candyList[candyIndex]; //Candy.LoadFromFile(CandyNames[candyIndex]);
                        } while ((candy.Weight > restWeight) || CandySet.Any(n => n.Candy.CandyName == candy.CandyName)); // конфеты в наборе не должны повторяться

                        int candyMaxAmount = (restWeight / (int)Math.Round(candy.Weight) > MaxCandyAmountInSet) ? MaxCandyAmountInSet : restWeight / (int)Math.Round(candy.Weight); // не больше MaxCandyAmountInSet (20) одинаковых
                        int candyAmount = r.Next(1, candyMaxAmount);
                        CandyForSet candyForSet = new CandyForSet() { Amount = candyAmount, Candy = candy };
                        CandySet.Add(candyForSet);
                        restWeight -= candyAmount * (int)candy.Weight;
                    } while (restWeight > minCandyWeight);
                }
                // в XML не сохраняет
                CandySet.CandySetName = string.Format("{0}{1:D3}", CandySetData[0], i);
                CandySet.SaveToFile();
                //Serializer.SaveListToBinnary(Path.Combine(AppPath, CandySetData[0], Path.ChangeExtension(string.Format("{0}{1:D3}", CandySetData[0], i), CandySetData[1])), CandySet);
            }  
        }
    }
}
