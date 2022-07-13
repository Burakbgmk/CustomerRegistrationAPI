// See https://aka.ms/new-console-template for more information


using CustomerRegistration.Core.Entities;
using CustomerRegistration.Report.Dtos;

var customers = new List<Customer>()
{
    new Customer() {FirstName = "Burak", LastName ="Boğmak", City="Ankara",CommercialActivities=new List<CommercialActivity>(){new CommercialActivity(),new CommercialActivity()} },
    new Customer() {FirstName = "Zeynep", LastName ="Sarıhan", City="Ankara",CommercialActivities=new List<CommercialActivity>(){new CommercialActivity(),new CommercialActivity(),new CommercialActivity() } },
    new Customer() {FirstName = "Sezgin", LastName ="Göksügür", City="Ankara",CommercialActivities=new List<CommercialActivity>(){new CommercialActivity() } },
    new Customer() {FirstName = "Kadir", LastName ="Ilbi", City="Adana",CommercialActivities=new List<CommercialActivity>(){new CommercialActivity(),new CommercialActivity()}},
    new Customer() {FirstName = "Nuri", LastName ="Şen", City="Çanakkale", CommercialActivities = new List < CommercialActivity >() { new CommercialActivity(), new CommercialActivity() }},
    new Customer() {FirstName = "Nuri", LastName ="Şen", City="Çanakkale", CommercialActivities = new List < CommercialActivity >() { new CommercialActivity(), new CommercialActivity() }},
    new Customer() {FirstName = "Yusuf", LastName ="ince", City="Adana", CommercialActivities = new List < CommercialActivity >() { new CommercialActivity() }}
};

var counts = customers.OrderByDescending(x => x.CommercialActivities.Count()).Take(5).ToList();
foreach(var count in counts)
    Console.WriteLine(count.FirstName + count.CommercialActivities.Count());

//var list = new List<CustomerCountByCityDto>();
//var results = customers.GroupBy(x => x.City).ToList();
//foreach (var count in results)
//    list.Add(new CustomerCountByCityDto() { City = count.Key, Count = count.Count() });

//foreach (var customer in list)
//    Console.WriteLine(customer.City + customer.Count);
//var summary = from order in customers
//              group order by order.City into g
//              select new
//              {
//                  Ankara = g.Count(s => s.City == "Ankara"),
//                  Adana = g.Count(s => s.City == "Adana"),
//                  Çanakkale = g.Count(s => s.City == "Çanakkale"),
//              };
//Console.WriteLine(summary);
//foreach (var result in summary)
//    Console.WriteLine(result);



//using FluentEmail.Core;
//using FluentEmail.Razor;
//using FluentEmail.Smtp;
//using System.Net;
//using System.Net.Mail;
//using System.Text;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var sender = new SmtpSender(() => new SmtpClient("smtp.outlook.com")
//        {
//            EnableSsl = true,
//            DeliveryMethod = SmtpDeliveryMethod.Network,
//            Port = 587,
//            UseDefaultCredentials = false,
//            Credentials = new NetworkCredential()
//            {
//                UserName = "Burak_029920@hotmail.com",
//                Password = "Thewill1685"
//            }
//        });

//        Email.DefaultSender = sender;

//        var email = await Email
//            .From("Burak_029920@hotmail.com")
//            .To("Burak_029920@hotmail.com", "Burak")
//            .Subject("Deneme123")
//            .Body("Thanks for sldfksdf")
//            .AttachFromFilename("D:/Desktop/Patika/LINK-BOOTCAMP/BitirmeProjesi/ExcelFile/Demo.xlsx", "application/vnd.ms-excel","Demo.xlsx")
//            .SendAsync();
//    }
//}

