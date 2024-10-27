using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using practicePAL.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using practicePAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Json;




class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        //DataContextDapper dapper = new DataContextDapper(config);


        DataContextEF entityFramework = new DataContextEF(config);


        string computersJson = File.ReadAllText("Computers.json");

        

        Console.WriteLine(computersJson);


        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };



        IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

        if(computers != null)
        {
            foreach(Computer computer in computers)
            {
                entityFramework.Add(computer);
            }

           entityFramework.SaveChanges();

        }

        
        string computerCopy =  System.Text.Json.JsonSerializer.Serialize(computers, options);

        File.WriteAllText("ComputerCopy.txt", computerCopy);


    
    }


}











//DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        // Console.WriteLine(rightNow);
        // Computer myComputer = new Computer()
        //     {
        //         Motherboard = "Z690",
        //         HasWifi = true,
        //         HasLTE = false,
        //         ReleaseDate = DateTime.Now,
        //         Price = 943.87m,
        //         VideoCard = "RTX 2060"
        //     };

        // entityFramework.Add(myComputer);
        // entityFramework.SaveChanges();

        // string sql = @"INSERT INTO TutorialAppSchema.Computer (
        //         Motherboard,
        //         HasWifi,
        //         HasLTE,
        //         ReleaseDate,
        //         Price,
        //         VideoCard
        //     ) VALUES('" + myComputer.Motherboard 
        //             + "','" + myComputer.HasWifi
        //             + "','" + myComputer.HasLTE
        //             + "','" + myComputer.ReleaseDate
        //             + "','" + myComputer.Price
        //             + "','" + myComputer.VideoCard
        // + "')";

        // //File.WriteAllText("log.txt", "\n" + sql + "\n");

        // using StreamWriter openFile = new ("log.txt", append : true);

        // openFile.WriteLine(sql + "\n");

        // openFile.Close();


        // Console.WriteLine(File.ReadAllText("log.txt"));