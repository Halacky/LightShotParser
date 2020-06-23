using AngleSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace LightShotProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите слово, длиной 6 символов. Допустимы символы: Латинские буквы и цифры");
                GetId.StartId = Console.ReadLine();
                Console.WriteLine("Сколько уникальных индетификаторов просмотреть? ");
                GetId.Count = int.Parse(Console.ReadLine());
                some(GetId.StartId);
            }
            catch(ArgumentException a)
            {
                Console.WriteLine(a.Message);
            }
            
        }
        static void SaveFiles(string imageUrl, string item, DirectoryInfo dirInfo, string path)
        {
            using (var client = new WebClient())
            {
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                client.DownloadFile(imageUrl, @$"{dirInfo}\{item}.png");
            }
        }
        static void some(string id)
        {
            GetId getId = new GetId();
            string mainPartOfUrl = "https://prnt.sc/ ";
            string addressGroup = mainPartOfUrl + id;
            var listOfId = getId.MakeId();
            int fell = 0;
            int successful = 0;
            var config = Configuration.Default.WithDefaultLoader();

            string path = Environment.CurrentDirectory;
            DirectoryInfo dirInfo = new DirectoryInfo(path + "\\Result");

            foreach (var item in listOfId)
            {
                addressGroup = addressGroup.Remove(16).Insert(16, item);
                var document = BrowsingContext.New(config).OpenAsync(addressGroup);
                var parsedHtml = document.Result;

                var image = parsedHtml.All.Where(m => m.LocalName == "img" && m.Id.Equals("screenshot-image"));
                var imageUrl = image.First().GetAttribute("src");
                if (imageUrl.StartsWith("//st.prntscr.com")) 
                { 
                    fell++; continue; 
                } else {
                    successful++;
                    Console.WriteLine(imageUrl);
                    SaveFiles(imageUrl, item, dirInfo, path);
                }              
                                
            }
            int countSuccessfulFiles = dirInfo.GetFiles().Count();
            Console.WriteLine($"Удачно спарсились: {countSuccessfulFiles}, не удалось спарсить: {fell + (successful-countSuccessfulFiles)}");
            Console.WriteLine("Для выхода нажмите Enter клавишу");
            Console.ReadLine();
        }

    }
}
