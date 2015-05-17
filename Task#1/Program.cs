using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
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

        public static string[] CandyData = { "Конфеты", "candy" };
        public static string[] CandySetData = { "Наборы", "candyset" };

        public static string AppPath;
        public static string[] CandyNames = {"Грильяж", "Балтика","Белочка","Вечерний звон","Золотая нива","Кара-Кум","Красная","Маска","Ну-ка, отними!","Петушок – золотой гребешок", "Гулливер","Красная шапочка","Мишка косолапый","Мишка на Севере","Садко","Шоколадный крем","Буревестник","Вихрь", "Клубничные", "Ласточка", "Пилот",
                                               "Радий","Ромашка","Цитрон","Южные орехи","Южная ночь","Сливовые листья","Смородинка","Цитрусовые","Басни Крылова","Космические","Красная Москва","Трюфель","Трюфель Экстра", "Птичье молоко","Стратосфера","Суфле","Юбилейные"};

        // рассматриваем Новогодний подарок как совокупность наборов конфет CandySet. Т.е. даже если 1 конфета - то мы ее помещаем в набор с кол-ом 1.
        static void Main(string[] args)
        {
            AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            // Создаем конфеты
            foreach (var c in CandyNames)
            {
                if (!File.Exists(Path.Combine(AppPath, CandyData[0], Path.ChangeExtension(c, CandyData[1]))))
                    Candy.SaveToFile(c);  
            }
            
            Random r = new Random();
            // делаем 200 наборов конфет.
            // т.е. в наборе конфета1-количество, ..., конфетаN-количество
            // считаем, что кол-во конфет в наборе не должно превышать 20 и вес 100г.
            ArrayList candyList = new ArrayList();
            for (int i = 0; i < 200; i++)
            {
                // делаем первых CandyName.Count наборов c 1 конфетой
                List<CandyForSet> CandySetList =  new List<CandyForSet>(); 
                
                if (i < CandyNames.Count())
                {
                    Candy candy = Candy.LoadFromFile(CandyNames[i]);
                    candyList.Add(candy); // сохраняем все конфеты в списке, чтобы потом было удобнее работать
                    CandyForSet candyForSet = new CandyForSet(){Amount = 1, Candy = candy};
                    CandySetList.Add(candyForSet);
                }
                else
                {
                    
                    double minCandyWeight = 1000;
                    foreach (var c in candyList)
                    {
                        minCandyWeight = ((c as Candy).Weight < minCandyWeight && (c as Candy).Weight > 0)
                            ? (c as Candy).Weight
                            : minCandyWeight;
                    }
                    Console.WriteLine(minCandyWeight);
                    Console.WriteLine(candyList.ToArray().Min(x => (x as Candy).Weight));
                    Console.WriteLine(candyList.Cast<object>().Aggregate<object, double>(1000, (current, c) => ((c as Candy).Weight < current && (c as Candy).Weight > 0) ? (c as Candy).Weight : current));
                    //double minCandyWeight = candyList.Cast<object>().Aggregate<object, double>(1000, (current, c) => ((c as Candy).Weight < current && (c as Candy).Weight > 0) ? (c as Candy).Weight : current);

                    int candyIndex = r.Next(CandyNames.Count() - 1);
                    Candy candy = (Candy)candyList[candyIndex]; //Candy.LoadFromFile(CandyNames[candyIndex]);
                    //max weight 1000
                    int lostWight = 1000;
                    int candyMaxAmount = lostWight / (int)Math.Round(candy.Weight);
                     
                    break;
                }
                // в XML не сохраняет
                Serializer.SaveListToBinnary(Path.Combine(AppPath, CandySetData[0], Path.ChangeExtension(string.Format("{0}{1:D3}", CandySetData[0], i), CandySetData[1])), CandySetList);
            }
            List<CandySet> CandySet = new List<CandySet>();

            CandySet = FillCandySet();

           


            XmasGift XmasGift = new XmasGift();
            //XmasGift.Add(new CandySet());
        }

        static public List<CandySet> FillCandySet()
        {
            #region create CandySet
            //CandySetBuilder candySetBuilder = new CandySetBuilder();
            //CandyForSet candyForSet = Serializer.LoadFromXml<CandyForSet>("CandySet1.xml");
            //candySetBuilder.CandySet.Add(candyForSet);
            #endregion

            List<CandySet> resultSet = new List<CandySet>();
            //resultSet.Add();
            return resultSet;
        }

        static public List<CandySet> FillCandySetByCandy()
        {
            return null;
        }

        
    }
}
