using System;
using System.Configuration;

namespace BlingIntegration
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------INTEGRAÇÃO BLING---------------");
            Console.WriteLine("INTEGRAÇÃO INICIADA");
            Console.WriteLine("***FAVOR NÃO FECHAR***");

            string apiKey = ConfigurationManager.AppSettings["APIkey"];
            ContactService.PrepareCustomerFile(apiKey);
            Console.WriteLine("Integração de clientes concluída.");
            ContactService.PrepareEmployeeFile(apiKey);
            Console.WriteLine("Integração de vendedores concluída.");
            ItemService.PrepareItemFile(apiKey);
            Console.WriteLine("Integração de produtos concluída.");
            OrderService.PrepareOrderFile(apiKey);
            Console.WriteLine("Integração de vendas concluída.");

            OrderCancelledService.PrepareOrderFile(apiKey);
            Console.WriteLine("Integração das vendas canceladas concluída.");

            Console.WriteLine("INTEGRAÇÃO CONCLUÍDA COM SUCESSO!");
        }
    }
}
